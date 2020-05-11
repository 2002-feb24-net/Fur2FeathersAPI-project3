using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Furs2Feathers.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    address_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    city = table.Column<string>(maxLength: 100, nullable: true),
                    street = table.Column<string>(maxLength: 100, nullable: true),
                    state = table.Column<string>(maxLength: 100, nullable: true),
                    zip = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.address_id);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    emp_id = table.Column<int>(nullable: false),
                    first_name = table.Column<string>(maxLength: 50, nullable: false),
                    last_name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employee_pkey", x => x.emp_id);
                });

            migrationBuilder.CreateTable(
                name: "pet",
                columns: table => new
                {
                    pet_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    img_url = table.Column<string>(type: "character varying", nullable: true),
                    description = table.Column<string>(type: "character varying", nullable: true),
                    species = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pet", x => x.pet_id);
                });

            migrationBuilder.CreateTable(
                name: "plan",
                columns: table => new
                {
                    plan_id = table.Column<int>(nullable: false),
                    est_cost = table.Column<decimal>(type: "money", nullable: true),
                    positives_max = table.Column<short>(nullable: true),
                    description = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plan", x => x.plan_id);
                });

            migrationBuilder.CreateTable(
                name: "policies",
                columns: table => new
                {
                    policy_id = table.Column<int>(nullable: false),
                    deductible = table.Column<decimal>(type: "money", nullable: false),
                    premium = table.Column<decimal>(type: "money", nullable: false),
                    pet_id = table.Column<int>(nullable: true),
                    renewal_date = table.Column<DateTime>(type: "date", nullable: true),
                    emp_id = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("policies_pkey", x => x.policy_id);
                    table.ForeignKey(
                        name: "policy_emp",
                        column: x => x.emp_id,
                        principalTable: "employee",
                        principalColumn: "emp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "policy_pet",
                        column: x => x.pet_id,
                        principalTable: "pet",
                        principalColumn: "pet_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "plan_pro_labels",
                columns: table => new
                {
                    plan_pro_labels_id = table.Column<int>(nullable: false),
                    labels = table.Column<string>(type: "char", nullable: true),
                    plan_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plan_pro_labels", x => x.plan_pro_labels_id);
                    table.ForeignKey(
                        name: "plan_pro_labels-plan",
                        column: x => x.plan_id,
                        principalTable: "plan",
                        principalColumn: "plan_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "claims",
                columns: table => new
                {
                    claim_id = table.Column<int>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: false),
                    policy_id = table.Column<int>(nullable: false),
                    filing_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("claims_pkey", x => x.claim_id);
                    table.ForeignKey(
                        name: "claim_policy",
                        column: x => x.policy_id,
                        principalTable: "policies",
                        principalColumn: "policy_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customer_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    policies = table.Column<int>(nullable: true),
                    address = table.Column<int>(nullable: true),
                    email = table.Column<string>(maxLength: 100, nullable: false),
                    phone = table.Column<string>(maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.customer_id);
                    table.ForeignKey(
                        name: "cust_addr",
                        column: x => x.address,
                        principalTable: "address",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "cust_policies",
                        column: x => x.policies,
                        principalTable: "policies",
                        principalColumn: "policy_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice",
                columns: table => new
                {
                    invoice_id = table.Column<int>(nullable: false),
                    cost = table.Column<decimal>(type: "money", nullable: false),
                    customer_id = table.Column<int>(nullable: false),
                    pet_id = table.Column<int>(nullable: false),
                    notes = table.Column<string>(type: "character varying", nullable: true),
                    emp_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice", x => x.invoice_id);
                    table.ForeignKey(
                        name: "invoice_cust",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "invoice_emp",
                        column: x => x.emp_id,
                        principalTable: "employee",
                        principalColumn: "emp_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "invoice_pet",
                        column: x => x.pet_id,
                        principalTable: "pet",
                        principalColumn: "pet_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "plan_reviews",
                columns: table => new
                {
                    plan_review_id = table.Column<int>(nullable: false),
                    review = table.Column<string>(maxLength: 1000, nullable: false),
                    customer_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("plan_reviews_pkey", x => x.plan_review_id);
                    table.ForeignKey(
                        name: "plan_reviews-cust",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_claims_policy_id",
                table: "claims",
                column: "policy_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_address",
                table: "customer",
                column: "address");

            migrationBuilder.CreateIndex(
                name: "uq_email",
                table: "customer",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uq_phone",
                table: "customer",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fki_cust_policies",
                table: "customer",
                column: "policies");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_customer_id",
                table: "invoice",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "fki_invoice_emp",
                table: "invoice",
                column: "emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_pet_id",
                table: "invoice",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "IX_plan_pro_labels_plan_id",
                table: "plan_pro_labels",
                column: "plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_plan_reviews_customer_id",
                table: "plan_reviews",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_policies_emp_id",
                table: "policies",
                column: "emp_id");

            migrationBuilder.CreateIndex(
                name: "fki_policy_pet",
                table: "policies",
                column: "pet_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "claims");

            migrationBuilder.DropTable(
                name: "invoice");

            migrationBuilder.DropTable(
                name: "plan_pro_labels");

            migrationBuilder.DropTable(
                name: "plan_reviews");

            migrationBuilder.DropTable(
                name: "plan");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "policies");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "pet");
        }
    }
}
