namespace BichoBet.Domain.Entities;

public class AccessLog
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime DateTime { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
    public string DeviceFingerPrint { get; set; }
}