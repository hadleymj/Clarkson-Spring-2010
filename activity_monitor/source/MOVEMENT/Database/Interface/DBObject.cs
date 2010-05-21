using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Movement.Database
{
	/// <summary>
	/// Manages interaction with the underlying database.
	/// </summary>
	public abstract class DBObject
	{
		protected DateTime _RetrievalTimestamp = DateTime.UtcNow;

		/// <summary>
		/// The datetime that this object's data was last retrieved from the database (in UTC time).
		/// </summary>
		/// <remarks>This can be used as an indication as to the age of the data.  The older
		/// the timestamp, the greater the chance that the data stored in the object may differ
		/// from that stored in the database.</remarks>
		public DateTime RetrievalTimestamp { get { return _RetrievalTimestamp; } }

        #region Static Initialization
        static DBObject()
		{
            // read the connection string out of a "config" file
			Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
			XmlReaderSettings Settings = new XmlReaderSettings();
			Settings.IgnoreComments = true;
			Settings.IgnoreProcessingInstructions = true;
			Settings.IgnoreWhitespace = true;
			Settings.ValidationType = ValidationType.None;

			try
			{
				using(XmlReader Reader = XmlReader.Create(CurrentAssembly.CodeBase + ".config", Settings))
				{
					while(Reader.Read())
					{
						if(Reader.Name == "add" && string.Compare(Reader.GetAttribute("key"), "DefaultDatabase", true) == 0)
						{
							_ConnectionString = Reader.GetAttribute("value");
							break;
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
				if(_ConnectionString == string.Empty)
					throw new ApplicationException("Database configuration file not found or format is invalid.");

			}
		}

		private static string _ConnectionString = string.Empty;
        #endregion

        private static string ConnectionString{get{return _ConnectionString;}}

		/// <summary>
		/// Gets a new connection from the connection pool.
		/// </summary>
		/// <returns></returns>
		protected static DbConnection GetConnection()
		{
			return new SqlConnection(ConnectionString);
		}

		/// <summary>
		/// Gets a new connection from the connection pool that supports asynchronous command execution.
		/// </summary>
		/// <returns></returns>
		protected static DbConnection GetAsyncConnection()
		{
			return new SqlConnection(string.Concat(ConnectionString, @";Asynchronous Processing=true"));
		}


		/// <summary>
		/// Creates a new SQL command.
		/// </summary>
		/// <param name="conn">The connection that the command will be executed on.</param>
		/// <param name="trans">The transaction that the command will be executed against, or null for an implicit transaction.</param>
		/// <param name="command">The name of the stored procedure.</param>
		/// <param name="args">The arguments to the stored procedure, in (string)Name, (object)Value pairs.</param>
		/// <returns>The created SQL command.</returns>
		private static SqlCommand CreateCommand(DbConnection conn, DbTransaction trans, string command, params object[] args)
		{
			SqlCommand Command = new SqlCommand(command, (SqlConnection)conn);
			Command.CommandType = CommandType.StoredProcedure;

			if(trans != null)
				Command.Transaction = (SqlTransaction)trans;

			for(int i = 0; i < args.Length; i++)
			{
				//add the parameters, each in name, value pairs
				Command.Parameters.Add(new SqlParameter((string)args[i], args[++i]));
			}

			return Command;
		}

		/// <summary>
		/// Creates and prepares a new SQL command.
		/// </summary>
		/// <param name="conn">The connection that the command will be executed on.</param>
		/// <param name="trans">The transaction that the command will be executed against, or null for an implicit transaction.</param>
		/// <param name="command">The name of the stored procedure.</param>
		/// <returns>The prepared SQL command.</returns>
		protected static DbCommand PrepareCommand(DbConnection conn, DbTransaction trans, string command)
		{
			SqlCommand Command = new SqlCommand(command, (SqlConnection)conn);
			Command.CommandType = CommandType.StoredProcedure;

			if(trans != null)
				Command.Transaction = (SqlTransaction)trans;

			SqlCommandBuilder.DeriveParameters(Command);

			return Command;
		}

		#region DBNull Read Operations

		/// <summary>
		/// Reads a field value from a reader and checks if it is null.  If the field is null, the default
		/// argument is returned as the value.  Otherwise, the field value is returned.
		/// </summary>
		/// <typeparam name="T">The type of data read.</typeparam>
		/// <param name="reader">The reader to read data from.</param>
		/// <param name="field">The name of the column to read from.</param>
		/// <param name="def">The default that will be returned if the column is null.</param>
		/// <returns>The field value if not null, otherwise the default.</returns>
		protected static T SafeRead<T>(IDataReader reader, string field, T def)
		{
			int o = reader.GetOrdinal(field);
			return reader.IsDBNull(o) ? def : (T)Convert.ChangeType(reader[o], def.GetType());
		}

		/// <summary>
		/// Reads a field value from a reader and checks if it is null.  If the field is null, the default
		/// argument is returned as the value.  Otherwise, the field value is returned.
		/// </summary>
		/// <param name="reader">The reader to read data from.</param>
		/// <param name="field">The name of the column to read from.</param>
		/// <param name="def">The default that will be returned if the column is null.</param>
		/// <returns>The field value if not null, otherwise the default.</returns>
		/// <remarks>Specialized for char since reading a single char requires slightly different operations.</remarks>
		/// <see cref="Movement.Database.DBObject.SafeRead"/>
		protected static char SafeRead(IDataReader reader, string field, char def)
		{
			return SafeRead(reader, field, new string(def, 1))[0];
		}

		#endregion

		#region Command Execution


		/// <summary>
		/// Executes a non-query command against a connection and an implicit transaction.
		/// </summary>
		/// <param name="conn">The connection to execute the command on.</param>
		/// <param name="command">The name of the stored procedure to execute.</param>
		/// <param name="args">The arguments to the stored procedure, in (string)Name, (object)Value pairs.</param>
		/// <remarks>If the connection is closed when Execute is called, the connection will be opened before
		/// executing the stored procedure and then closed.</remarks>
		protected static int Execute(DbConnection conn, string command, params object[] args)
		{
			return Execute(conn, null, command, args);
		}

		/// <summary>
		/// Executes a command against a connection and an implicit transaction and returns a reader for reading the results.
		/// </summary>
		/// <param name="conn">The connection to execute the command on.</param>
		/// <param name="command">The name of the stored procedure to execute.</param>
		/// <param name="behavior">The behavior for the reader.</param>
		/// <param name="args">The arguments to the stored procedure, in (string)Name, (object)Value pairs.</param>
		/// <remarks>If the connection is closed when ExecuteReader is called and behavior includes the 
		/// CloseConnection behavior, then the connection is opened before executing the command.</remarks>
		protected static IDataReader ExecuteReader(DbConnection conn, string command, CommandBehavior behavior, params object[] args)
		{
			return ExecuteReader(conn, null, command, behavior, args);
		}

		/// <summary>
		/// Executes a scalar query command against a connection and an implicit transaction.
		/// </summary>
		/// <param name="conn">The connection to execute the command on.</param>
		/// <param name="command">The name of the stored procedure to execute.</param>
		/// <param name="args">The arguments to the stored procedure, in (string)Name, (object)Value pairs.</param>
		/// <remarks>If the connection is closed when ExecuteScalar is called, the connection will be opened before
		/// executing the stored procedure and then closed.</remarks>
		protected static object ExecuteScalar(DbConnection conn, string command, params object[] args)
		{
			return ExecuteScalar(conn, null, command, args);
		}

		/// <summary>
		/// Begins execution of a non-query command against a connection and transaction.
		/// </summary>
		/// <param name="conn">The connection to execute the command on.</param>
		/// <param name="trans">The transaction to execute the command against, or null to use implicit transactioning.</param>
		/// <param name="command">The name of the stored procedure to execute.</param>
		/// <param name="args">The arguments to the stored procedure, in (string)Name, (object)Value pairs.</param>
		/// <remarks>The connection must be opened.</remarks>
		protected static IAsyncResult BeginExecute(DbConnection conn, DbTransaction trans, string command, params object[] args)
		{
			return BeginExecute(CreateCommand(conn, trans, command, args));
		}

		/// <summary>
		/// Begins execution of a non-query command against a connection and transaction.
		/// </summary>
		/// <param name="command">The command to begin executing.</param>
		/// <remarks>The connection must be opened.</remarks>
		protected static IAsyncResult BeginExecute(DbCommand command)
		{
			//begin command execution
			return ((SqlCommand)command).BeginExecuteNonQuery(null, command);
		}

		/// <summary>
		/// Completes execution of a previously asynchronously-executed command.
		/// </summary>
		/// <param name="r">The result of starting execution of an asynchronous command.</param>
		/// <remarks>Null instances of IAsyncResult are ignored (for ease of callee) and -1 is returned.</remarks>
		protected static void EndExecute(IAsyncResult r)
		{
			//ignore null async results
			if(r != null)
				((SqlCommand)r.AsyncState).EndExecuteNonQuery(r);
		}

		/// <summary>
		/// Executes a non-query command against a connection and transaction.
		/// </summary>
		/// <param name="conn">The connection to execute the command on.</param>
		/// <param name="trans">The transaction to execute the command against, or null to use implicit transactioning.</param>
		/// <param name="command">The name of the stored procedure to execute.</param>
		/// <param name="args">The arguments to the stored procedure, in (string)Name, (object)Value pairs.</param>
		/// <remarks>If the connection is closed when Execute is called, the connection will be opened before
		/// executing the stored procedure and then closed.</remarks>
		protected static int Execute(DbConnection conn, DbTransaction trans, string command, params object[] args)
		{
			bool OpenConnection = (conn.State & ConnectionState.Open) == 0;

			SqlCommand Command = CreateCommand(conn, trans, command, args);

			//get the return value
			SqlParameter ReturnParameter = new SqlParameter("@RETURN", SqlDbType.Int);
			ReturnParameter.Direction = ParameterDirection.ReturnValue;

			Command.Parameters.Add(ReturnParameter);

			if(OpenConnection)
			{
				conn.Open();
				try { Command.ExecuteNonQuery(); }
				finally { conn.Close(); }
			}
			else
			{
				Command.ExecuteNonQuery();
			}

			return (int)ReturnParameter.Value;
		}

		/// <summary>
		/// Executes a command against a connection and transaction and returns a reader for reading the results.
		/// </summary>
		/// <param name="conn">The connection to execute the command on.</param>
		/// <param name="trans">The transaction to execute the command against, or null to use implicit transactioning.</param>
		/// <param name="command">The name of the stored procedure to execute.</param>
		/// <param name="behavior">The behavior for the reader.</param>
		/// <param name="args">The arguments to the stored procedure, in (string)Name, (object)Value pairs.</param>
		/// <remarks>If the connection is closed when ExecuteReader is called and behavior includes the 
		/// CloseConnection behavior, then the connection is opened before executing the command.</remarks>
		protected static IDataReader ExecuteReader(DbConnection conn, DbTransaction trans, string command, CommandBehavior behavior, params object[] args)
		{
			bool OpenConnection = (conn.State & ConnectionState.Open) == 0 && (behavior & CommandBehavior.CloseConnection) == CommandBehavior.CloseConnection;

			if(OpenConnection)
				conn.Open();

			return CreateCommand(conn, trans, command, args).ExecuteReader(behavior);
		}

		/// <summary>
		/// Executes a scalar query command against a connection and transaction.
		/// </summary>
		/// <param name="conn">The connection to execute the command on.</param>
		/// <param name="trans">The transaction to execute the command against, or null to use implicit transactioning.</param>
		/// <param name="command">The name of the stored procedure to execute.</param>
		/// <param name="args">The arguments to the stored procedure, in (string)Name, (object)Value pairs.</param>
		/// <remarks>If the connection is closed when ExecuteScalar is called, the connection will be opened before
		/// executing the stored procedure and then closed.</remarks>
		protected static object ExecuteScalar(DbConnection conn, DbTransaction trans, string command, params object[] args)
		{
			bool OpenConnection = (conn.State & ConnectionState.Open) == 0;

			if(OpenConnection)
			{
				conn.Open();
				try { return CreateCommand(conn, trans, command, args).ExecuteScalar(); }
				finally { conn.Close(); }
			}
			else
			{
				return CreateCommand(conn, trans, command, args).ExecuteScalar();
			}

		}

		#endregion
	}
}
