using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CurrencyConverter.Api.Data.Migrations
{
    public partial class updatedrates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Assets_AssetBaseAssetId",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Assets_AssetQuoteAssetId",
                table: "ExchangeRates");

            migrationBuilder.RenameColumn(
                name: "AssetQuoteAssetId",
                table: "ExchangeRates",
                newName: "AssetQuoteId");

            migrationBuilder.RenameColumn(
                name: "AssetBaseAssetId",
                table: "ExchangeRates",
                newName: "AssetBaseId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeRates_AssetQuoteAssetId",
                table: "ExchangeRates",
                newName: "IX_ExchangeRates_AssetQuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeRates_AssetBaseAssetId",
                table: "ExchangeRates",
                newName: "IX_ExchangeRates_AssetBaseId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Assets_AssetBaseId",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Assets_AssetQuoteId",
                table: "ExchangeRates");

            migrationBuilder.RenameColumn(
                name: "AssetQuoteId",
                table: "ExchangeRates",
                newName: "AssetQuoteAssetId");

            migrationBuilder.RenameColumn(
                name: "AssetBaseId",
                table: "ExchangeRates",
                newName: "AssetBaseAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeRates_AssetQuoteId",
                table: "ExchangeRates",
                newName: "IX_ExchangeRates_AssetQuoteAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeRates_AssetBaseId",
                table: "ExchangeRates",
                newName: "IX_ExchangeRates_AssetBaseAssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Assets_AssetBaseAssetId",
                table: "ExchangeRates",
                column: "AssetBaseAssetId",
                principalTable: "Assets",
                principalColumn: "AssetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Assets_AssetQuoteAssetId",
                table: "ExchangeRates",
                column: "AssetQuoteAssetId",
                principalTable: "Assets",
                principalColumn: "AssetId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
