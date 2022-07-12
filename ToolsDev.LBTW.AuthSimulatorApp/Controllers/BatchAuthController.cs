using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using ToolsDev.LBTW.AuthSimulatorApp.Models.BatchAuth;
using ToolsDev.Utilities.Helper;

namespace ToolsDev.LBTW.AuthSimulatorApp.Controllers
{
    public class BatchAuthController : Controller
    {
        private readonly string _folder;

        public BatchAuthController()
        {
            _folder = $@"~\Uploads\BatchAuth";
        }


        [HttpGet]
        public ActionResult Upload(UploadViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Upload()
        {
            UploadViewModel model = new UploadViewModel() { };

            try
            {
                model.ReturnCode = "0000";
                model.ReturnMessage = "BatchAuth Upload Success!";
            }
            catch (Exception ex)
            {
                model.ReturnCode = "9999";
                model.ReturnMessage = $"BatchAuth Upload Exception: {ex.ToJsonString()}";
            }

            return View(model);
        }

        public ActionResult Query()
        {
            return View();
        }
    }
}