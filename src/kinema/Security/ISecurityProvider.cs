using System.Collections.Generic;

namespace kinema.Security
{
    public interface ISecurityProvider
    {
        string GetDomain(byte[] messageIdentity);

        bool DomainIsAllowed(string domain);

        IEnumerable<string> GetAllowedDomains();
    }
}