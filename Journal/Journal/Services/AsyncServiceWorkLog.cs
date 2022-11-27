using AutoMapper;
using Journal.Dto.Get;
using Journal.Models;
using Journal.Paging;
using Journal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Journal.Services
{
    public class AsyncServiceWorkLog : IAsyncServiceWorkLog<WorkLog, GetWorkLogTableDto, GetDataChartDto>
    {
        private readonly IAsyncRepositoryWorkLog<WorkLog> _asyncRepository;
        private readonly IAsyncRepository<Department> _asyncRepositoryDepartment;

        public AsyncServiceWorkLog(
            IAsyncRepositoryWorkLog<WorkLog> asyncRepository,
            IAsyncRepository<Department> asyncRepositoryDepartment
            )
        {
            _asyncRepository = asyncRepository;
            _asyncRepositoryDepartment = asyncRepositoryDepartment;
        }

        public async Task Create(WorkLog WorkLog)
        {
            await _asyncRepository.Create(WorkLog);
        }

        public async Task Delete(int id)
        {
            WorkLog WorkLog = await _asyncRepository.GetById(id);
            if (WorkLog != null)
            {
                await _asyncRepository.Delete(WorkLog);
            }
        }

        public async Task<PagedList<GetWorkLogTableDto>> GetAll(IMapper mapper, QueryStringPagingParameters queryStringPagingParameters)
        {
            IEnumerable<WorkLog> workLogs = await _asyncRepository.Get();
            List<GetWorkLogTableDto> workLogsDto = mapper
                .Map<List<GetWorkLogTableDto>>(
                workLogs.OrderBy(i => i.Date).ToList()
                );
            int count = workLogsDto.Count();
            for (int i = 0; i < count; i++)
            {
                workLogsDto[i].Hour =
                    workLogsDto[i].GetWorkLogDto.EndTime.Hour - workLogsDto[i].GetWorkLogDto.StartTime.Hour;
                workLogsDto[i].Minute =
                    Math.Abs(workLogsDto[i].GetWorkLogDto.EndTime.Minute + (60 - workLogsDto[i].GetWorkLogDto.StartTime.Minute));
                workLogsDto[i].TranslateHour = Math.Round(workLogsDto[i].Hour + (workLogsDto[i].Minute / 60.0), 1);
            }
            workLogsDto = workLogsDto
                .Skip((queryStringPagingParameters.PageNumber - 1) * queryStringPagingParameters.PageSize)
                .Take(queryStringPagingParameters.PageSize).ToList();

            return PagedList<GetWorkLogTableDto>.ToPagedList(count, workLogsDto,
                    queryStringPagingParameters.PageNumber,
                   queryStringPagingParameters.PageSize);
        }

        public async Task<List<WorkLog>> GetAll()
        {
            return (await _asyncRepository.Get()).ToList();
        }

        public async Task<WorkLog> GetById(int id)
        {
            return await _asyncRepository.GetById(id);
        }

        public async Task<List<GetDataChartDto>> GetDataChart()
        {
            IEnumerable<WorkLog> workLogs = await _asyncRepository.Get();
            List<GetDataChartDto> dataChar = new List<GetDataChartDto>();
            foreach (WorkLog i in workLogs)
            {
                dataChar.Add(new GetDataChartDto()
                {
                    Name = i.Person.FirstName + ' ' + i.Person.LastName + ' ' + i.Person.Surname,
                    Value = Math.Round(
                    (i.EndTime.Hour - i.StartTime.Hour) +
                    ((Math.Abs(i.EndTime.Minute + (60 - i.StartTime.Minute))) / 60.0)
                    , 1)
                });
            }

            return dataChar;
        }

        public async Task<List<GetDataChartDto>> GetDataChart(DateTime? start, DateTime? end)
        {
            IEnumerable<WorkLog> workLogs = await _asyncRepository.Get();
            List<GetDataChartDto> dataChar = new List<GetDataChartDto>();
            foreach (WorkLog i in workLogs)
            {
                if ((i.StartTime > start && i.EndTime < end) || (end == null && i.StartTime > start))
                {
                    dataChar.Add(new GetDataChartDto()
                    {
                        Name = i.Person.FirstName + ' ' + i.Person.LastName + ' ' + i.Person.Surname,
                        Value = Math.Round(
                        (i.EndTime.Hour - i.StartTime.Hour) +
                        ((Math.Abs(i.EndTime.Minute + (60 - i.StartTime.Minute))) / 60.0)
                        , 1)
                    });
                }
            }

            return dataChar;
        }

        public async Task<List<GetDataChartDto>> GetDepartmentDataChart()
        {
            IEnumerable<Department> departments = await _asyncRepositoryDepartment.Get();
            List<GetDataChartDto> dataChar = new List<GetDataChartDto>();
            double sumHour;
            foreach (Department department in departments)
            {
                IEnumerable<WorkLog> works = await _asyncRepository.GetllByDepartment(department.Name);
                sumHour = 0.0;
                foreach (WorkLog i in works)
                {
                    sumHour += (i.EndTime.Hour - i.StartTime.Hour) +
                      ((Math.Abs(i.EndTime.Minute + (60 - i.StartTime.Minute))) / 60.0);
                }
                dataChar.Add(new GetDataChartDto()
                {
                    Name = department.Name,
                    Value = Math.Round(sumHour, 1)
                });
            }

            return dataChar;
        }

        public async Task<List<GetDataChartDto>> GetDepartmentDataChart(DateTime? start, DateTime? end)
        {
            IEnumerable<Department> departments = await _asyncRepositoryDepartment.Get();
            List<GetDataChartDto> dataChar = new List<GetDataChartDto>();
            double sumHour;
            foreach (Department department in departments)
            {
                IEnumerable<WorkLog> works = await _asyncRepository.GetllByDepartment(department.Name);
                sumHour = 0.0;
                foreach (WorkLog i in works)
                {
                    if ((i.StartTime > start && i.EndTime < end) || (end == null && i.StartTime > start))
                    {
                        sumHour += (i.EndTime.Hour - i.StartTime.Hour) +
                      ((Math.Abs(i.EndTime.Minute + (60 - i.StartTime.Minute))) / 60.0);
                    }
                }
                dataChar.Add(new GetDataChartDto()
                {
                    Name = department.Name,
                    Value = Math.Round(sumHour, 1)
                });
            }

            return dataChar;
        }

        public async Task Update(WorkLog workLog)
        {
            WorkLog getWorkLog = await _asyncRepository.GetById(workLog.Id);
            if (getWorkLog != null)
            {
                getWorkLog.Date = workLog.Date;
                getWorkLog.StartTime = workLog.StartTime;
                getWorkLog.EndTime = workLog.EndTime;
                getWorkLog.PersonId = workLog.PersonId;
                await _asyncRepository.Update(getWorkLog);
            }
        }
    }
}
