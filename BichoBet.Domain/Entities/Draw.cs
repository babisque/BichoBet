namespace BichoBet.Domain.Entities;

public class Draw
{
    public Guid Id { get; set; }
    public DateTime DrawDate { get; set; }
    public string Status { get; set; }
}