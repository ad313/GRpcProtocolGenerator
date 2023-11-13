using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLogin.Models
{
    public class Config
    {
        public List<Site> Sites { get; set; }
        public List<User> Users { get; set; }
        public string Ocr { get; set; }
    }

    public class Site
    {
        public string Group { get; set; }

        //旧版网站
        public bool Old { get; set; }

        public List<SiteItem> Items { get; set; }
    }

    public class SiteItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class User
    {
        public string Group { get; set; }
        public List<UserItem> Items { get; set; }
    }

    public class UserItem
    {
        public string User { get; set; }
        public string Password { get; set; }
    }

}