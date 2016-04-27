using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
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
    public sealed class FISEFDbContext : DbContext
    {
        static FISEFDbContext instance = null;
        //Setup
        public DbSet<Directory> Directories { get; set; }
        public DbSet<FieldSpecFieldCondition> FieldSpecFieldConditions { get; set; }
        public DbSet<FieldSpecification> FieldSpecifications { get; set; }
        public DbSet<FileSpecification> FileSpecifications { get; set; }
        public DbSet<FileSpecFieldCondition> FileSpecFieldConditions { get; set; }
        public DbSet<GroupCondition> GroupConditions { get; set; }
        public DbSet<HeaderCondition> HeaderConditions { get; set; }
        public DbSet<WorkflowTemplate> WorkflowTemplates { get; set; }
        public DbSet<AllowedValue> AllowedValues { get; set; }


        // Operational
        public DbSet<Field> Fields { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<HeaderField> HeaderFields { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Workflow> Workflows { get; set; }


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
