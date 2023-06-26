﻿using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EMS.Common.DB
{
    public  class ORMContext : DbContext
    {
        public ORMContext()
            : base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder()
                {
                    DataSource = "LocalDb.db",
                    ForeignKeys = true
                }.ConnectionString
            }, true)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // 如果不存在数据库，则创建之
            Database.SetInitializer(new SqliteDropCreateDatabaseWhenModelChanges<ORMContext>(modelBuilder));
        }

        public DbSet<BatteryModel> BatteryModelInfos { get; set; }
    }
}