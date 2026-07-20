namespace Hydra.Auth.Models
{
    public record OtpEntry
    {
        public string Code { get; init; }
        public DateTime CreatedAt { get; init; }
        public int AttemptCount { get; set; }
    }
}
