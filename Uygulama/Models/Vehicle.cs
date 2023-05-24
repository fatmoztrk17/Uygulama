using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Uygulama.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public string Plate { get; set; }
        public int ModelYear { get; set; }
        public string Color { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}