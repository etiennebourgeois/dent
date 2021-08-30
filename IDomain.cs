using System;
using System.Collections.Generic;
using System.Text;

namespace Dent
{
    public interface IDomain
    {
        public DomainInfo ExtractDomainInfo(string fqdn);
        public List<string> GetOnlinePublicSuffixList();
        public List<string> GetOfflinePublicSuffixList();
    }
}
