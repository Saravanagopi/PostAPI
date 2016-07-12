using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Postapi.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;

namespace Postapi.Controllers
{
    public class TestController : ApiController
    {
        static async void CreateTable(CancellationToken cancel, string param)
        {
            try
            {


                //string accountName = "teststorenew";
                //string accountKey = "wYprFx2YPddhfvURWulSER+HXfJg+pRsJAjmM/yCk7kdz3U3K737bieEh8MDKQmCY6Nm05//lyUhbqobe004ug==";
                //StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                //CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
                //CloudTableClient client = account.CreateCloudTableClient();
                //CloudTable table = client.GetTableReference("Test");
                //table.CreateIfNotExists();


                // Create a new customer entity.

                string str = "{'vin': '00000','fcd': [{'latitude': '10.000000','longitude':'-20.000000','speed': 60,'heading': '10','timestamp': '2016-06-20T10:20:30Z'},]}";
                //JavaScriptSerializer j = new JavaScriptSerializer();
                //object a = j.Deserialize(str, typeof(object));

                //string json = new JavaScriptSerializer().Serialize(jstr);


                //objcls.PartitionKey = "1";
                //objcls.RowKey = str;


                // Create the TableOperation object that inserts the customer entity.

                string accountName = "teststorenew";
                string accountKey = "wYprFx2YPddhfvURWulSER+HXfJg+pRsJAjmM/yCk7kdz3U3K737bieEh8MDKQmCY6Nm05//lyUhbqobe004ug==";
                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
                CloudTableClient client = account.CreateCloudTableClient();
                CloudTable table = client.GetTableReference("TestCallSample");
                table.CreateIfNotExists();

                //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Microsoft.Azure.CloudConfigurationManager.GetSetting("StorageConnectionString"));
                //CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                Vehicle objvehicle = JsonConvert.DeserializeObject<Vehicle>(param); //Just deseriaizing string to object not used

                for (int i = 0; i < 10000000; i++)
                {
                    if (objvehicle.vin == "000000")
                    {
                        if (i == 999999)
                        {
                            // test objcls = new test(Convert.ToString(i) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second), param);
                            TestEnity TE = new TestEnity(Convert.ToString(i) + "-" + Convert.ToString(DateTime.Now.Hour) + ":" + Convert.ToString(DateTime.Now.Minute) + ":" + Convert.ToString(DateTime.Now.Second) + ":" + Convert.ToString(DateTime.Now.Millisecond), param);
                            Thread.Sleep(1000);
                            TableOperation insertOp = TableOperation.Insert(TE);
                            table.Execute(insertOp);
                        }
                    }
                    else if (i == 10000)
                    {
                        Thread.Sleep(1000);
                        //test objcls = new test(Convert.ToString(i) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second), param);
                        TestEnity TE = new TestEnity(Convert.ToString(i) + "-" + Convert.ToString(DateTime.Now.Hour) + ":" + Convert.ToString(DateTime.Now.Minute) + ":" + Convert.ToString(DateTime.Now.Second) + ":" + Convert.ToString(DateTime.Now.Millisecond), param);
                        TableOperation insertOp = TableOperation.Insert(TE);
                        table.Execute(insertOp);
                    }

                    else
                    {
                        if (i == 8900000)
                        {
                            Thread.Sleep(1000);
                            //test objcls = new test(Convert.ToString(i) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second), param);
                            TestEnity TE = new TestEnity(Convert.ToString(i) + "-" + Convert.ToString(DateTime.Now.Hour) + ":" + Convert.ToString(DateTime.Now.Minute) + ":" + Convert.ToString(DateTime.Now.Second) + ":" + Convert.ToString(DateTime.Now.Millisecond), param);
                            TableOperation insertOp = TableOperation.Insert(TE);
                            table.Execute(insertOp);
                        }
                    }

                }
                if (cancel.IsCancellationRequested)
                {
                    //code for cancel to exit 
                }
            }
            catch (Exception ex)
            { }
        }

        private void testcallBack(string url)
        {
            HttpClient client = new HttpClient();
            // string baseUrl = "http://localhost:64103/";
            string baseUrl = "http://postapitest.azurewebsites.net/";
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string serviceUrl;
            serviceUrl = "api/Test/InsertCallback";
            HttpResponseMessage response = client.PostAsync(serviceUrl, new StringContent("")).Result;// work
        }
        static void TaskCancel()
        {
            //exit code
        }
        [System.Web.Http.HttpPost]
        public void InsertCallback()
        {
            string accountName = "teststorenew";
            string accountKey = "wYprFx2YPddhfvURWulSER+HXfJg+pRsJAjmM/yCk7kdz3U3K737bieEh8MDKQmCY6Nm05//lyUhbqobe004ug==";
            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
            CloudTableClient client = account.CreateCloudTableClient();
            CloudTable table = client.GetTableReference("TestCallSample");
            table.CreateIfNotExists();
            TestEnity TEcallback = new TestEnity(Convert.ToString("Callback") + "-" + Convert.ToString(DateTime.Now.Hour) + ":" + Convert.ToString(DateTime.Now.Minute) + ":" + Convert.ToString(DateTime.Now.Second) + ":" + Convert.ToString(DateTime.Now.Millisecond), "callback");
            Thread.Sleep(1000);
            TableOperation insertOp = TableOperation.Insert(TEcallback);
            table.Execute(insertOp);
        }
        [System.Web.Http.HttpPost]
        public Vehicle Insert(string param)
        {
            try
            {
                var CancelTokensrc = new CancellationTokenSource();
                var cancelToken = CancelTokensrc.Token;

                cancelToken.Register(() => TaskCancel());
                string url = "";
                //var t = Task.Factory.StartNew(() => TestMethod(cancelToken, filename), CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                //Task.Run(() => TestMethod(cancelToken, filename), cancelToken);
                //uncomment above line ,up to above code will do mutitask simultaneously
                //brlow code is newly added for callback

                Task a = Task.Run(() => CreateTable(cancelToken, param), cancelToken);

                Task b = a.ContinueWith((Taskcall) =>
                {
                    testcallBack(url);

                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            Vehicle obj = jsonSerializer.Deserialize<Vehicle>(param); //Just deseriaizing string to object not used
            // Vehicle objvehicle = JsonConvert.DeserializeObject<Vehicle>(param); //Just deseriaizing string to object not used
            return obj;
        }

    }
}
