using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamsPerformance
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        private string userLogin, userPassword;

        public string UserLogin
        {
            get { return userLogin; }
            set { userLogin = value; }
        }

        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }

        public User() { }

        public User(string userLogin, string userPassword)
        {
            this.userLogin = userLogin;
            this.userPassword = userPassword;
        }
    }
}
