using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Config
    {
        public ConnectionStrings connectionStrings { get; set; }
        public AppSettings appSettings { get; set; }
    }

    public class ConnectionStrings
    {
        public string Database { get; set; }
    }

    public class AppSettings
    {
        public string numdiascambio { get; set; }
    }
}
