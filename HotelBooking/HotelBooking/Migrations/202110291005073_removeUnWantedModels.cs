namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeUnWantedModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HotelBrunches", new[] { "RoomUserBooking_UserId", "RoomUserBooking_RoomId" }, "dbo.RoomUserBookings");
            DropForeignKey("dbo.RoomUserBookings", "UserId", "dbo.MyUsers");
            DropForeignKey("dbo.RoomUserBookings", "RoomId", "dbo.Rooms");
            DropIndex("dbo.HotelBrunches", new[] { "RoomUserBooking_UserId", "RoomUserBooking_RoomId" });
            DropIndex("dbo.RoomUserBookings", new[] { "UserId" });
            DropIndex("dbo.RoomUserBookings", new[] { "RoomId" });
            DropColumn("dbo.HotelBrunches", "RoomUserBooking_UserId");
            DropColumn("dbo.HotelBrunches", "RoomUserBooking_RoomId");
            DropTable("dbo.MyUsers");
            DropTable("dbo.RoomUserBookings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoomUserBookings",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoomId });
            
            CreateTable(
                "dbo.MyUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserWallet = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.HotelBrunches", "RoomUserBooking_RoomId", c => c.Int());
            AddColumn("dbo.HotelBrunches", "RoomUserBooking_UserId", c => c.Int());
            CreateIndex("dbo.RoomUserBookings", "RoomId");
            CreateIndex("dbo.RoomUserBookings", "UserId");
            CreateIndex("dbo.HotelBrunches", new[] { "RoomUserBooking_UserId", "RoomUserBooking_RoomId" });
            AddForeignKey("dbo.RoomUserBookings", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoomUserBookings", "UserId", "dbo.MyUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HotelBrunches", new[] { "RoomUserBooking_UserId", "RoomUserBooking_RoomId" }, "dbo.RoomUserBookings", new[] { "UserId", "RoomId" });
        }
    }
}
