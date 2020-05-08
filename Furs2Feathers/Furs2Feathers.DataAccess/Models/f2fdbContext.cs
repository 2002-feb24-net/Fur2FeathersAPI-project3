using System;
using Microsoft.EntityFrameworkCore;

namespace Furs2Feathers.DataAccess.Models
{
    public partial class f2fdbContext : DbContext
    {
        

        public f2fdbContext(DbContextOptions<f2fdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Claims> Claims { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<Pet> Pet { get; set; }
        public virtual DbSet<Plan> Plan { get; set; }
        public virtual DbSet<PlanProLabels> PlanProLabels { get; set; }
        public virtual DbSet<PlanReviews> PlanReviews { get; set; }
        public virtual DbSet<Policies> Policies { get; set; }

  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.Property(e => e.AddressId)
                    .HasColumnName("address_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(100);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(100);

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasMaxLength(100);

                entity.Property(e => e.Zip)
                    .HasColumnName("zip")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Claims>(entity =>
            {
                entity.HasKey(e => e.ClaimId)
                    .HasName("claims_pkey");

                entity.ToTable("claims");

                entity.Property(e => e.ClaimId)
                    .HasColumnName("claim_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(1000);

                entity.Property(e => e.FilingDate)
                    .HasColumnName("filing_date")
                    .HasColumnType("date");

                entity.Property(e => e.PolicyId).HasColumnName("policy_id");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.PolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("claim_policy");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.Email)
                    .HasName("uq_email")
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .HasName("uq_phone")
                    .IsUnique();

                entity.HasIndex(e => e.Policies)
                    .HasName("fki_cust_policies");

                entity.HasIndex(e => e.Username)
                    .HasName("uq_uname")
                    .IsUnique();

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(12);

                entity.Property(e => e.Policies).HasColumnName("policies");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(30);

                entity.HasOne(d => d.AddressNavigation)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.Address)
                    .HasConstraintName("cust_addr");

                entity.HasOne(d => d.PoliciesNavigation)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.Policies)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("cust_policies");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("employee_pkey");

                entity.ToTable("employee");

                entity.Property(e => e.EmpId)
                    .HasColumnName("emp_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("invoice");

                entity.HasIndex(e => e.EmpId)
                    .HasName("fki_invoice_emp");

                entity.Property(e => e.InvoiceId)
                    .HasColumnName("invoice_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasColumnType("money");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("character varying");

                entity.Property(e => e.PetId).HasColumnName("pet_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("invoice_cust");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("invoice_emp");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.PetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("invoice_pet");
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("pet");

                entity.Property(e => e.PetId)
                    .HasColumnName("pet_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("character varying");

                entity.Property(e => e.ImgUrl)
                    .HasColumnName("img_url")
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.Species)
                    .IsRequired()
                    .HasColumnName("species")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("plan");

                entity.Property(e => e.PlanId)
                    .HasColumnName("plan_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("character varying");

                entity.Property(e => e.EstCost)
                    .HasColumnName("est_cost")
                    .HasColumnType("money");

                entity.Property(e => e.PositivesMax).HasColumnName("positives_max");
            });

            modelBuilder.Entity<PlanProLabels>(entity =>
            {
                entity.ToTable("plan_pro_labels");

                entity.Property(e => e.PlanProLabelsId)
                    .HasColumnName("plan_pro_labels_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Labels)
                    .HasColumnName("labels")
                    .HasColumnType("char");

                entity.Property(e => e.PlanId).HasColumnName("plan_id");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.PlanProLabels)
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("plan_pro_labels-plan");
            });

            modelBuilder.Entity<PlanReviews>(entity =>
            {
                entity.HasKey(e => e.PlanReviewId)
                    .HasName("plan_reviews_pkey");

                entity.ToTable("plan_reviews");

                entity.Property(e => e.PlanReviewId)
                    .HasColumnName("plan_review_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Review)
                    .IsRequired()
                    .HasColumnName("review")
                    .HasMaxLength(1000);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.PlanReviews)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("plan_reviews-cust");
            });

            modelBuilder.Entity<Policies>(entity =>
            {
                entity.HasKey(e => e.PolicyId)
                    .HasName("policies_pkey");

                entity.ToTable("policies");

                entity.HasIndex(e => e.PetId)
                    .HasName("fki_policy_pet");

                entity.Property(e => e.PolicyId)
                    .HasColumnName("policy_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Deductible)
                    .HasColumnName("deductible")
                    .HasColumnType("money");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.PetId).HasColumnName("pet_id");

                entity.Property(e => e.Premium)
                    .HasColumnName("premium")
                    .HasColumnType("money");

                entity.Property(e => e.RenewalDate)
                    .HasColumnName("renewal_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Policies)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("policy_emp");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Policies)
                    .HasForeignKey(d => d.PetId)
                    .HasConstraintName("policy_pet");
            });

        }
    }
}