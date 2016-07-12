using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
namespace Postapi.Models
{
    public class TestEnity : TableEntity
    {

        public TestEnity(string Vin, string FCD)
        {
            _Id = Vin;
            _FCD = FCD;
            PartitionKey = Vin.ToString();
            RowKey = Vin.ToString();
        }
        public string  _Id { get; set; }
        public string _FCD { get; set; }

    }

}
