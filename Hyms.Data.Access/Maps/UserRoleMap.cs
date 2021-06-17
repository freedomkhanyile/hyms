using Hyms.Data.Access.Maps.Common;
using Hyms.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Hyms.Data.Access.Maps
{
    public class UserRoleMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<UserRole>()
                .ToTable("UserRoles")
                .HasKey(x => x.Id);
        }
    }
}
