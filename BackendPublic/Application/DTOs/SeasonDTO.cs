namespace Application.DTOs;

    public class SeasonDTO
    {
        public int SeasonID { get; set; }
        public string SeasonName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Percent { get; set; }
        public bool IsActive { get; set; }
        public bool IsHigh { get; set; }
    }
