using System;
using System.Collections.Generic;
using System.Text;

namespace Dent
{
    public class DomainInfo
    {   public string DomaineName { get; set; }
        public string TLD { get; set; }
        public string StrippedDomain { get; set; }
        public string Subdomain { get; set; }
    }
}
