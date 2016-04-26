using DAL.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    class FISEFDbConfiguration : DbConfiguration
    {
        public FISEFDbConfiguration()
        {
            this.SetDefaultConnectionFactory(new SqlConnectionFactory());
            this.SetProviderServices("System.Data.SqlClient", SqlProviderServices.Instance);
            this.SetDatabaseInitializer<FISEFDbContext>(new FISEFDbInitializer());
        }
   
    }
}
