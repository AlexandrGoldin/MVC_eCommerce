using MVC_eCommerce.Models.Home;

namespace MVC_eCommerce.Interfaces
{
    public interface IHomeService
    {
        IndexVM GetSearch(string search);
    }
}
