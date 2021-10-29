namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataAnnotationToRoomClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "Description", c => c.String());
        }
    }
}
