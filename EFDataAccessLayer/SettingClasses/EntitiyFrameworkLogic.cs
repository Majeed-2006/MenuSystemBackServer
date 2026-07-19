using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.SettingClasses
{
    internal class EntitiyFrameworkLogic : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var configurations = new ConfigurationBuilder()
                .AddJsonFile("connstr.json")
                .Build();
            var constr = configurations.GetSection("Const").Value;
            optionsBuilder.UseSqlServer(constr);
        }
    }

}
