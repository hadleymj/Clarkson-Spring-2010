using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using Movement.UserInterface.movement_web_service;
using System.IO;

namespace Movement.UserInterface
{
    class Connection
    {
        private static WebServicer web_connection = new WebServicer();
        private UserObject _User;
        private String hostname;
        private Boolean connected;
        private String error;
        private MessageBoxIcon error_type;

        /// <summary>
        /// Connect to the server with the given username and password
        /// </summary>
        /// <param name="host">Host to connect to</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public Boolean Connect(String host, String username, String password)
        {
            hostname = host;

            try
            {
                //build the web service url from the given host
                web_connection.Servicer.Url = "http://" + host + "/Web/Service.asmx";
                //_User.userName = username;
                //_User.password = password;
                //try to login the user
                _User = web_connection.Servicer.logIn(username, password);
            }
            catch (System.UriFormatException)
            {
                return false;
            }
            catch (System.Net.WebException)
            {
                return false;
            }

            connected = true;
            return true;
        }


        public Boolean Disconnect()
        {
            try
            {
                if (web_connection.Servicer.logOut())
                {
                    connected = false;
                    return true;
                }
            }
            catch (System.Net.WebException e)
            {
                error = e.Message;
                error_type = MessageBoxIcon.Error;
                return false;
            }

            error = "The system could not disconnect at this time.";
            error_type = MessageBoxIcon.Error;
            return false;
        }

        /// <summary>
        /// Rerturns the user object obtained after login
        /// </summary>
        public UserObject User
        {
            get { return _User; }

        }

        /// <summary>
        /// Rerturns the current hostname
        /// </summary>
        public String Host
        {
            get { return hostname; }

        }

        /// <summary>
        /// Returns whether or not the client is connected to the server
        /// </summary>
        public Boolean Connected
        {
            get { return connected; }

        }

        /// <summary>
        /// Appends the host last connected to to a file of host previously connected to
        /// </summary>
        /// <param name="filename">File to append the host to</param>
        /// <returns>True if the write was successful, false if not</returns>
        public int SaveServer(String filename)
        {
            FileStream fs;
            BinaryWriter bw;

            //get the list of host to make sure we don't add it twice
            ArrayList temp = LoadServers(filename);

            //Apped the host to the file if the file is empty, or if the host is not already in the file
            if (temp == null || !temp.Contains(hostname))
            {
                try
                {
                    fs = new FileStream(filename, FileMode.Append);
                    bw = new BinaryWriter(fs);
                }
                catch (System.IO.FileNotFoundException)
                {
                    return 0;
                }

                //write the data and close the stream
                bw.Write(hostname);
                bw.Close();
                fs.Close();
            }
            return 1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">File to load the hosts from</param>
        /// <returns>An ArrayList of strings</returns>
        public ArrayList LoadServers(String filename)
        {
            ArrayList temp = new ArrayList();
            FileStream fs;
            BinaryReader br;
            
            try
            {
                fs = File.OpenRead(filename);
                br = new BinaryReader(fs);
            }
            catch (System.IO.FileNotFoundException)
            {
                return null;
            }
            
            //Read all the hosts in the file
            while(br.PeekChar() != -1)
                temp.Add(br.ReadString());

            //close the stream
            br.Close();
            fs.Close();

            return temp;
        }


        /// <summary>
        /// Error property
        /// </summary>
        public String Error
        {
            get { return error; }
        }

        /// <summary>
        /// Error type property
        /// </summary>
        public MessageBoxIcon ErrorType
        {
            get { return error_type; }
        }
    }
}
