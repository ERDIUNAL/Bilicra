using System;
using System.Collections.Generic;
using System.Text;

namespace Bilicra.Persistence.Domain.Models
{
    public class ConnectionModel
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
