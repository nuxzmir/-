using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Дипломочка.Models
{
    public class StockOperation
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = "";

        public string OperationType { get; set; } = "";

        public int Quantity { get; set; }

        public string OperationDate { get; set; } = "";
    }
}
