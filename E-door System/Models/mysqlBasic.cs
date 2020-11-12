namespace E_door_System.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class mysqlBasic : DbContext
    {
        public mysqlBasic()
            : base("name=eDoor")
        {
        }

        public virtual DbSet<demand_list> demand_list { get; set; }
        public virtual DbSet<department> departments { get; set; }
        public virtual DbSet<first_type> first_type { get; set; }
        public virtual DbSet<location> locations { get; set; }
        public virtual DbSet<project> projects { get; set; }
        public virtual DbSet<tab_second_type> tab_second_type { get; set; }
        public virtual DbSet<tier> tiers { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<power> powers { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<demand_list>()
                .Property(e => e.from)
                .IsUnicode(false);

            modelBuilder.Entity<demand_list>()
                .Property(e => e.senior_tab)
                .IsUnicode(false);

            modelBuilder.Entity<demand_list>()
                .Property(e => e.tab)
                .IsUnicode(false);

            modelBuilder.Entity<demand_list>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<demand_list>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<demand_list>()
                .Property(e => e.to)
                .IsUnicode(false);

            modelBuilder.Entity<demand_list>()
                .Property(e => e.cc)
                .IsUnicode(false);

            modelBuilder.Entity<demand_list>()
                .Property(e => e.session_location)
                .IsUnicode(false);

            modelBuilder.Entity<demand_list>()
                .Property(e => e.session_duration)
                .IsUnicode(false);

            modelBuilder.Entity<demand_list>()
                .Property(e => e.remark)
                .IsUnicode(false);

            modelBuilder.Entity<demand_list>()
                .Property(e => e.attatchment)
                .IsUnicode(false);

            modelBuilder.Entity<department>()
                .Property(e => e.department1)
                .IsUnicode(false);

            modelBuilder.Entity<first_type>()
                .Property(e => e.tab_type)
                .IsUnicode(false);

            modelBuilder.Entity<first_type>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<location>()
                .Property(e => e.location1)
                .IsUnicode(false);

            modelBuilder.Entity<project>()
                .Property(e => e.project1)
                .IsUnicode(false);

            modelBuilder.Entity<tab_second_type>()
                .Property(e => e.first_type)
                .IsUnicode(false);

            modelBuilder.Entity<tab_second_type>()
                .Property(e => e.second_type)
                .IsUnicode(false);

            modelBuilder.Entity<tab_second_type>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<tier>()
                .Property(e => e.tier_level)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.displayname)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.ntid)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.employeeNum)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.tier)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.department)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.project)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.eMail)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.power)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.phoneNum)
                .IsUnicode(false);
        }
    }
}
