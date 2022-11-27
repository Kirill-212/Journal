using AutoMapper;
using Journal.Dto.Create;
using Journal.Dto.Get;
using Journal.Dto.Update;
using Journal.Models;
using Journal.Paging;
using Journal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Journal.Controllers
{
    public class PersonController : Controller
    {
        private readonly IMapper mapper;
        private readonly IAsyncService<Person, GetPersonDto> asyncServicePerson;
        private readonly IAsyncService<Department, GetDepartmentDto> asyncServiceDepartment;

        public PersonController(
            IMapper mapper,
            IAsyncService<Person, GetPersonDto> asyncServicePerson,
            IAsyncService<Department, GetDepartmentDto> asyncServiceDepartment
            )
        {
            this.mapper = mapper;
            this.asyncServicePerson = asyncServicePerson;
            this.asyncServiceDepartment = asyncServiceDepartment;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = new SelectList(await asyncServiceDepartment.GetAll(), "Id", "Name"); 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PostPersonDto postPersonDto)
        {
            if (ModelState.IsValid)
            {
                await asyncServicePerson.Create(mapper.Map<Person>(postPersonDto));
            }
            else
            {
                return View(postPersonDto);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] QueryStringPagingParameters queryStringPagingParameters
            )
        {
            return View(await asyncServicePerson.GetAll(mapper, queryStringPagingParameters));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, string pageNumber)
        {
            await asyncServicePerson.Delete(id);

            return RedirectToAction("GetAll", new { PageNumber = pageNumber });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Departments =mapper.Map<List<GetDepartmentDto>>( await asyncServiceDepartment.GetAll());
            ViewBag.Person = mapper.Map<GetPersonDto>(await asyncServicePerson.GetById(id));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdatePersonDto updatePersonDto)
        {
            if (ModelState.IsValid)
            {
                await asyncServicePerson.Update(mapper.Map<Person>(updatePersonDto));
            }
            else
            {
                return View(updatePersonDto);
            }

            return RedirectToAction("GetAll");
        }
    }
}

