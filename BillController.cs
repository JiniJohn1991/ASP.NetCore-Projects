using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeManagementApplicationMain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeManagementApplicationMain.Controllers
{
    [Route("HomeManagementApplicationMain/[controller]")]
    public class BillController : Controller
    {
        private HMDBContext context;
        public IConfiguration Configuration { get; set; }

        public BillController(HMDBContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Bill()
        {

            BillMaster billDetails = new BillMaster();
            var billDdl = context.BillMaster.Select(y => new { y.BillId, y.BillName });
            List<SelectListItem> BillList = new List<SelectListItem>();
            foreach (var item in billDdl)
            {
                BillList.Add(new SelectListItem { Text = item.BillName, Value = item.BillId.ToString() });
            }
            billDetails.BillList = BillList;
            return View(billDetails);
        }
        [HttpPost]
        public IActionResult Bill(BillDetails billDetails)
        {
            if (ModelState.IsValid)
            {
                BillDetails details = new BillDetails();
                details.Amount = billDetails.Amount;
                details.Date = billDetails.Date;
                details.Store = billDetails.Store;
                context.Add<BillDetails>(billDetails);
                context.SaveChanges();
            }
            return View();
        }
    }
}
