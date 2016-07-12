using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
namespace Postapi.Models
{
    public class Vehicle: TableEntity
    {

        //public Vehicle(string _vin, string FCD)
        //{
        //    vin = _vin;
        //    fcd = FCD;
        //    PartitionKey = _vin.ToString();
        //    RowKey = _vin.ToString();
        //}

        private string _number;
        public string vin
        {
            get { return _number; }
            set { _number = value; }
        }

        public List<VehicleVM> fcd { get; set; }
    }
}