using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
namespace AccessAzureStorage.Models
{
    public class EmployeeData : TableEntity
    {
        public EmployeeData(string EmpId, string Name)
        {
            this.PartitionKey = EmpId;
            this.RowKey = Name;
        }

        public EmployeeData() { }
        public string id { get; set; }
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public string Domain { get; set; }
        public string Email { get; set; }
    }
}