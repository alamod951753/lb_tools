using System.Web;
using System.Web.Mvc;

namespace ToolsDev.LBTW.AuthSimulatorApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
