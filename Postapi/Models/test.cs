using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
namespace Postapi.Models
{
    public class test :TableEntity 
    {
        public test(string ID, string json)
        {
            this.PartitionKey = ID;
            this.RowKey = ID;
        }
        public test() { }
        public string id { get; set; }
        public string jsonobject { get; set; }
    }
}