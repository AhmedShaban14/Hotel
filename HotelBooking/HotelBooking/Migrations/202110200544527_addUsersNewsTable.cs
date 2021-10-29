namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUsersNewsTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "UsersNews");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UsersNews", newName: "Users");
        }
    }
}
