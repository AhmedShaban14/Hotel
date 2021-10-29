namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImageUrlToRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "Image");
        }
    }
}
