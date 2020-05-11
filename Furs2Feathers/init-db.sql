﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE address (
    address_id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    city character varying(100) NULL,
    street character varying(100) NULL,
    state character varying(100) NULL,
    zip character varying(10) NULL,
    CONSTRAINT "PK_address" PRIMARY KEY (address_id)
);

CREATE TABLE employee (
    emp_id integer NOT NULL,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    CONSTRAINT employee_pkey PRIMARY KEY (emp_id)
);

CREATE TABLE pet (
    pet_id integer NOT NULL,
    name character varying NOT NULL,
    img_url character varying NULL,
    description character varying NULL,
    species character varying NOT NULL,
    CONSTRAINT "PK_pet" PRIMARY KEY (pet_id)
);

CREATE TABLE plan (
    plan_id integer NOT NULL,
    est_cost money NULL,
    positives_max smallint NULL,
    description character varying NULL,
    CONSTRAINT "PK_plan" PRIMARY KEY (plan_id)
);

CREATE TABLE policies (
    policy_id integer NOT NULL,
    deductible money NOT NULL,
    premium money NOT NULL,
    pet_id integer NULL,
    renewal_date date NULL,
    emp_id integer NOT NULL,
    active boolean NOT NULL,
    CONSTRAINT policies_pkey PRIMARY KEY (policy_id),
    CONSTRAINT policy_emp FOREIGN KEY (emp_id) REFERENCES employee (emp_id) ON DELETE RESTRICT,
    CONSTRAINT policy_pet FOREIGN KEY (pet_id) REFERENCES pet (pet_id) ON DELETE RESTRICT
);

CREATE TABLE plan_pro_labels (
    plan_pro_labels_id integer NOT NULL,
    labels char NULL,
    plan_id integer NULL,
    CONSTRAINT "PK_plan_pro_labels" PRIMARY KEY (plan_pro_labels_id),
    CONSTRAINT "plan_pro_labels-plan" FOREIGN KEY (plan_id) REFERENCES plan (plan_id) ON DELETE RESTRICT
);

CREATE TABLE claims (
    claim_id integer NOT NULL,
    description character varying(1000) NOT NULL,
    policy_id integer NOT NULL,
    filing_date date NOT NULL,
    CONSTRAINT claims_pkey PRIMARY KEY (claim_id),
    CONSTRAINT claim_policy FOREIGN KEY (policy_id) REFERENCES policies (policy_id) ON DELETE RESTRICT
);

CREATE TABLE customer (
    customer_id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    policies integer NULL,
    address integer NULL,
    email character varying(100) NOT NULL,
    phone character varying(12) NULL,
    CONSTRAINT "PK_customer" PRIMARY KEY (customer_id),
    CONSTRAINT cust_addr FOREIGN KEY (address) REFERENCES address (address_id) ON DELETE RESTRICT,
    CONSTRAINT cust_policies FOREIGN KEY (policies) REFERENCES policies (policy_id) ON DELETE CASCADE
);

CREATE TABLE invoice (
    invoice_id integer NOT NULL,
    cost money NOT NULL,
    customer_id integer NOT NULL,
    pet_id integer NOT NULL,
    notes character varying NULL,
    emp_id integer NOT NULL,
    CONSTRAINT "PK_invoice" PRIMARY KEY (invoice_id),
    CONSTRAINT invoice_cust FOREIGN KEY (customer_id) REFERENCES customer (customer_id) ON DELETE RESTRICT,
    CONSTRAINT invoice_emp FOREIGN KEY (emp_id) REFERENCES employee (emp_id) ON DELETE RESTRICT,
    CONSTRAINT invoice_pet FOREIGN KEY (pet_id) REFERENCES pet (pet_id) ON DELETE RESTRICT
);

CREATE TABLE plan_reviews (
    plan_review_id integer NOT NULL,
    review character varying(1000) NOT NULL,
    customer_id integer NOT NULL,
    CONSTRAINT plan_reviews_pkey PRIMARY KEY (plan_review_id),
    CONSTRAINT "plan_reviews-cust" FOREIGN KEY (customer_id) REFERENCES customer (customer_id) ON DELETE RESTRICT
);

CREATE INDEX "IX_claims_policy_id" ON claims (policy_id);

CREATE INDEX "IX_customer_address" ON customer (address);

CREATE UNIQUE INDEX uq_email ON customer (email);

CREATE UNIQUE INDEX uq_phone ON customer (phone);

CREATE INDEX fki_cust_policies ON customer (policies);

CREATE INDEX "IX_invoice_customer_id" ON invoice (customer_id);

CREATE INDEX fki_invoice_emp ON invoice (emp_id);

CREATE INDEX "IX_invoice_pet_id" ON invoice (pet_id);

CREATE INDEX "IX_plan_pro_labels_plan_id" ON plan_pro_labels (plan_id);

CREATE INDEX "IX_plan_reviews_customer_id" ON plan_reviews (customer_id);

CREATE INDEX "IX_policies_emp_id" ON policies (emp_id);

CREATE INDEX fki_policy_pet ON policies (pet_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200511041426_initial', '3.1.3');

ALTER TABLE plan_pro_labels ALTER COLUMN plan_pro_labels_id TYPE integer;
ALTER TABLE plan_pro_labels ALTER COLUMN plan_pro_labels_id SET NOT NULL;
ALTER TABLE plan_pro_labels ALTER COLUMN plan_pro_labels_id ADD GENERATED BY DEFAULT AS IDENTITY;

ALTER TABLE plan ALTER COLUMN plan_id TYPE integer;
ALTER TABLE plan ALTER COLUMN plan_id SET NOT NULL;
ALTER TABLE plan ALTER COLUMN plan_id ADD GENERATED BY DEFAULT AS IDENTITY;

ALTER TABLE pet ALTER COLUMN pet_id TYPE integer;
ALTER TABLE pet ALTER COLUMN pet_id SET NOT NULL;
ALTER TABLE pet ALTER COLUMN pet_id ADD GENERATED BY DEFAULT AS IDENTITY;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200511042323_val-gen', '3.1.3');

ALTER TABLE pet ADD age integer NOT NULL DEFAULT 0;

ALTER TABLE pet ADD sex character varying NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200511043802_asdsasa', '3.1.3');

