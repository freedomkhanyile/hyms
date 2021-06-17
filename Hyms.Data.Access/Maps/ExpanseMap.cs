using Hyms.Data.Access.Maps.Common;
using Hyms.Data.Model;
using Microsoft.EntityFrameworkCore;
 

namespace Hyms.Data.Access.Maps
{
    public class ExpenseMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Expense>()
                   .ToTable("Expenses")
                   .HasKey(x => x.Id);
        }
    }
}
