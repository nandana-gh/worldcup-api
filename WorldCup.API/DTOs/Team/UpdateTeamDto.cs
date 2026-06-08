namespace WorldCup.API.DTOs.Team
{
    public class UpdateTeamDto
    {
        public string TeamName { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public string FlagImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
