using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Structs
{
    public struct AdvancedSearch
    {
        public string? brand;
        public Guid? model;
        public double? minprice;
        public double? maxprice;
        public int? minyear;
        public int? maxyear;
        public Guid? color;
        public Guid? ban;
        public Guid? fuel;
        public int page = 1;

        public AdvancedSearch()
        {
            
        }
    }
}
