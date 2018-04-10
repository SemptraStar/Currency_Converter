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
    [Migration("20180315143353_added-signalr-core")]
    partial class addedsignalrcore
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CurrencyConverter.Api.Data.Models.Currency.Asset", b =>
                {
                    b.Property<string>("AssetId");

                    b.Property<bool>("IsTypeCrypto");

                    b.Property<string>("Name");

                    b.HasKey("AssetId", "IsTypeCrypto");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("CurrencyConverter.Api.Data.Models.Currency.ExchangeRate", b =>
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

            modelBuilder.Entity("CurrencyConverter.Api.Data.Models.Users.Connection", b =>
                {
                    b.Property<string>("ConnectionID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Connected");

                    b.Property<string>("UserAgent");

                    b.Property<string>("UserName");

                    b.HasKey("ConnectionID");

                    b.HasIndex("UserName");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("CurrencyConverter.Api.Data.Models.Users.User", b =>
                {
                    b.Property<string>("UserName")
                        .ValueGeneratedOnAdd();

                    b.HasKey("UserName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CurrencyConverter.Api.Data.Models.Currency.ExchangeRate", b =>
                {
                    b.HasOne("CurrencyConverter.Api.Data.Models.Currency.Asset", "AssetBase")
                        .WithMany("ExchangeRatesBase")
                        .HasForeignKey("AssetBaseAssetId", "AssetBaseIsTypeCrypto");

                    b.HasOne("CurrencyConverter.Api.Data.Models.Currency.Asset", "AssetQuote")
                        .WithMany("ExchangeRatesQuote")
                        .HasForeignKey("AssetQuoteAssetId", "AssetQuoteIsTypeCrypto");
                });

            modelBuilder.Entity("CurrencyConverter.Api.Data.Models.Users.Connection", b =>
                {
                    b.HasOne("CurrencyConverter.Api.Data.Models.Users.User")
                        .WithMany("Connections")
                        .HasForeignKey("UserName");
                });
#pragma warning restore 612, 618
        }
    }
}
