using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Models
{
    public class AddInventoryModel
    {
        public AddInventoryModel()
        {
            AddedItems = new HashSet<AddedItem>();
        }
        public int ID { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public ICollection<AddedItem> AddedItems { get; set; }

    }

    public class AddedItem
    {
        public int ID { get; set; }
        public int AddInventoryModelId { get; set;}
        public int InventoryItemId { get; set; }
        public int Quantity { get; set; }
        public virtual AddInventoryModel AddInventoryModel { get; set; }
        public virtual InventoryItem InventoryItem { get; set; }

    }

    public class AddInventoryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }

    public class AddInventoryDetailViewModel
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }
}

