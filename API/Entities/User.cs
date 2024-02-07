namespace API.Entities;

public class User
{
    public int Id { get; set; }
    public long CardNum { get; set; }
    public int PIN { get; set; }
    public decimal Balance { get; set; }
    public byte[] PinHash { get; set; }
    public byte[] PinSalt { get; set; }
}
