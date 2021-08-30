using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System;

namespace Dent
{
    class Domain : IDomain
    {
        private DomainInfo _domainInfo;

        /// <summary>
        /// This method extracts the Domain info, subdomain, tld, and base domain name from the fully qualified domain name.
        /// </summary>
        /// <param name="fqdn"></param>
        /// <returns>A DomainInfo object.</returns>
        public DomainInfo ExtractDomainInfo(string fqdn)
        {
            if(_domainInfo is null)
            {
                _domainInfo = new DomainInfo();
            }
            _domainInfo.DomaineName = fqdn;
        
            List<string> matches = GetTLDMatches();
            foreach (var tld in matches)
            {
                if (_domainInfo.DomaineName.Contains("." + tld))
                {
                    _domainInfo.TLD = tld;
                }
            }
            _domainInfo.StrippedDomain = _domainInfo.DomaineName.Replace("." + _domainInfo.TLD, "");
            _domainInfo.Subdomain = _domainInfo.StrippedDomain.Substring(0, _domainInfo.StrippedDomain.LastIndexOf("."));
            _domainInfo.StrippedDomain = _domainInfo.StrippedDomain.Replace(_domainInfo.Subdomain + ".", "");

            return _domainInfo;
        }

        /// <summary>
        /// This method downloads the public suffix list from here: https://publicsuffix.org/list/public_suffix_list.dat
        /// It extracts all of the tlds in the file, and puts them in a list of strings.
        /// </summary>
        /// <returns>List of TLD strings</returns>
        public List<string> GetOnlinePublicSuffixList()
        {
            List<string> tlds = new List<string>();
            var webRequest = WebRequest.Create(@"https://publicsuffix.org/list/public_suffix_list.dat");
            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!line.StartsWith("//") || !string.IsNullOrEmpty(line))
                    {
                        tlds.Add(line);
                    }
                }

            }
            return tlds;
        }

        /// <summary>
        /// This method looks at the _domainInfo member and returns all the possible tld matches in the order in which they appear in the tld list (see GetOnlinePublicSuffixList
        /// or GetOfflinePublicSuffixList)
        /// </summary>
        /// <returns>List of matching TLD strings</returns>
        private List<string> GetTLDMatches()
        {
            return GetOnlinePublicSuffixList().Where(x => _domainInfo.DomaineName.EndsWith("." + x)).ToList<string>();
        }

        /// <summary>
        /// This packages keeps an offline copy of the public suffix list so that this library can still work without and Internet connection.
        /// </summary>
        /// <returns></returns>
        public List<string> GetOfflinePublicSuffixList()
        {
            throw new Exception("Not implemented yet.");
        }
    }
}
