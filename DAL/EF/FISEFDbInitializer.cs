using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    class FISEFDbInitializer: CreateDatabaseIfNotExists<FISEFDbContext>
    {
        protected override void Seed(FISEFDbContext context)
        {
            context.SaveChanges();//
        }
    }
}
