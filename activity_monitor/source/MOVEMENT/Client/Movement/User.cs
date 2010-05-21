using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Movement.UserInterface.movement_web_service;

namespace Movement.UserInterface
{
    class User
    {
        WebServicer Connection = new WebServicer();
        String error;
        MessageBoxIcon error_type;

        public Boolean CreateUser(String name, String contact, String role, String username, String password, String confirm_password)
        {
            //set the properties of the new user
            UserObject user = new UserObject();
            user.name = name;
            user.contactInfo = contact;
            user.userName = username;
            user.password = password;

            if (role == "Adminitrator")
                user.role = 'A';
            else if (role == "Clinician")
                user.role = 'C';

            //check for matching passwords and required information
            if (password != confirm_password)
            {
                error = "The entered passwords do not match.";
                error_type = MessageBoxIcon.Warning;
                return false;
            }

            //check for empty fields
            if (password.Equals("") || username.Equals("") || name.Equals(""))
            {
                error = "Please fill out all user information.";
                error_type = MessageBoxIcon.Warning;
                return false;
            }

            //try to create the user
            try
            {
                if (!Connection.Servicer.makeUser(user))
                {
                    error = "An error occured while creating the user.  Please try again.";
                    error_type = MessageBoxIcon.Error;
                    return false;
                }
            }
            catch (System.Net.WebException e)
            {
                error = e.Message;
                error_type = MessageBoxIcon.Error;
                return false;
            }

            return true;
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