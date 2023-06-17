﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbBenchmarks.Migrations;

/// <inheritdoc />
public partial class ProductPriceDouble : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<double>(
            name: "Price",
            table: "Products",
            type: "REAL",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "TEXT");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "Price",
            table: "Products",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(double),
            oldType: "REAL");
    }
}
