namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingHeaders", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.BookingHeaders", "UserID");
            AddForeignKey("dbo.BookingHeaders", "UserID", "dbo.MyUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingHeaders", "UserID", "dbo.MyUsers");
            DropIndex("dbo.BookingHeaders", new[] { "UserID" });
            DropColumn("dbo.BookingHeaders", "UserID");
        }
    }
}
