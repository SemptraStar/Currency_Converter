﻿// <auto-generated />
using CurrencyConverter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CurrencyConverter.Data.Migrations
{
    [DbContext(typeof(CurrencyContext))]
    partial class CurrencyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CurrencyConverter.Data.Models.DataModels.Asset", b =>
                {
                    b.Property<string>("AssetId");

                    b.Property<bool>("IsTypeCrypto");

                    b.Property<string>("Name");

                    b.HasKey("AssetId", "IsTypeCrypto");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("CurrencyConverter.Data.Models.DataModels.ExchangeRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssetBaseAssetId");

                    b.Property<string>("AssetBaseId");

                    b.Property<bool?>("AssetBaseIsTypeCrypto");

                    b.Property<string>("AssetQuoteAssetId");

                    b.Property<string>("AssetQuoteId");

                    b.Property<bool?>("AssetQuoteIsTypeCrypto");

                    b.Property<decimal>("Rate");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("AssetBaseAssetId", "AssetBaseIsTypeCrypto");

                    b.HasIndex("AssetQuoteAssetId", "AssetQuoteIsTypeCrypto");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("CurrencyConverter.Data.Models.DataModels.ExchangeRate", b =>
                {
                    b.HasOne("CurrencyConverter.Data.Models.DataModels.Asset", "AssetBase")
                        .WithMany("ExchangeRatesBase")
                        .HasForeignKey("AssetBaseAssetId", "AssetBaseIsTypeCrypto");

                    b.HasOne("CurrencyConverter.Data.Models.DataModels.Asset", "AssetQuote")
                        .WithMany("ExchangeRatesQuote")
                        .HasForeignKey("AssetQuoteAssetId", "AssetQuoteIsTypeCrypto");
                });
#pragma warning restore 612, 618
        }
    }
}
