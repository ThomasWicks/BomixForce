using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
    }
    public class ItemViewIndex
    {
        public IEnumerable<ItemViewModel> ItemList { get; set; }
        public ItemViewModel Item { get; set; }
        public ItemViewModel ItemViewDetails { get; set; }
    }
}
