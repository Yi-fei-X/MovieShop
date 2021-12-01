using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface ICurrentUserService
    {
        // expose some properties and methods that can be implemented by CurrentUserService class
        // that will read user info from HttpContext 
        public int UserId { get; }
        public bool IsAuthenticated { get; }
        public string FullName { get; }
        public string Email { get; }
        public IEnumerable<string> Roles { get; }
        public bool IsAdmin { get; }
        public DateTime DateOfBirth { get; }
        // Only get method, no set method. So only class that implementing this interface can change the property. Outside that class can not change the property.
    }
}
