using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class addUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO AspNetUsers (Id,Discriminator,DateCreated,Deactivated,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount) VALUES ('0b4c6369-e851-42ac-8945-deb415f590e5','ApplicationUser',GETDATE(),0,'fastlink','FASTLINK','martihills4real@gmail.com','MARTIHILLS4REAL@GMAIL.COM',0,'AQAAAAEAACcQAAAAEKxgpEgGE0UntU8gU0xYAxgcs2u7QXTxTgWKlUp2OVmnJUOA3wsRbjPdC4xTAlxOEg==','b75e3331-7ec6-4a41-a3ce-cb5c96c2f957','54035e93-6f10-4c1f-93a5-9a849db994f9','08140140494',0,0,NULL,0,0)");

            migrationBuilder.Sql("INSERT INTO AspNetRoles VALUES ('FB32F6DB-6C51-457F-8FC8-087F9BD0DCDC','Admin','Admin',NEWID())");

            migrationBuilder.Sql("INSERT INTO AspNetUserRoles VALUES ('0b4c6369-e851-42ac-8945-deb415f590e5','FB32F6DB-6C51-457F-8FC8-087F9BD0DCDC');");
        }
    }
}
