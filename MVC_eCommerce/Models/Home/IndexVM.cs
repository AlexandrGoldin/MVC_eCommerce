using MVC_eCommerce.Models.Admin;
using System.Collections.Generic;

namespace MVC_eCommerce.Models.Home
{
    public class IndexVM
    {
        public string CatIdToString { get; set; }
        public string CurrentSearch { get; set; }
        public IEnumerable<ProductVM> ListProductVM { get; set; }
        public List<CategoryVM> categoryVMList { get; set; }
    }
}