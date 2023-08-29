using DataSource_InMemory.Business;
using Reveal.Sdk.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataSource_InMemory.Providers
{
    class InMemoryDataProvider : IRVDataProvider
    {
        RVInMemoryData<Sale> _salesInMemoryData;

        public InMemoryDataProvider(IEnumerable<Sale> sales)
        {
            _salesInMemoryData = new RVInMemoryData<Sale>(sales);
        }

        public Task<IRVInMemoryData> GetData(RVInMemoryDataSourceItem dataSourceItem)
        {
            if (dataSourceItem.DatasetId == "SalesRecords")
            {
                return Task.FromResult<IRVInMemoryData>(_salesInMemoryData);
            }
            else
            {
                throw new Exception("Invalid datasetId");
            }
        }
    }
}
