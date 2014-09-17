namespace TopMachine.Topping.Dto
{
    using System;
    using System.Collections.Generic;

    public class UserDto
    {
        private string _Email;
        public string _Firstname;
        public DateTime _LastLoginDate;
        public string _Lastname;
        private List<string> _Roles;
        private string _Username;

        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                this._Email = value;
            }
        }

        public string Firstname
        {
            get
            {
                return this._Firstname;
            }
            set
            {
                this._Firstname = value;
            }
        }

        public DateTime LastLoginDate
        {
            get
            {
                return this._LastLoginDate;
            }
            set
            {
                this._LastLoginDate = value;
            }
        }

        public string Lastname
        {
            get
            {
                return this._Lastname;
            }
            set
            {
                this._Lastname = value;
            }
        }

        public List<string> Roles
        {
            get
            {
                return this._Roles;
            }
            set
            {
                this._Roles = value;
            }
        }

        public string Username
        {
            get
            {
                return this._Username;
            }
            set
            {
                this._Username = value;
            }
        }
    }
}

