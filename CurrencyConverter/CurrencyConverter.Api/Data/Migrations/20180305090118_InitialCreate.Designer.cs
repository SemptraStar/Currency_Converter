﻿// <auto-generated />
using CurrencyConverter.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CurrencyConverter.Api.Data.Migrations
{
    [DbContext(typeof(CurrencyContext))]
    [Migration("20180305090118_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CurrencyConverter.Api.Data.Models.DataModels.Asset", b =>
                {
                    b.Property<string>("AssetId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsTypeCrypto");

                    b.Property<string>("Name");

                    b.HasKey("AssetId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("CurrencyConverter.Api.Data.Models.DataModels.ExchangeRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssetBaseAssetId");

                    b.Property<string>("AssetQuoteAssetId");

                    b.Property<decimal>("Rate");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("AssetBaseAssetId");

                    b.HasIndex("AssetQuoteAssetId");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("CurrencyConverter.Api.Data.Models.DataModels.ExchangeRate", b =>
                {
                    b.HasOne("CurrencyConverter.Api.Data.Models.DataModels.Asset", "AssetBase")
                        .WithMany("ExchangeRatesBase")
                        .HasForeignKey("AssetBaseAssetId");

                    b.HasOne("CurrencyConverter.Api.Data.Models.DataModels.Asset", "AssetQuote")
                        .WithMany("ExchangeRatesQuote")
                        .HasForeignKey("AssetQuoteAssetId");
                });
#pragma warning restore 612, 618
        }
    }
}
