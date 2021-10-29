namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDNewCols : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserBooks", newName: "RoomUserBookings");
            DropForeignKey("dbo.UserBooks", "UserId", "dbo.UsersNews");
            RenameColumn(table: "dbo.HotelBrunches", name: "UserBook_UserId", newName: "RoomUserBooking_UserId");
            RenameColumn(table: "dbo.HotelBrunches", name: "UserBook_RoomId", newName: "RoomUserBooking_RoomId");
            RenameIndex(table: "dbo.HotelBrunches", name: "IX_UserBook_UserId_UserBook_RoomId", newName: "IX_RoomUserBooking_UserId_RoomUserBooking_RoomId");
            CreateTable(
                "dbo.MyUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserWallet = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddForeignKey("dbo.RoomUserBookings", "UserId", "dbo.MyUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.UsersNews");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersNews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Wallet = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            DropForeignKey("dbo.RoomUserBookings", "UserId", "dbo.MyUsers");
            DropTable("dbo.MyUsers");
            RenameIndex(table: "dbo.HotelBrunches", name: "IX_RoomUserBooking_UserId_RoomUserBooking_RoomId", newName: "IX_UserBook_UserId_UserBook_RoomId");
            RenameColumn(table: "dbo.HotelBrunches", name: "RoomUserBooking_RoomId", newName: "UserBook_RoomId");
            RenameColumn(table: "dbo.HotelBrunches", name: "RoomUserBooking_UserId", newName: "UserBook_UserId");
            AddForeignKey("dbo.UserBooks", "UserId", "dbo.UsersNews", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.RoomUserBookings", newName: "UserBooks");
        }
    }
}
