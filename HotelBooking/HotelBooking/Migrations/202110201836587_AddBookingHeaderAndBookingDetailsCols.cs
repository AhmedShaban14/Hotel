namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookingHeaderAndBookingDetailsCols : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingHeaderId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingHeaders", t => t.BookingHeaderId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.BookingHeaderId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.BookingHeaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingDate = c.DateTime(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RoomsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingDetails", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.BookingDetails", "BookingHeaderId", "dbo.BookingHeaders");
            DropIndex("dbo.BookingDetails", new[] { "RoomId" });
            DropIndex("dbo.BookingDetails", new[] { "BookingHeaderId" });
            DropTable("dbo.BookingHeaders");
            DropTable("dbo.BookingDetails");
        }
    }
}
