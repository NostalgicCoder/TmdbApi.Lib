﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestHarness.Visual.Migrations
{
    /// <inheritdoc />
    public partial class AddWatchedMediaToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchedMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TMDBId = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WatchedQty = table.Column<int>(type: "int", nullable: false),
                    FirstWatched = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastWatched = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchedMedia", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchedMedia");
        }
    }
}