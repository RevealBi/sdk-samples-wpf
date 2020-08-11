using DataSource_DataTable.Data;
using Infragistics.Sdk;
using System;
using System.Threading.Tasks;

namespace DataSource_DataTable.Providers
{
    class DataTableDataProvider : IRVDataProvider
    {
        DataTableInMemoryData _dataSetInMemoryData = null;

        public DataTableDataProvider()
        {
            _dataSetInMemoryData = new DataTableInMemoryData(DataTableGenerator.CreateNewDataTable());
        }

        public Task<IRVInMemoryData> GetData(RVInMemoryDataSourceItem dataSourceItem)
        {
            var datasetId = dataSourceItem.DatasetId;
            if (datasetId == "employees")
            {

                return Task.FromResult<IRVInMemoryData>(_dataSetInMemoryData);
            }
            else
            {
                throw new Exception("Invalid datasetId");
            }

        }
    }
}
