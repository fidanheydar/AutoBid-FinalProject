using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Responses
{
    public class ChartResponse
    {
        public string ModelName { get; set; }
        public double[] TotalSales { get; set; }
    }
}
