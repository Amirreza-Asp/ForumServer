﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Persistence.Migrations
{
    public partial class AddRegisterAtToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterAt",
                table: "AspNetUsers");
        }
    }
}
