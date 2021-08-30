using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Dent;

namespace DomainTLD_Stripper
{
    class Program
    {
        static void Main(string[] args)
        {
            bool another = true;

            while (another)
            {

                
                Domain domain = new Domain();

                Console.WriteLine("Please enter a FQDN to strip the subdomains and TLDs: ");
                string domainName = Console.ReadLine();

                DomainInfo dinfo = domain.ExtractDomainInfo(domainName);

                Console.WriteLine("Stripping TLD: {0}", dinfo.TLD);

                
                Console.WriteLine("Subdomain is: {0}", dinfo.Subdomain);

                
                Console.WriteLine("Stripped subdomain: {0}", dinfo.StrippedDomain);



                Console.WriteLine("Would you like to process another domain (y/n)?");
                var key = Console.ReadKey();

                if(key.KeyChar == 'y')
                {
                    another = true;
                } else
                {
                    another = false;
                }
            }
            

        }
    }
}
