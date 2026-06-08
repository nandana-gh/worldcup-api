namespace WorldCup.API.DTOs.Admin
{
    public class UpdateSettingsDto
    {
        public bool IsResultPublished { get; set; }
        public DateTime? PollClosingDate { get; set; }
    }
}
