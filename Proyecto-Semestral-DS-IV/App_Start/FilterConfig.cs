using System.Web;
using System.Web.Mvc;

namespace Proyecto_Semestral_DS_IV
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
