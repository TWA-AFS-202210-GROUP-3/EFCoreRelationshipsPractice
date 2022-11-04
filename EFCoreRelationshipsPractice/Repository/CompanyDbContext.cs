﻿using EFCoreRelationshipsPractice.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsPractice.Repository
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        {
        }

        public DbSet<CompanyEntity> Companies
        {
            get;
            set;
        }

        public DbSet<ProfileEntity> Profiles
        {
            get;
            set;
        }

        public DbSet<EmployeeEntity> Employees  // DbSet 中的 set 为名词 集合
        {
            get;
            set;
        }
    }
}