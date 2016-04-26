using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.DAL.EF
{
    [DbConfigurationType(typeof(FISEFDbConfiguration))]
    public sealed class FISEFDbContext : IdentityDbContext
    {
        static FISEFDbContext instance = null;

        public FISEFDbContext() : base("File_Interface_Simulator")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public static FISEFDbContext Instance
        /* De FISEFDbContext is een singleton klasse. We doen dit zodat alle repositories op dezelfde instantie van de context werken.
         * Hierdoor onstaan er geen problemen bij het inladen van gerelateerde objecten. 
         */
        {
            get
            {
                if (instance == null)
                {
                    instance = new FISEFDbContext();
                }
                return instance;
            }
        }

        public static FISEFDbContext Create()
        {
            return new FISEFDbContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
