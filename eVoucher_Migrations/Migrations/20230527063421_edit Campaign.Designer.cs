﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eVoucher_DAL;

#nullable disable

namespace eVoucher_Migrations.Migrations
{
    [DbContext(typeof(eVoucherDbContext))]
    [Migration("20230527063421_edit Campaign")]
    partial class editCampaign
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens", (string)null);
                });

            modelBuilder.Entity("eVoucher_DTO.Models.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRoles");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Campaign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BeginningDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndingDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HomeFlag")
                        .HasColumnType("bit");

                    b.Property<bool>("HotFlag")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MetaDescription")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MetaKeyword")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("PartnerID")
                        .HasColumnType("int");

                    b.Property<string>("Slogan")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PartnerID");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.CampaignGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CampaignID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CampaignID");

                    b.HasIndex("GameID");

                    b.ToTable("CampaignGames");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.CampaignImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CampaignID")
                        .HasColumnType("int");

                    b.Property<string>("Caption")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CampaignID");

                    b.ToTable("CampaignImages");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CampaignChosenCount")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("PlayedCount")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.GamePlayResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AppUserId")
                        .HasColumnType("int");

                    b.Property<int>("CampaignGameID")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GotNumberResult")
                        .HasColumnType("int");

                    b.Property<bool>("IsGotVoucher")
                        .HasColumnType("bit");

                    b.Property<int?>("VoucherTypeID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CampaignGameID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("VoucherTypeID");

                    b.ToTable("GamePlayResults");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Partner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("PartnerCategoryID")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PartnerCategoryID");

                    b.HasIndex("UserID");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.PartnerCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PartnerCategories");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.PartnerImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Caption")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<int>("PartnerID")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PartnerID");

                    b.ToTable("PartnerImages");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Voucher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateGet")
                        .HasColumnType("datetime2");

                    b.Property<int>("GamePlayResultID")
                        .HasColumnType("int");

                    b.Property<int>("VoucherStatus")
                        .HasColumnType("int");

                    b.Property<int?>("VoucherTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GamePlayResultID")
                        .IsUnique();

                    b.HasIndex("VoucherTypeId");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.VoucherType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CampaignID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DiscountRate")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiringDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LuckyNumbers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxAmount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Promotion")
                        .HasColumnType("nvarchar");

                    b.Property<int>("RemainAmount")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CampaignID");

                    b.ToTable("VoucherTypes");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.VoucherTypeImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Caption")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<int>("VoucherTypeID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VoucherTypeID");

                    b.ToTable("VoucherTypeImages");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Campaign", b =>
                {
                    b.HasOne("eVoucher_DTO.Models.Partner", "Partner")
                        .WithMany()
                        .HasForeignKey("PartnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.CampaignGame", b =>
                {
                    b.HasOne("eVoucher_DTO.Models.Campaign", "Campaign")
                        .WithMany("CampaignGames")
                        .HasForeignKey("CampaignID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eVoucher_DTO.Models.Game", "Game")
                        .WithMany("CampaignGames")
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campaign");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.CampaignImage", b =>
                {
                    b.HasOne("eVoucher_DTO.Models.Campaign", "Campaign")
                        .WithMany("CampaignImages")
                        .HasForeignKey("CampaignID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campaign");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Customer", b =>
                {
                    b.HasOne("eVoucher_DTO.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.GamePlayResult", b =>
                {
                    b.HasOne("eVoucher_DTO.Models.AppUser", null)
                        .WithMany("GamePlayResults")
                        .HasForeignKey("AppUserId");

                    b.HasOne("eVoucher_DTO.Models.CampaignGame", "CampaignGame")
                        .WithMany()
                        .HasForeignKey("CampaignGameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eVoucher_DTO.Models.Customer", "Customer")
                        .WithMany("GamePlayResults")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eVoucher_DTO.Models.VoucherType", "VoucherType")
                        .WithMany()
                        .HasForeignKey("VoucherTypeID");

                    b.Navigation("CampaignGame");

                    b.Navigation("Customer");

                    b.Navigation("VoucherType");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Partner", b =>
                {
                    b.HasOne("eVoucher_DTO.Models.PartnerCategory", "Partnercategory")
                        .WithMany("Partners")
                        .HasForeignKey("PartnerCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eVoucher_DTO.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Partnercategory");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.PartnerImage", b =>
                {
                    b.HasOne("eVoucher_DTO.Models.Partner", "Partner")
                        .WithMany("PartnerImages")
                        .HasForeignKey("PartnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Staff", b =>
                {
                    b.HasOne("eVoucher_DTO.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Voucher", b =>
                {
                    b.HasOne("eVoucher_DTO.Models.GamePlayResult", "GamePlayResult")
                        .WithOne("Voucher")
                        .HasForeignKey("eVoucher_DTO.Models.Voucher", "GamePlayResultID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eVoucher_DTO.Models.VoucherType", null)
                        .WithMany("Vouchers")
                        .HasForeignKey("VoucherTypeId");

                    b.Navigation("GamePlayResult");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.VoucherType", b =>
                {
                    b.HasOne("eVoucher_DTO.Models.Campaign", "Campaign")
                        .WithMany()
                        .HasForeignKey("CampaignID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campaign");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.VoucherTypeImage", b =>
                {
                    b.HasOne("eVoucher_DTO.Models.VoucherType", "VoucherType")
                        .WithMany("VoucherTypeImages")
                        .HasForeignKey("VoucherTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VoucherType");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.AppUser", b =>
                {
                    b.Navigation("GamePlayResults");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Campaign", b =>
                {
                    b.Navigation("CampaignGames");

                    b.Navigation("CampaignImages");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Customer", b =>
                {
                    b.Navigation("GamePlayResults");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Game", b =>
                {
                    b.Navigation("CampaignGames");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.GamePlayResult", b =>
                {
                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.Partner", b =>
                {
                    b.Navigation("PartnerImages");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.PartnerCategory", b =>
                {
                    b.Navigation("Partners");
                });

            modelBuilder.Entity("eVoucher_DTO.Models.VoucherType", b =>
                {
                    b.Navigation("VoucherTypeImages");

                    b.Navigation("Vouchers");
                });
#pragma warning restore 612, 618
        }
    }
}