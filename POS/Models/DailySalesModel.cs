using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
        public class DailySalesModel
        {

            public DailySalesModel()
            {
                ItemsSoldDaily = new HashSet<ItemsSoldDaily>();
            }
            public int ID { get; set; }
            public string SalesRep { get; set; }
            public DateTime Date { get; set; } = DateTime.Today;
            public ICollection<ItemsSoldDaily> ItemsSoldDaily { get; set; }

        }

        public class ItemsSoldDaily
        {
            public int ID { get; set; }
            public int SalesRepId { get; set; }
            public int DailySalesId { get; set; }
            public virtual DailySalesModel DailySalesModel { get; set; }

        }

        public class PaymentMethod
        {
            public int ID { get; set; }
            public string Type { get; set; }
        }

        public class InventoryItem
        {
            public InventoryItem()
            {
                AddedItems = new HashSet<AddedItem>();
            }

            public int ID { get; set; }
            public string Name { get; set; }
            public string Ref { get; set; }
            public string VendorName { get; set; }
            public string VendorAddress { get; set; }
            public string VendorPhone { get; set; }
            public double Cost { get; set; }
            public double PriceCOP { get; set; }
            public double PriceUSD { get; set; }
            public int ReorderQty { get; set; }
            public int StockQty { get; set; }
            public ICollection<AddedItem> AddedItems { get; set; }

    }

        public class DailySalesEntry
        {
            public int ID { get; set; }
            public int InventoryItemId { get; set; }
            public virtual InventoryItem InventoryItem { get; set; }
            public int DailySalesId { get; set; }
            public virtual DailySalesModel DailySales { get; set; }
            public int PaymentMethodId { get; set; }
            public virtual PaymentMethod PaymentMethod { get; set; }
            public int Quantity { get; set; }
            public double ItemPriceCOP { get; set; }
            public double AmountCOP { get; set; }
            public double ItemPriceUSD { get; set; }
            public double AmountUSD { get; set; }

        }

}
