using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BetterCalendar.Controllers
{
    public class VueController : Controller
    {
        [Route("vue")]
        public IActionResult Index()
        {
            return Content(LoadHtmlPage($"VueApp/dist/index"), "text/html");
        }

        #region Helpers

        private string LoadHtmlPage(string path)
        {
            return System.IO.File.ReadAllText($"{path}.html");
        }

        #endregion
    }


}   
