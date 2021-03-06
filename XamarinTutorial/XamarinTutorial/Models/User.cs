﻿namespace XamarinTutorial.Models
{
    public class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public byte[] ImageArray { get; set; }
        public int? UserTypeId { get; set; }
        public string Password { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
        public string ImageFullPath { get; set; }

    }
}














