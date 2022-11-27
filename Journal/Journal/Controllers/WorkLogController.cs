using AutoMapper;
using Journal.Dto.Create;
using Journal.Dto.Get;
using Journal.Dto.Update;
using Journal.Models;
using Journal.Paging;
using Journal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Journal.Controllers
{
    public class WorkLogController : Controller
    {
        private readonly IMapper mapper;
        private readonly IAsyncServiceWorkLog<WorkLog, GetWorkLogTableDto,GetDataChartDto> asyncServiceWorkLog;
        private readonly IAsyncService<Person, GetPersonDto> asyncServicePerson;

        public WorkLogController(
            IMapper mapper,
            IAsyncServiceWorkLog<WorkLog, GetWorkLogTableDto, GetDataChartDto> asyncServiceWorkLog,
            IAsyncService<Person, GetPersonDto> asyncServicePerson
            )
        {
            this.mapper = mapper;
            this.asyncServiceWorkLog = asyncServiceWorkLog;
            this.asyncServicePerson = asyncServicePerson;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Persons = mapper.Map<List<GetPersonDto>>(await asyncServicePerson.GetAll());

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PostWorkLogDto postWorkLogDto)
        {
            if (ModelState.IsValid)
            {
                await asyncServiceWorkLog.Create(mapper.Map<WorkLog>(postWorkLogDto));
            }
            else
            {
                return View(postWorkLogDto);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] QueryStringPagingParameters queryStringPagingParameters
            )
        {
            return View(await asyncServiceWorkLog.GetAll(mapper, queryStringPagingParameters));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, string pageNumber)
        {
            await asyncServiceWorkLog.Delete(id);

            return RedirectToAction("GetAll", new { PageNumber = pageNumber });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Persons = mapper.Map<List<GetPersonDto>>(await asyncServicePerson.GetAll());
            ViewBag.WorkLog = mapper.Map<GetWorkLogDto>(await asyncServiceWorkLog.GetById(id));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateWorkLogDto updateWorkLogDto)
        {
            if (ModelState.IsValid)
            {
                await asyncServiceWorkLog.Update(mapper.Map<WorkLog>(updateWorkLogDto));
            }
            else
            {
                return View(updateWorkLogDto);
            }

            return RedirectToAction("GetAll");
        }
    }
}

