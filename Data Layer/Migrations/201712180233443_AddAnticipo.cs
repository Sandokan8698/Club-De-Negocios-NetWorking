namespace Data_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnticipo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Anticipo",
                c => new
                    {
                        AnticipoId = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, storeType: "money"),
                        Fecha = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.AnticipoId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Anticipo", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.Anticipo", new[] { "ClienteId" });
            DropTable("dbo.Anticipo");
        }
    }
}
