namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUserIdToString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookingHeaders", "UserID", "dbo.MyUsers");
            DropIndex("dbo.BookingHeaders", new[] { "UserID" });
            AlterColumn("dbo.BookingHeaders", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.BookingHeaders", "UserID");
            AddForeignKey("dbo.BookingHeaders", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingHeaders", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.BookingHeaders", new[] { "UserID" });
            AlterColumn("dbo.BookingHeaders", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.BookingHeaders", "UserID");
            AddForeignKey("dbo.BookingHeaders", "UserID", "dbo.MyUsers", "Id", cascadeDelete: true);
        }
    }
}
