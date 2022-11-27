using AutoMapper;
using Journal.Dto.Create;
using Journal.Dto.Get;
using Journal.Dto.Update;
using Journal.Models;
using Journal.Paging;
using Journal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Journal.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IMapper mapper;
        private readonly IAsyncService<Department, GetDepartmentDto> asyncServiceDepartment;

        public DepartmentController(
            IMapper mapper,
            IAsyncService<Department, GetDepartmentDto> asyncServiceDepartment
            )
        {
            this.mapper = mapper;
            this.asyncServiceDepartment = asyncServiceDepartment;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PostDepartmentDto postDepartmentDto)
        {
            if (ModelState.IsValid)
            {
                await asyncServiceDepartment.Create(mapper.Map<Department>(postDepartmentDto));
            }
            else
            {
                return View(postDepartmentDto);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] QueryStringPagingParameters queryStringPagingParameters
            )
        {
            return View(await asyncServiceDepartment.GetAll(mapper, queryStringPagingParameters));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, string pageNumber)
        {
            await asyncServiceDepartment.Delete(id);

            return RedirectToAction("GetAll", new { PageNumber = pageNumber });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View(mapper.Map<GetDepartmentDto>(await asyncServiceDepartment.GetById(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateDepartmentDto updateDepartmentDto)
        {
            if (ModelState.IsValid)
            {
                await asyncServiceDepartment.Update(mapper.Map<Department>(updateDepartmentDto));

                return RedirectToAction("GetAll");
            }
            else
            {
                return View(updateDepartmentDto);
            }

        }
    }
}
