using System.Collections.Generic;

namespace kinema.Routing
{
    public interface IInternalRegistrationService
    {
        void RegisterInternalRoutes(IEnumerable<InternalRouteRegistration> registrations);
    }
}