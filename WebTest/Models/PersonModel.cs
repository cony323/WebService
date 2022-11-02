using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTest.Models
{
    public class PersonModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Tel { get; set; }

        public DateTime SignTime { get; set; }
    }
}