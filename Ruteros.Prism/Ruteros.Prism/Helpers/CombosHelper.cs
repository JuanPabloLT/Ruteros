using Ruteros.Common.Models;
using System.Collections.Generic;

namespace Ruteros.Prism.Helpers
{
    public static class CombosHelper
    {
        public static List<Role> GetRoles()
        {
            return new List<Role>
            {
                new Role { Id = 1, Name = Languages.Driver}
            };
        }
    }


}
