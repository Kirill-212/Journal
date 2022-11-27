namespace Journal.Dto.Get
{
    public class GetPersonDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Surname { get; set; }

        public GetDepartmentDto GetDepartmentDto { get; set; }
    }
}
