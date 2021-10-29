namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataAnnotations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserBooks", "RoomType_Id", "dbo.RoomTypes");
            DropIndex("dbo.UserBooks", new[] { "RoomType_Id" });
            CreateIndex("dbo.UserBooks", "RoomId");
            AddForeignKey("dbo.UserBooks", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
            DropColumn("dbo.UserBooks", "RoomType_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserBooks", "RoomType_Id", c => c.Int());
            DropForeignKey("dbo.UserBooks", "RoomId", "dbo.Rooms");
            DropIndex("dbo.UserBooks", new[] { "RoomId" });
            CreateIndex("dbo.UserBooks", "RoomType_Id");
            AddForeignKey("dbo.UserBooks", "RoomType_Id", "dbo.RoomTypes", "Id");
        }
    }
}
