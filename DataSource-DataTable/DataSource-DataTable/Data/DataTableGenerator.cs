using System;
using System.Data;

namespace DataSource_DataTable.Data
{
    public class DataTableGenerator
    {
        public static DataTable CreateNewDataTable()
        {
            // Create a new DataTable.
            System.Data.DataTable table = new DataTable("Employees");
            // Declare variables for DataColumn and DataRow objects.
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType,
            // ColumnName and add to DataTable.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "EmployeeID";
            column.ReadOnly = true;
            column.Unique = true;
            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Fullname";
            column.AutoIncrement = false;
            column.Caption = "Fullname";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "Wage";
            column.AutoIncrement = false;
            column.Caption = "Wage";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.DateTime");
            column.ColumnName = "DateTimeColumn";
            column.AutoIncrement = false;
            column.Caption = "DateTimeColumn";
            column.ReadOnly = false;
            column.Unique = false;
            column.AllowDBNull = true;

            // Add the column to the table.
            table.Columns.Add(column);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["id"];
            table.PrimaryKey = PrimaryKeyColumns;

            // Create three new DataRow objects and add
            // them to the DataTable
            for (int i = 0; i <= 2; i++)
            {
                row = table.NewRow();
                row["EmployeeID"] = i.ToString();
                row["Fullname"] = "John Doe " + i.ToString();
                row["Wage"] = i * 1000;
                row["DateTimeColumn"] = DateTime.Now - new TimeSpan(24 * i, 0, 0);
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
