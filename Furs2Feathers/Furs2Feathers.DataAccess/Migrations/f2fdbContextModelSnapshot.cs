﻿// <auto-generated />
using System;
using Furs2Feathers.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Furs2Feathers.DataAccess.Migrations
{
    [DbContext(typeof(f2fdbContext))]
    partial class f2fdbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("address_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("City")
                        .HasColumnName("city")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("State")
                        .HasColumnName("state")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Street")
                        .HasColumnName("street")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Zip")
                        .HasColumnName("zip")
                        .HasColumnType("character varying(10)")
                        .HasMaxLength(10);

                    b.HasKey("AddressId");

                    b.ToTable("address");
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.Claims", b =>
                {
                    b.Property<int>("ClaimId")
                        .HasColumnName("claim_id")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("character varying(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("FilingDate")
                        .HasColumnName("filing_date")
                        .HasColumnType("date");

                    b.Property<int>("PolicyId")
                        .HasColumnName("policy_id")
                        .HasColumnType("integer");

                    b.HasKey("ClaimId")
                        .HasName("claims_pkey");

                    b.HasIndex("PolicyId");

                    b.ToTable("claims");
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("customer_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Address")
                        .HasColumnName("address")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasColumnType("character varying(12)")
                        .HasMaxLength(12);

                    b.Property<int?>("Policies")
                        .HasColumnName("policies")
                        .HasColumnType("integer");

                    b.HasKey("CustomerId");

                    b.HasIndex("Address");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("uq_email");

                    b.HasIndex("Phone")
                        .IsUnique()
                        .HasName("uq_phone");

                    b.HasIndex("Policies")
                        .HasName("fki_cust_policies");

                    b.ToTable("customer");
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.Employee", b =>
                {
                    b.Property<int>("EmpId")
                        .HasColumnName("emp_id")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("EmpId")
                        .HasName("employee_pkey");

                    b.ToTable("employee");
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .HasColumnName("invoice_id")
                        .HasColumnType("integer");

                    b.Property<decimal>("Cost")
                        .HasColumnName("cost")
                        .HasColumnType("money");

                    b.Property<int>("CustomerId")
                        .HasColumnName("customer_id")
                        .HasColumnType("integer");

                    b.Property<int>("EmpId")
                        .HasColumnName("emp_id")
                        .HasColumnType("integer");

                    b.Property<string>("Notes")
                        .HasColumnName("notes")
                        .HasColumnType("character varying");

                    b.Property<int>("PetId")
                        .HasColumnName("pet_id")
                        .HasColumnType("integer");

                    b.HasKey("InvoiceId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmpId")
                        .HasName("fki_invoice_emp");

                    b.HasIndex("PetId");

                    b.ToTable("invoice");
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.Pet", b =>
                {
                    b.Property<int>("PetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("pet_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Age")
                        .HasColumnName("age")
                        .HasColumnType("integer");

                    b.Property<int>("CustomerId")
                        .HasColumnName("customerId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("character varying");

                    b.Property<string>("ImgUrl")
                        .HasColumnName("img_url")
                        .HasColumnType("character varying");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnName("sex")
                        .HasColumnType("character varying");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnName("species")
                        .HasColumnType("character varying");

                    b.HasKey("PetId");

                    b.ToTable("pet");
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.Plan", b =>
                {
                    b.Property<int>("PlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("plan_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("character varying");

                    b.Property<decimal?>("EstCost")
                        .HasColumnName("est_cost")
                        .HasColumnType("money");

                    b.Property<short?>("PositivesMax")
                        .HasColumnName("positives_max")
                        .HasColumnType("smallint");

                    b.HasKey("PlanId");

                    b.ToTable("plan");
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.PlanProLabels", b =>
                {
                    b.Property<int>("PlanProLabelsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("plan_pro_labels_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Labels")
                        .HasColumnName("labels")
                        .HasColumnType("char");

                    b.Property<int?>("PlanId")
                        .HasColumnName("plan_id")
                        .HasColumnType("integer");

                    b.HasKey("PlanProLabelsId");

                    b.HasIndex("PlanId");

                    b.ToTable("plan_pro_labels");
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.PlanReviews", b =>
                {
                    b.Property<int>("PlanReviewId")
                        .HasColumnName("plan_review_id")
                        .HasColumnType("integer");

                    b.Property<int>("CustomerId")
                        .HasColumnName("customer_id")
                        .HasColumnType("integer");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnName("review")
                        .HasColumnType("character varying(1000)")
                        .HasMaxLength(1000);

                    b.HasKey("PlanReviewId")
                        .HasName("plan_reviews_pkey");

                    b.HasIndex("CustomerId");

                    b.ToTable("plan_reviews");
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.Policies", b =>
                {
                    b.Property<int>("PolicyId")
                        .HasColumnName("policy_id")
                        .HasColumnType("integer");

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Deductible")
                        .HasColumnName("deductible")
                        .HasColumnType("money");

                    b.Property<int>("EmpId")
                        .HasColumnName("emp_id")
                        .HasColumnType("integer");

                    b.Property<int?>("PetId")
                        .HasColumnName("pet_id")
                        .HasColumnType("integer");

                    b.Property<decimal>("Premium")
                        .HasColumnName("premium")
                        .HasColumnType("money");

                    b.Property<DateTime?>("RenewalDate")
                        .HasColumnName("renewal_date")
                        .HasColumnType("date");

                    b.HasKey("PolicyId")
                        .HasName("policies_pkey");

                    b.HasIndex("EmpId");

                    b.HasIndex("PetId")
                        .HasName("fki_policy_pet");

                    b.ToTable("policies");
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.Claims", b =>
                {
                    b.HasOne("Furs2Feathers.DataAccess.Models.Policies", "Policy")
                        .WithMany("Claims")
                        .HasForeignKey("PolicyId")
                        .HasConstraintName("claim_policy")
                        .IsRequired();
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.Customer", b =>
                {
                    b.HasOne("Furs2Feathers.DataAccess.Models.Address", "AddressNavigation")
                        .WithMany("Customer")
                        .HasForeignKey("Address")
                        .HasConstraintName("cust_addr");

                    b.HasOne("Furs2Feathers.DataAccess.Models.Policies", "PoliciesNavigation")
                        .WithMany("Customer")
                        .HasForeignKey("Policies")
                        .HasConstraintName("cust_policies")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.Invoice", b =>
                {
                    b.HasOne("Furs2Feathers.DataAccess.Models.Customer", "Customer")
                        .WithMany("Invoice")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("invoice_cust")
                        .IsRequired();

                    b.HasOne("Furs2Feathers.DataAccess.Models.Employee", "Emp")
                        .WithMany("Invoice")
                        .HasForeignKey("EmpId")
                        .HasConstraintName("invoice_emp")
                        .IsRequired();

                    b.HasOne("Furs2Feathers.DataAccess.Models.Pet", "Pet")
                        .WithMany("Invoice")
                        .HasForeignKey("PetId")
                        .HasConstraintName("invoice_pet")
                        .IsRequired();
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.PlanProLabels", b =>
                {
                    b.HasOne("Furs2Feathers.DataAccess.Models.Plan", "Plan")
                        .WithMany("PlanProLabels")
                        .HasForeignKey("PlanId")
                        .HasConstraintName("plan_pro_labels-plan");
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.PlanReviews", b =>
                {
                    b.HasOne("Furs2Feathers.DataAccess.Models.Customer", "Customer")
                        .WithMany("PlanReviews")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("plan_reviews-cust")
                        .IsRequired();
                });

            modelBuilder.Entity("Furs2Feathers.DataAccess.Models.Policies", b =>
                {
                    b.HasOne("Furs2Feathers.DataAccess.Models.Employee", "Emp")
                        .WithMany("Policies")
                        .HasForeignKey("EmpId")
                        .HasConstraintName("policy_emp")
                        .IsRequired();

                    b.HasOne("Furs2Feathers.DataAccess.Models.Pet", "Pet")
                        .WithMany("Policies")
                        .HasForeignKey("PetId")
                        .HasConstraintName("policy_pet");
                });
#pragma warning restore 612, 618
        }
    }
}
