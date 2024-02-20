using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MG_Admin_GUI.Models;

public partial class MenugeniusContext : DbContext
{
    public MenugeniusContext()
    {
    }

    public MenugeniusContext(DbContextOptions<MenugeniusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Allergen> Allergens { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Desk> Desks { get; set; }

    public virtual DbSet<EventLog> EventLogs { get; set; }

    public virtual DbSet<FailedJob> FailedJobs { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientAllergen> IngredientAllergens { get; set; }

    public virtual DbSet<Migration> Migrations { get; set; }

    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    public virtual DbSet<PersonalAccessToken> PersonalAccessTokens { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductIngredient> ProductIngredients { get; set; }

    public virtual DbSet<ProductPurchase> ProductPurchases { get; set; }

    public virtual DbSet<ProductUser> ProductUsers { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=menugenius;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Allergen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("allergens")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.Code, "allergens_code_unique").IsUnique();

            entity.HasIndex(e => e.Name, "allergens_name_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasPrecision(8, 2)
                .HasColumnName("code");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("categories")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.Name, "categories_name_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Desk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("desks")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.NumberOfSeats)
                .HasColumnType("int(11)")
                .HasColumnName("number_of_seats");
        });

        modelBuilder.Entity<EventLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("event_logs")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.UserId, "event_logs_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("date_time");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.EventType)
                .HasMaxLength(255)
                .HasColumnName("event_type");
            entity.Property(e => e.Route)
                .HasMaxLength(255)
                .HasColumnName("route");
            entity.Property(e => e.UserId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.EventLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("event_logs_user_id_foreign");
        });

        modelBuilder.Entity<FailedJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("failed_jobs")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.Uuid, "failed_jobs_uuid_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Connection)
                .HasColumnType("text")
                .HasColumnName("connection");
            entity.Property(e => e.Exception).HasColumnName("exception");
            entity.Property(e => e.FailedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("failed_at");
            entity.Property(e => e.Payload).HasColumnName("payload");
            entity.Property(e => e.Queue)
                .HasColumnType("text")
                .HasColumnName("queue");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("images")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.ImgName, "images_img_name_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.ImgData).HasColumnName("img_data");
            entity.Property(e => e.ImgName).HasColumnName("img_name");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("ingredients")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.Name, "ingredients_name_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<IngredientAllergen>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ingredient_allergen")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.AllergenId, "ingredient_allergen_allergen_id_foreign");

            entity.HasIndex(e => e.IngredientId, "ingredient_allergen_ingredient_id_foreign");

            entity.Property(e => e.AllergenId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("allergen_id");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.IngredientId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("ingredient_id");

            entity.HasOne(d => d.Allergen).WithMany()
                .HasForeignKey(d => d.AllergenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_allergen_allergen_id_foreign");

            entity.HasOne(d => d.Ingredient).WithMany()
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_allergen_ingredient_id_foreign");
        });

        modelBuilder.Entity<Migration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("migrations")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Batch)
                .HasColumnType("int(11)")
                .HasColumnName("batch");
            entity.Property(e => e.Migration1)
                .HasMaxLength(255)
                .HasColumnName("migration");
        });

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PRIMARY");

            entity
                .ToTable("password_reset_tokens")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .HasColumnName("token");
        });

        modelBuilder.Entity<PersonalAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("personal_access_tokens")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.Token, "personal_access_tokens_token_unique").IsUnique();

            entity.HasIndex(e => new { e.TokenableType, e.TokenableId }, "personal_access_tokens_tokenable_type_tokenable_id_index");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Abilities)
                .HasColumnType("text")
                .HasColumnName("abilities");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp")
                .HasColumnName("expires_at");
            entity.Property(e => e.LastUsedAt)
                .HasColumnType("timestamp")
                .HasColumnName("last_used_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Token)
                .HasMaxLength(64)
                .HasColumnName("token");
            entity.Property(e => e.TokenableId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("tokenable_id");
            entity.Property(e => e.TokenableType).HasColumnName("tokenable_type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("products")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.CategoryId, "products_category_id_foreign");

            entity.HasIndex(e => e.ImageId, "products_image_id_foreign");

            entity.HasIndex(e => e.Name, "products_name_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CategoryId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("category_id");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.ImageId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("image_id");
            entity.Property(e => e.IsFood).HasColumnName("is_food");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Packing)
                .HasMaxLength(255)
                .HasColumnName("packing");
            entity.Property(e => e.Price)
                .HasColumnType("int(11)")
                .HasColumnName("price");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_category_id_foreign");

            entity.HasOne(d => d.Image).WithMany(p => p.Products)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_image_id_foreign");
        });

        modelBuilder.Entity<ProductIngredient>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("product_ingredient")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.IngredientId, "product_ingredient_ingredient_id_foreign");

            entity.HasIndex(e => e.ProductId, "product_ingredient_product_id_foreign");

            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.IngredientId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("ingredient_id");
            entity.Property(e => e.ProductId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("product_id");

            entity.HasOne(d => d.Ingredient).WithMany()
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ingredient_ingredient_id_foreign");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ingredient_product_id_foreign");
        });

        modelBuilder.Entity<ProductPurchase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("product_purchase")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.ProductId, "product_purchase_product_id_foreign");

            entity.HasIndex(e => e.PurchaseId, "product_purchase_purchase_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.ProductId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("product_id");
            entity.Property(e => e.PurchaseId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("purchase_id");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPurchases)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_purchase_product_id_foreign");

            entity.HasOne(d => d.Purchase).WithMany(p => p.ProductPurchases)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("product_purchase_purchase_id_foreign");
        });

        modelBuilder.Entity<ProductUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("product_user")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.ProductId, "product_user_product_id_foreign");

            entity.HasIndex(e => e.UserId, "product_user_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Favorite).HasColumnName("favorite");
            entity.Property(e => e.ProductId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("product_id");
            entity.Property(e => e.Stars)
                .HasDefaultValueSql("'5'")
                .HasColumnType("int(11)")
                .HasColumnName("stars");
            entity.Property(e => e.UserId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductUsers)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_user_product_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.ProductUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("product_user_user_id_foreign");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("purchases")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.DeskId, "purchases_desk_id_foreign");

            entity.HasIndex(e => e.UserId, "purchases_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("date_time");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeskId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("desk_id");
            entity.Property(e => e.Paid).HasColumnName("paid");
            entity.Property(e => e.Status)
                .HasColumnType("enum('ordered','cooked','served')")
                .HasColumnName("status");
            entity.Property(e => e.StripeId)
                .HasMaxLength(255)
                .HasColumnName("stripe_id");
            entity.Property(e => e.TotalPay)
                .HasColumnType("int(11)")
                .HasColumnName("total_pay");
            entity.Property(e => e.UserId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Desk).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.DeskId)
                .HasConstraintName("purchases_desk_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("purchases_user_id_foreign");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("reservations")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.DeskId, "reservations_desk_id_foreign");

            entity.HasIndex(e => e.UserId, "reservations_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CheckinDate)
                .HasColumnType("datetime")
                .HasColumnName("checkin_date");
            entity.Property(e => e.CheckoutDate)
                .HasColumnType("datetime")
                .HasColumnName("checkout_date");
            entity.Property(e => e.Closed).HasColumnName("closed");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DeskId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("desk_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NumberOfGuests)
                .HasColumnType("int(11)")
                .HasColumnName("number_of_guests");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.UserId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Desk).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.DeskId)
                .HasConstraintName("reservations_desk_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("reservations_user_id_foreign");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("users")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.Email, "users_email_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Admin).HasColumnName("admin");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailVerifiedAt)
                .HasColumnType("timestamp")
                .HasColumnName("email_verified_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("'guest'")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.RememberToken)
                .HasMaxLength(100)
                .HasColumnName("remember_token");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
