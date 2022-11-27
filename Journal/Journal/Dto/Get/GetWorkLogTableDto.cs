namespace Journal.Dto.Get
{
    public class GetWorkLogTableDto
    {
        public GetWorkLogDto GetWorkLogDto { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public double TranslateHour { get; set; }
    }
}
