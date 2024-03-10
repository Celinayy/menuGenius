using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MG_Admin_GUI.Models;

public partial class menugeniusContext : DbContext
{
    public menugeniusContext()
    {
    }

    public menugeniusContext(DbContextOptions<menugeniusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<allergen> allergens { get; set; }

    public virtual DbSet<category> categories { get; set; }

    public virtual DbSet<desk> desks { get; set; }

    public virtual DbSet<event_log> event_logs { get; set; }

    public virtual DbSet<failed_job> failed_jobs { get; set; }

    public virtual DbSet<image> images { get; set; }

    public virtual DbSet<ingredient> ingredients { get; set; }

    public virtual DbSet<ingredient_allergen> ingredient_allergens { get; set; }

    public virtual DbSet<migration> migrations { get; set; }

    public virtual DbSet<password_reset_token> password_reset_tokens { get; set; }

    public virtual DbSet<personal_access_token> personal_access_tokens { get; set; }

    public virtual DbSet<product> products { get; set; }

    public virtual DbSet<product_ingredient> product_ingredients { get; set; }

    public virtual DbSet<product_purchase> product_purchases { get; set; }

    public virtual DbSet<product_user> product_users { get; set; }

    public virtual DbSet<purchase> purchases { get; set; }

    public virtual DbSet<reservation> reservations { get; set; }

    public virtual DbSet<user> users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=menugenius;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<allergen>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.code, "allergens_code_unique").IsUnique();

