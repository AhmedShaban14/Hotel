namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesUserBookModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HotelBrunches", "UserBook_UserId", c => c.Int());
            AddColumn("dbo.HotelBrunches", "UserBook_RoomId", c => c.Int());
            CreateIndex("dbo.HotelBrunches", new[] { "UserBook_UserId", "UserBook_RoomId" });
            AddForeignKey("dbo.HotelBrunches", new[] { "UserBook_UserId", "UserBook_RoomId" }, "dbo.UserBooks", new[] { "UserId", "RoomId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HotelBrunches", new[] { "UserBook_UserId", "UserBook_RoomId" }, "dbo.UserBooks");
            DropIndex("dbo.HotelBrunches", new[] { "UserBook_UserId", "UserBook_RoomId" });
            DropColumn("dbo.HotelBrunches", "UserBook_RoomId");
            DropColumn("dbo.HotelBrunches", "UserBook_UserId");
        }
    }
}
