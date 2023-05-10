using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exams_performance
{
    [Table("User")]
    public class User
    {
        [Key]
        public int user_id { get; set; }

        private string user_login, user_password;

        public string User_login
        {
            get { return user_login; }
            set { user_login = value; }
        }

        public string User_password
        {
            get { return user_password; }
            set { user_password = value; }
        }

        public User() { }

        public User(string user_login, string user_password)
        {
            this.user_login = user_login;
            this.user_password = user_password;
        }
    }
}
