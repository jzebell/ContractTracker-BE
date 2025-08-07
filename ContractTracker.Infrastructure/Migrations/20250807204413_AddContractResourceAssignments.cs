using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContractResourceAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractLCATs_LCATs_LCATId",
                table: "ContractLCATs");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractResources_Resources_ResourceId",
                table: "ContractResources");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Contracts_ContractId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_LCATs_LCATId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_ContractId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "AnnualSalary",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "FixedPriceAmount",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "FixedPriceHours",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ContractName",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractType",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "PrimeContractor",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "StandardFullTimeHours",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "FixedMonthlyAmount",
                table: "ContractResources");

            migrationBuilder.DropColumn(
                name: "ModificationNumber",
                table: "ContractModifications");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ContractModifications");

            migrationBuilder.DropColumn(
                name: "ContractBillRate",
                table: "ContractLCATs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ContractLCATs");

            migrationBuilder.DropColumn(
                name: "Justification",
                table: "ContractLCATs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ContractLCATs");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Resources",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Resources",
                newName: "ClearanceExpirationDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Contracts",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Contracts",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ContractResources",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ContractResources",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ContractModifications",
                newName: "ModificationDate");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Resources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "LCATId",
                table: "Resources",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Resources",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Resources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Resources",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Resources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ClearanceLevel",
                table: "Resources",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPayRate",
                table: "Resources",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Resources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Resources",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LCATs",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "LCATs",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "LCATs",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LCATs",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "LCATs",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalValue",
                table: "Contracts",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Contracts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "FundedValue",
                table: "Contracts",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Contracts",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Contracts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContractNumber",
                table: "Contracts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<decimal>(
                name: "ActualBurnedAmount",
                table: "Contracts",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedAnnualBurn",
                table: "Contracts",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedMonthlyBurn",
                table: "Contracts",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCalculatedDate",
                table: "Contracts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Contracts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimeContractorName",
                table: "Contracts",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "StandardFTEHours",
                table: "Contracts",
                type: "numeric(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Contracts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualHours",
                table: "ContractResources",
                type: "numeric(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "AllocationPercentage",
                table: "ContractResources",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<decimal>(
                name: "ContractBillRateOverride",
                table: "ContractResources",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ContractResources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ContractResources",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ContractResources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "PreviousValue",
                table: "ContractModifications",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NewValue",
                table: "ContractModifications",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Justification",
                table: "ContractModifications",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ContractModifications",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OverrideBillRate",
                table: "ContractLCATs",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Email",
                table: "Resources",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_IsActive",
                table: "Resources",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Name",
                table: "Resources",
                columns: new[] { "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_LCATs_IsActive",
                table: "LCATs",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_LCATs_Name",
                table: "LCATs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CustomerName",
                table: "Contracts",
                column: "CustomerName");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_Dates",
                table: "Contracts",
                columns: new[] { "StartDate", "EndDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_Status",
                table: "Contracts",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ContractResources_Contract_Resource_Active",
                table: "ContractResources",
                columns: new[] { "ContractId", "ResourceId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "UX_ContractResources_ActiveAssignment",
                table: "ContractResources",
                columns: new[] { "ContractId", "ResourceId" },
                unique: true,
                filter: "\"IsActive\" = true");

            migrationBuilder.CreateIndex(
                name: "IX_ContractModifications_Date",
                table: "ContractModifications",
                column: "ModificationDate");

            migrationBuilder.CreateIndex(
                name: "IX_ContractLCATs_Contract_LCAT_Effective",
                table: "ContractLCATs",
                columns: new[] { "ContractId", "LCATId", "EffectiveDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_ContractLCATs_LCATs_LCATId",
                table: "ContractLCATs",
                column: "LCATId",
                principalTable: "LCATs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractResources_Resources_ResourceId",
                table: "ContractResources",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_LCATs_LCATId",
                table: "Resources",
                column: "LCATId",
                principalTable: "LCATs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractLCATs_LCATs_LCATId",
                table: "ContractLCATs");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractResources_Resources_ResourceId",
                table: "ContractResources");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_LCATs_LCATId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_Email",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_IsActive",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_Name",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_LCATs_IsActive",
                table: "LCATs");

            migrationBuilder.DropIndex(
                name: "IX_LCATs_Name",
                table: "LCATs");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_CustomerName",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_Dates",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_Status",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_ContractResources_Contract_Resource_Active",
                table: "ContractResources");

            migrationBuilder.DropIndex(
                name: "UX_ContractResources_ActiveAssignment",
                table: "ContractResources");

            migrationBuilder.DropIndex(
                name: "IX_ContractModifications_Date",
                table: "ContractModifications");

            migrationBuilder.DropIndex(
                name: "IX_ContractLCATs_Contract_LCAT_Effective",
                table: "ContractLCATs");

            migrationBuilder.DropColumn(
                name: "ClearanceLevel",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "CurrentPayRate",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ActualBurnedAmount",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "EstimatedAnnualBurn",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "EstimatedMonthlyBurn",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LastCalculatedDate",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "PrimeContractorName",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "StandardFTEHours",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractBillRateOverride",
                table: "ContractResources");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ContractResources");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ContractResources");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ContractResources");

            migrationBuilder.DropColumn(
                name: "OverrideBillRate",
                table: "ContractLCATs");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Resources",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "ClearanceExpirationDate",
                table: "Resources",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Contracts",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Contracts",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "ContractResources",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "ContractResources",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ModificationDate",
                table: "ContractModifications",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Resources",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "LCATId",
                table: "Resources",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Resources",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Resources",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Resources",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Resources",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<decimal>(
                name: "AnnualSalary",
                table: "Resources",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContractId",
                table: "Resources",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FixedPriceAmount",
                table: "Resources",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FixedPriceHours",
                table: "Resources",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HourlyRate",
                table: "Resources",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ResourceType",
                table: "Resources",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LCATs",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "LCATs",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "LCATs",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LCATs",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "LCATs",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalValue",
                table: "Contracts",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Contracts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "FundedValue",
                table: "Contracts",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Contracts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Contracts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ContractNumber",
                table: "Contracts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "ContractName",
                table: "Contracts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractType",
                table: "Contracts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Contracts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimeContractor",
                table: "Contracts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "StandardFullTimeHours",
                table: "Contracts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Contracts",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualHours",
                table: "ContractResources",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,2)",
                oldPrecision: 8,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "AllocationPercentage",
                table: "ContractResources",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddColumn<decimal>(
                name: "FixedMonthlyAmount",
                table: "ContractResources",
                type: "numeric",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PreviousValue",
                table: "ContractModifications",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "NewValue",
                table: "ContractModifications",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Justification",
                table: "ContractModifications",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ContractModifications",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "ModificationNumber",
                table: "ContractModifications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ContractModifications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ContractBillRate",
                table: "ContractLCATs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ContractLCATs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Justification",
                table: "ContractLCATs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ContractLCATs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ContractId",
                table: "Resources",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractLCATs_LCATs_LCATId",
                table: "ContractLCATs",
                column: "LCATId",
                principalTable: "LCATs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractResources_Resources_ResourceId",
                table: "ContractResources",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Contracts_ContractId",
                table: "Resources",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_LCATs_LCATId",
                table: "Resources",
                column: "LCATId",
                principalTable: "LCATs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
