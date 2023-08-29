using Reveal.Sdk;
using Reveal.Sdk.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataSource_DataTable.Data
{
    class DataTableInMemoryData : RVInMemoryData
    {
        DataTable _dataTable;
        List<string> _columnNames = new List<string>();

        public DataTableInMemoryData(DataTable dataTable)
        {
            _dataTable = dataTable;
            foreach (DataColumn column in _dataTable.Columns)
            {
                _columnNames.Add(column.ColumnName);
            }
        }

        public override IEnumerable<IEnumerable<object>> GetData()
        {
            foreach (DataRow row in _dataTable.Rows)
            {
                var resultRow = new List<object>();
                foreach (var columnName in _columnNames)
                {
                    resultRow.Add(row[columnName]);
                }
                yield return resultRow;
            }
        }

        public override IEnumerable<RVSchemaColumn> GetSchema()
        {
            foreach (DataColumn column in _dataTable.Columns)
            {
                yield return new RVSchemaColumn(column.ColumnName, column.Caption, ResolveDataType(column));
            }
        }

        private RVSchemaColumnType ResolveDataType(DataColumn column)
        {
            RVSchemaColumnType columnType = default;
            var type = column.DataType;
            if (type == typeof(double) || type == typeof(double?) ||
                type == typeof(float) || type == typeof(float?) ||
                type == typeof(decimal) || type == typeof(decimal?) ||
                type == typeof(int) || type == typeof(int?) ||
                type == typeof(long) || type == typeof(long?) ||
                type == typeof(double) || type == typeof(double?))
            {
                columnType = RVSchemaColumnType.Number;
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                columnType = RVSchemaColumnType.DateTime;
            }
            else if (type == typeof(TimeSpan) || type == typeof(TimeSpan?))
            {
                columnType = RVSchemaColumnType.Time;
            }
            else if (type == typeof(string))
            {
                columnType = RVSchemaColumnType.Text;
            }

            return columnType;
        }
    }
}
