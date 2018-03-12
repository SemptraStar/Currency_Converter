using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CurrencyConverter.Data.Migrations
{
    public partial class updatedassets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Assets_AssetBaseId",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Assets_AssetQuoteId",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_AssetBaseId",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_AssetQuoteId",
                table: "ExchangeRates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.AlterColumn<string>(
                name: "AssetQuoteId",
                table: "ExchangeRates",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssetBaseId",
                table: "ExchangeRates",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssetBaseAssetId",
                table: "ExchangeRates",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AssetBaseIsTypeCrypto",
                table: "ExchangeRates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssetQuoteAssetId",
                table: "ExchangeRates",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AssetQuoteIsTypeCrypto",
                table: "ExchangeRates",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                columns: new[] { "AssetId", "IsTypeCrypto" });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_AssetBaseAssetId_AssetBaseIsTypeCrypto",
                table: "ExchangeRates",
                columns: new[] { "AssetBaseAssetId", "AssetBaseIsTypeCrypto" });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_AssetQuoteAssetId_AssetQuoteIsTypeCrypto",
                table: "ExchangeRates",
                columns: new[] { "AssetQuoteAssetId", "AssetQuoteIsTypeCrypto" });

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Assets_AssetBaseAssetId_AssetBaseIsTypeCrypto",
                table: "ExchangeRates",
                columns: new[] { "AssetBaseAssetId", "AssetBaseIsTypeCrypto" },
                principalTable: "Assets",
                principalColumns: new[] { "AssetId", "IsTypeCrypto" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Assets_AssetQuoteAssetId_AssetQuoteIsTypeCrypto",
                table: "ExchangeRates",
                columns: new[] { "AssetQuoteAssetId", "AssetQuoteIsTypeCrypto" },
                principalTable: "Assets",
                principalColumns: new[] { "AssetId", "IsTypeCrypto" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Assets_AssetBaseAssetId_AssetBaseIsTypeCrypto",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Assets_AssetQuoteAssetId_AssetQuoteIsTypeCrypto",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_AssetBaseAssetId_AssetBaseIsTypeCrypto",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_AssetQuoteAssetId_AssetQuoteIsTypeCrypto",
                table: "ExchangeRates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AssetBaseAssetId",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "AssetBaseIsTypeCrypto",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "AssetQuoteAssetId",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "AssetQuoteIsTypeCrypto",
                table: "ExchangeRates");

            migrationBuilder.AlterColumn<string>(
                name: "AssetQuoteId",
                table: "ExchangeRates",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssetBaseId",
                table: "ExchangeRates",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_AssetBaseId",
                table: "ExchangeRates",
                column: "AssetBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_AssetQuoteId",
                table: "ExchangeRates",
                column: "AssetQuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Assets_AssetBaseId",
                table: "ExchangeRates",
                column: "AssetBaseId",
                principalTable: "Assets",
                principalColumn: "AssetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Assets_AssetQuoteId",
                table: "ExchangeRates",
                column: "AssetQuoteId",
                principalTable: "Assets",
                principalColumn: "AssetId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
