using DataSource_InMemory.Business;
using Reveal.Sdk;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataSource_InMemory.Providers
{
    class InMemoryDataProvider : IRVDataProvider
    {
        RVInMemoryData<Sale> _salesInMemoryData = null;

        private IEnumerable<Sale> _salesRecords;
        public IEnumerable<Sale> SalesRecords
        {
            get { return _salesRecords; }
            set 
            { 
                _salesRecords = value;
                _salesInMemoryData = new RVInMemoryData<Sale>(_salesRecords);
            }
        }        

        public InMemoryDataProvider(IEnumerable<Sale> sales)
        {
            SalesRecords = sales;
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
