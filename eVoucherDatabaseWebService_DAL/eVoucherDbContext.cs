global using eVoucher_DTO.Models;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;

namespace eVoucher_DAL
{
    public class eVoucherDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public eVoucherDbContext(DbContextOptions options) : base(options)
        {
        }
        // the followed functions are disable because it was declare in eVoucherDatabaseWebservice startup.cs -> configureservices
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("name=ConnectionStrings:DefaultConnection", b => b.MigrationsAssembly("eVoucher_Migrations"));
        }

        protected eVoucherDbContext()
        { }

        public DbSet<PartnerCategory> PartnerCategories { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignGame> CampaignGames { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }
        public DbSet<GamePlayResult> GamePlayResults { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<PartnerImage> PartnerImages { get; set; }
        public DbSet<CampaignImage> CampaignImages { get; set; }
        public DbSet<VoucherTypeImage> VoucherTypeImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
            modelBuilder.Entity<VoucherType>()
                .HasMany(e => e.Vouchers)
                .WithOne(e => e.VoucherType)
                .OnDelete(DeleteBehavior.NoAction);
            //base.OnModelCreating(builder);
        }
    }
}