            entity.HasIndex(e => e.name, "allergens_name_unique").IsUnique();

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.code).HasPrecision(8, 2);
            entity.Property(e => e.deleted_at).HasColumnType("timestamp");

            entity.HasMany(e => e.ingredients)
                .WithMany(e => e.allergens)
                .UsingEntity<ingredient_allergen>(
                    l => l.HasOne<ingredient>().WithMany(e => e.ingredient_allergens).HasForeignKey(e => e.ingredient_id),
                    r => r.HasOne<allergen>().WithMany(e => e.ingredient_allergens).HasForeignKey(e => e.allergen_id)
                );

        });

        modelBuilder.Entity<category>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.name, "categories_name_unique").IsUnique();

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp");
        });

        modelBuilder.Entity<desk>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp");
            entity.Property(e => e.number_of_seats).HasColumnType("int(11)");
        });

        modelBuilder.Entity<event_log>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.user_id, "event_logs_user_id_foreign");

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.body).HasColumnType("text");
            entity.Property(e => e.date_time).HasColumnType("datetime");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp");
            entity.Property(e => e.event_type).HasMaxLength(255);
            entity.Property(e => e.route).HasMaxLength(255);
            entity.Property(e => e.user_id).HasColumnType("bigint(20) unsigned");

            entity.HasOne(d => d.user).WithMany(p => p.event_logs)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("event_logs_user_id_foreign");
        });

        modelBuilder.Entity<failed_job>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.uuid, "failed_jobs_uuid_unique").IsUnique();

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.connection).HasColumnType("text");
            entity.Property(e => e.failed_at)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp");
            entity.Property(e => e.queue).HasColumnType("text");
        });

        modelBuilder.Entity<image>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.img_name, "images_img_name_unique").IsUnique();

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp");
        });

        modelBuilder.Entity<ingredient>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.name, "ingredients_name_unique").IsUnique();

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp");

            entity.HasMany(e => e.products)
                .WithMany(e => e.ingredients)
                .UsingEntity<product_ingredient>(
                    l => l.HasOne<product>().WithMany(e => e.product_ingredients).HasForeignKey(e => e.product_id),
                    r => r.HasOne<ingredient>().WithMany(e => e.product_ingredients).HasForeignKey(e => e.ingredient_id)
                );

            entity.HasMany(e => e.allergens)
                .WithMany(e => e.ingredients)
                .UsingEntity<ingredient_allergen>(
                    l => l.HasOne<allergen>().WithMany(e => e.ingredient_allergens).HasForeignKey(e => e.allergen_id),
                    r => r.HasOne<ingredient>().WithMany(e => e.ingredient_allergens).HasForeignKey(e => e.ingredient_id)
                );


        });

        modelBuilder.Entity<ingredient_allergen>(entity =>
        {
            entity.HasKey(ia => new { ia.ingredient_id, ia.allergen_id });
            entity
                //.HasNoKey()
                .ToTable("ingredient_allergen")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.allergen_id, "ingredient_allergen_allergen_id_foreign");

            entity.HasIndex(e => e.ingredient_id, "ingredient_allergen_ingredient_id_foreign");

            entity.Property(e => e.allergen_id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp");
            entity.Property(e => e.ingredient_id).HasColumnType("bigint(20) unsigned");

            entity.HasOne(d => d.allergen).WithMany()
                .HasForeignKey(d => d.allergen_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_allergen_allergen_id_foreign");

            entity.HasOne(d => d.ingredient).WithMany()
                .HasForeignKey(d => d.ingredient_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_allergen_ingredient_id_foreign");
        });

        modelBuilder.Entity<migration>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.Property(e => e.id).HasColumnType("int(10) unsigned");
            entity.Property(e => e.batch).HasColumnType("int(11)");
            entity.Property(e => e.migration1)
                .HasMaxLength(255)
                .HasColumnName("migration");
        });

        modelBuilder.Entity<password_reset_token>(entity =>
        {
            entity.HasKey(e => e.email).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.token).HasMaxLength(255);
        });

        modelBuilder.Entity<personal_access_token>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.token, "personal_access_tokens_token_unique").IsUnique();

            entity.HasIndex(e => new { e.tokenable_type, e.tokenable_id }, "personal_access_tokens_tokenable_type_tokenable_id_index");

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.abilities).HasColumnType("text");
            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.expires_at).HasColumnType("timestamp");
            entity.Property(e => e.last_used_at).HasColumnType("timestamp");
            entity.Property(e => e.name).HasMaxLength(255);
            entity.Property(e => e.token).HasMaxLength(64);
            entity.Property(e => e.tokenable_id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.updated_at).HasColumnType("timestamp");
        });

        modelBuilder.Entity<product>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.category_id, "products_category_id_foreign");

            entity.HasIndex(e => e.image_id, "products_image_id_foreign");

            entity.HasIndex(e => e.name, "products_name_unique").IsUnique();

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.category_id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp");
            entity.Property(e => e.description).HasMaxLength(255);
            entity.Property(e => e.image_id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.packing).HasMaxLength(255);
            entity.Property(e => e.price).HasColumnType("int(11)");

            entity.HasOne(d => d.category).WithMany(p => p.products)
                .HasForeignKey(d => d.category_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_category_id_foreign");

            entity.HasOne(d => d.image).WithMany(p => p.products)
                .HasForeignKey(d => d.image_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_image_id_foreign");

            entity.HasMany(e => e.ingredients)
                .WithMany(e => e.products)
                .UsingEntity<product_ingredient>(
                    l => l.HasOne<ingredient>().WithMany(e => e.product_ingredients).HasForeignKey(e => e.ingredient_id),
                    r => r.HasOne<product>().WithMany(e => e.product_ingredients).HasForeignKey(e => e.product_id)
                );
        });

        modelBuilder.Entity<product_ingredient>(entity =>
        {
            entity.HasKey(pi => new { pi.product_id, pi.ingredient_id });
            entity
                //.HasNoKey()
                .ToTable("product_ingredient")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.ingredient_id, "product_ingredient_ingredient_id_foreign");

            entity.HasIndex(e => e.product_id, "product_ingredient_product_id_foreign");

            entity.Property(e => e.deleted_at).HasColumnType("timestamp");
            entity.Property(e => e.ingredient_id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.product_id).HasColumnType("bigint(20) unsigned");

            entity.HasOne(d => d.ingredient).WithMany()
                .HasForeignKey(d => d.ingredient_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ingredient_ingredient_id_foreign");

            entity.HasOne(d => d.product).WithMany()
                .HasForeignKey(d => d.product_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ingredient_product_id_foreign");
        });

        modelBuilder.Entity<product_purchase>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity
                .ToTable("product_purchase")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.product_id, "product_purchase_product_id_foreign");

            entity.HasIndex(e => e.purchase_id, "product_purchase_purchase_id_foreign");

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp");
            entity.Property(e => e.product_id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.purchase_id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.quantity).HasColumnType("int(11)");

            entity.HasOne(d => d.product).WithMany(p => p.product_purchases)
                .HasForeignKey(d => d.product_id)
                .HasConstraintName("product_purchase_product_id_foreign");

            entity.HasOne(d => d.purchase).WithMany(p => p.product_purchases)
                .HasForeignKey(d => d.purchase_id)
                .HasConstraintName("product_purchase_purchase_id_foreign");
        });

        modelBuilder.Entity<product_user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity
                .ToTable("product_user")
                .UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.product_id, "product_user_product_id_foreign");

            entity.HasIndex(e => e.user_id, "product_user_user_id_foreign");

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp");
            entity.Property(e => e.product_id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.stars)
                .HasDefaultValueSql("'5'")
                .HasColumnType("int(11)");
            entity.Property(e => e.user_id).HasColumnType("bigint(20) unsigned");

            entity.HasOne(d => d.product).WithMany(p => p.product_users)
                .HasForeignKey(d => d.product_id)
                .HasConstraintName("product_user_product_id_foreign");

            entity.HasOne(d => d.user).WithMany(p => p.product_users)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("product_user_user_id_foreign");
        });

        modelBuilder.Entity<purchase>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.desk_id, "purchases_desk_id_foreign");

            entity.HasIndex(e => e.user_id, "purchases_user_id_foreign");

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.date_time).HasColumnType("datetime");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp");
            entity.Property(e => e.desk_id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.status).HasColumnType("enum('ordered','cooked','served')");
            entity.Property(e => e.stripe_id).HasMaxLength(255);
            entity.Property(e => e.total_pay).HasColumnType("int(11)");
            entity.Property(e => e.user_id).HasColumnType("bigint(20) unsigned");

            entity.HasOne(d => d.desk).WithMany(p => p.purchases)
                .HasForeignKey(d => d.desk_id)
                .HasConstraintName("purchases_desk_id_foreign");

            entity.HasOne(d => d.user).WithMany(p => p.purchases)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("purchases_user_id_foreign");
        });

        modelBuilder.Entity<reservation>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.desk_id, "reservations_desk_id_foreign");

            entity.HasIndex(e => e.user_id, "reservations_user_id_foreign");

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.checkin_date).HasColumnType("datetime");
            entity.Property(e => e.checkout_date).HasColumnType("datetime");
            entity.Property(e => e.comment).HasColumnType("text");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp");
            entity.Property(e => e.desk_id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.name).HasMaxLength(255);
            entity.Property(e => e.number_of_guests).HasColumnType("int(11)");
            entity.Property(e => e.phone).HasMaxLength(255);
            entity.Property(e => e.user_id).HasColumnType("bigint(20) unsigned");

            entity.HasOne(d => d.desk).WithMany(p => p.reservations)
                .HasForeignKey(d => d.desk_id)
                .HasConstraintName("reservations_desk_id_foreign");

            entity.HasOne(d => d.user).WithMany(p => p.reservations)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("reservations_user_id_foreign");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.UseCollation("utf8mb4_hungarian_ci");

            entity.HasIndex(e => e.email, "users_email_unique").IsUnique();

            entity.Property(e => e.id).HasColumnType("bigint(20) unsigned");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp");
            entity.Property(e => e.email_verified_at).HasColumnType("timestamp");
            entity.Property(e => e.name)
                .HasMaxLength(255)
                .HasDefaultValueSql("'guest'");
            entity.Property(e => e.password).HasMaxLength(255);
            entity.Property(e => e.phone).HasMaxLength(255);
            entity.Property(e => e.remember_token).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
