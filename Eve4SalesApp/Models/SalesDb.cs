using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Eve4SalesApp.Models
{
    public class SalesDb : DbContext

    {

        public SalesDb() : base("name=SalesConnection") { }



        public DbSet<SalesModel> SalesModels { get; set; }
        public DbSet<SalesMarketModel> SalesMarketModels { get; set; }
        public DbSet<ModelYearDeclaration> ModelYearDeclarations { get; set; }
        public DbSet<SalesFigure> SalesFigures { get; set; }

        public DbSet<PortalStats> PortalStats { get; set; }

        public DbSet<ReleaseModel> CurrentRelease { get; set; }

        public System.Data.Entity.DbSet<Eve4SalesApp.Models.ExcelExportViewModel> ListViewModels { get; set; }

        public System.Data.Entity.DbSet<Eve4SalesApp.Models.StatsViewModel> StatsViewModels { get; set; }

        public System.Data.Entity.DbSet<Eve4SalesApp.Models.MarketOverviewViewModel> MarketOverviewViewModels { get; set; }

        public System.Data.Entity.DbSet<Eve4SalesApp.Models.UserEditModel> UserEditModels { get; set; }

        public System.Data.Entity.DbSet<Eve4SalesApp.Models.AggEuList> AggEuLists { get; set; }
    }

}