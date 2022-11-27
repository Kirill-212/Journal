using Journal.Dto.Get;
using Journal.Models;
using Journal.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Journal.Controllers
{
    public class ChartsController : Controller
    {
        private readonly IAsyncServiceWorkLog<WorkLog, GetWorkLogTableDto, GetDataChartDto> asyncServiceWorkLog;

        public ChartsController(
            IAsyncServiceWorkLog<WorkLog, GetWorkLogTableDto, GetDataChartDto> asyncServiceWorkLog
            )
        {         
            this.asyncServiceWorkLog = asyncServiceWorkLog;
        }

        public IActionResult Charts()
        {
            return View();
        }

        public IActionResult DateCharts()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Data()
        {
            return Json(await asyncServiceWorkLog.GetDataChart());
        }

        [HttpGet]
        public async Task<JsonResult> DataDepartments()
        {
            return Json(await asyncServiceWorkLog.GetDepartmentDataChart());
        }

        [HttpGet]
        public async Task<JsonResult> DateData(DateTime? start, DateTime? end)
        {
            return Json(await asyncServiceWorkLog.GetDataChart(start, end));
        }

        [HttpGet]
        public async Task<JsonResult> DateDataDepartments(DateTime? start, DateTime? end)
        {
            return Json(await asyncServiceWorkLog.GetDepartmentDataChart(start, end));
        }
    }
}
