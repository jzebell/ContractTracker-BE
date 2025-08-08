using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityPropertiesToMatchNewStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractResources_Resources_ResourceId",
                table: "ContractResources");

            migrationBuilder.DropForeignKey(
                name: "FK_LCATRate_LCATs_LCATId",
                table: "LCATRate");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionTitle_LCATs_LCATId",
                table: "PositionTitle");

            migrationBuilder.DropTable(
                name: "ContractLCATRate");

            migrationBuilder.DropIndex(
                name: "IX_Resources_Name",
                table: "Resources");

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
                name: "IX_ContractResources_ContractId",
                table: "ContractResources");

            migrationBuilder.DropIndex(
                name: "UX_ContractResources_ActiveAssignment",
                table: "ContractResources");

            migrationBuilder.DropIndex(
                name: "IX_ContractModifications_ContractId",
                table: "ContractModifications");

            migrationBuilder.DropIndex(
                name: "IX_ContractModifications_Date",
                table: "ContractModifications");

            migrationBuilder.DropIndex(
                name: "IX_ContractLCATs_ContractId",
                table: "ContractLCATs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PositionTitle",
                table: "PositionTitle");

            migrationBuilder.DropIndex(
                name: "IX_PositionTitle_LCATId",
                table: "PositionTitle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LCATRate",
                table: "LCATRate");

            migrationBuilder.DropIndex(
                name: "IX_LCATRate_LCATId",
                table: "LCATRate");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "LCATs");

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
                name: "StandardFTEHours",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractBillRateOverride",
                table: "ContractResources");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ContractModifications");

            migrationBuilder.DropColumn(
                name: "OverrideBillRate",
                table: "ContractLCATs");

            migrationBuilder.RenameTable(
                name: "PositionTitle",
                newName: "PositionTitles");

            migrationBuilder.RenameTable(
                name: "LCATRate",
                newName: "LCATRates");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Resources",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CurrentPayRate",
                table: "Resources",
                newName: "PayRate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Resources",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "LCATs",
                newName: "UpdatedAt");

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
                name: "PreviousValue",
                table: "ContractModifications",
                newName: "OldValue");

            migrationBuilder.RenameIndex(
                name: "IX_ContractLCATs_Contract_LCAT_Effective",
                table: "ContractLCATs",
                newName: "IX_ContractLCATs_Contract_LCAT_EffectiveDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "LCATRates",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Resources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Resources",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Resources",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Resources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<decimal>(
                name: "BurdenedCost",
                table: "Resources",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Resources",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Resources",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Resources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Resources",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "LCATs",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System",
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
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "LCATs",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "LCATs",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "LCATs",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "LCATs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Contracts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Contracts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PrimeContractorName",
                table: "Contracts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Contracts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Contracts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Contracts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ContractNumber",
                table: "Contracts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Contracts",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StandardFullTimeHours",
                table: "Contracts",
                type: "integer",
                nullable: false,
                defaultValue: 1912);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "ContractResources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "ContractResources",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ContractResources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualHours",
                table: "ContractResources",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,2)",
                oldPrecision: 8,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Justification",
                table: "ContractModifications",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ContractModifications",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "System");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ContractModifications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ContractLCATs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ContractLCATs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "OverrideRate",
                table: "ContractLCATs",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ContractLCATs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "PositionTitles",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PositionTitles",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "RateType",
                table: "LCATRates",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "LCATRates",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "LCATRates",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "LCATRates",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PositionTitles",
                table: "PositionTitles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LCATRates",
                table: "LCATRates",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LCATs_Code",
                table: "LCATs",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractModifications_Contract_Date",
                table: "ContractModifications",
                columns: new[] { "ContractId", "ModificationDate" });

            migrationBuilder.CreateIndex(
                name: "IX_PositionTitles_LCAT_Title",
                table: "PositionTitles",
                columns: new[] { "LCATId", "Title" });

            migrationBuilder.CreateIndex(
                name: "IX_LCATRates_LCAT_Type_EffectiveDate",
                table: "LCATRates",
                columns: new[] { "LCATId", "RateType", "EffectiveDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_ContractResources_Resources_ResourceId",
                table: "ContractResources",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LCATRates_LCATs_LCATId",
                table: "LCATRates",
                column: "LCATId",
                principalTable: "LCATs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionTitles_LCATs_LCATId",
                table: "PositionTitles",
                column: "LCATId",
                principalTable: "LCATs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractResources_Resources_ResourceId",
                table: "ContractResources");

            migrationBuilder.DropForeignKey(
                name: "FK_LCATRates_LCATs_LCATId",
                table: "LCATRates");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionTitles_LCATs_LCATId",
                table: "PositionTitles");

            migrationBuilder.DropIndex(
                name: "IX_LCATs_Code",
                table: "LCATs");

            migrationBuilder.DropIndex(
                name: "IX_ContractModifications_Contract_Date",
                table: "ContractModifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PositionTitles",
                table: "PositionTitles");

            migrationBuilder.DropIndex(
                name: "IX_PositionTitles_LCAT_Title",
                table: "PositionTitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LCATRates",
                table: "LCATRates");

            migrationBuilder.DropIndex(
                name: "IX_LCATRates_LCAT_Type_EffectiveDate",
                table: "LCATRates");

            migrationBuilder.DropColumn(
                name: "BurdenedCost",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "LCATs");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "LCATs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "LCATs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "StandardFullTimeHours",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ContractModifications");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ContractModifications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ContractLCATs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ContractLCATs");

            migrationBuilder.DropColumn(
                name: "OverrideRate",
                table: "ContractLCATs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ContractLCATs");

            migrationBuilder.RenameTable(
                name: "PositionTitles",
                newName: "PositionTitle");

            migrationBuilder.RenameTable(
                name: "LCATRates",
                newName: "LCATRate");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Resources",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Resources",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "PayRate",
                table: "Resources",
                newName: "CurrentPayRate");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "LCATs",
                newName: "CreatedDate");

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
                name: "OldValue",
                table: "ContractModifications",
                newName: "PreviousValue");

            migrationBuilder.RenameIndex(
                name: "IX_ContractLCATs_Contract_LCAT_EffectiveDate",
                table: "ContractLCATs",
                newName: "IX_ContractLCATs_Contract_LCAT_Effective");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "LCATRate",
                newName: "CreatedDate");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Resources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldDefaultValue: "System");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Resources",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Resources",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Resources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldDefaultValue: "System");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "LCATs",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldDefaultValue: "System");

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
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "LCATs",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldDefaultValue: "System");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "LCATs",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Contracts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Contracts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PrimeContractorName",
                table: "Contracts",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Contracts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldDefaultValue: "System");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Contracts",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Contracts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldDefaultValue: "System");

            migrationBuilder.AlterColumn<string>(
                name: "ContractNumber",
                table: "Contracts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

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

            migrationBuilder.AddColumn<decimal>(
                name: "StandardFTEHours",
                table: "Contracts",
                type: "numeric(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "ContractResources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldDefaultValue: "System");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "ContractResources",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ContractResources",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldDefaultValue: "System");

            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualHours",
                table: "ContractResources",
                type: "numeric(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddColumn<decimal>(
                name: "ContractBillRateOverride",
                table: "ContractResources",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Justification",
                table: "ContractModifications",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ContractModifications",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OverrideBillRate",
                table: "ContractLCATs",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "PositionTitle",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PositionTitle",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "RateType",
                table: "LCATRate",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "LCATRate",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "LCATRate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "LCATRate",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PositionTitle",
                table: "PositionTitle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LCATRate",
                table: "LCATRate",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContractLCATRate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractId = table.Column<Guid>(type: "uuid", nullable: false),
                    LCATId = table.Column<Guid>(type: "uuid", nullable: false),
                    BillRate = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractLCATRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractLCATRate_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractLCATRate_LCATs_LCATId",
                        column: x => x.LCATId,
                        principalTable: "LCATs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Name",
                table: "Resources",
                columns: new[] { "FirstName", "LastName" });

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
                name: "IX_ContractResources_ContractId",
                table: "ContractResources",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "UX_ContractResources_ActiveAssignment",
                table: "ContractResources",
                columns: new[] { "ContractId", "ResourceId" },
                unique: true,
                filter: "\"IsActive\" = true");

            migrationBuilder.CreateIndex(
                name: "IX_ContractModifications_ContractId",
                table: "ContractModifications",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractModifications_Date",
                table: "ContractModifications",
                column: "ModificationDate");

            migrationBuilder.CreateIndex(
                name: "IX_ContractLCATs_ContractId",
                table: "ContractLCATs",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionTitle_LCATId",
                table: "PositionTitle",
                column: "LCATId");

            migrationBuilder.CreateIndex(
                name: "IX_LCATRate_LCATId",
                table: "LCATRate",
                column: "LCATId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractLCATRate_ContractId",
                table: "ContractLCATRate",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractLCATRate_LCATId",
                table: "ContractLCATRate",
                column: "LCATId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractResources_Resources_ResourceId",
                table: "ContractResources",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LCATRate_LCATs_LCATId",
                table: "LCATRate",
                column: "LCATId",
                principalTable: "LCATs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionTitle_LCATs_LCATId",
                table: "PositionTitle",
                column: "LCATId",
                principalTable: "LCATs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
