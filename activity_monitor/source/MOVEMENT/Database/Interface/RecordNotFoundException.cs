using System;
using System.Collections.Generic;
using System.Text;

namespace Movement.Database
{
	/// <summary>
	/// Occurs when an action requires a database record that does not exist.
	/// </summary>
	public class RecordNotFoundException : Exception
	{
		public override string Message
		{
			get
			{
				return @"Database record not found.";
			}
		}
	}
}
