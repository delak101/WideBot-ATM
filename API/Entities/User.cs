namespace API.Entities;

public class User
{
    public int Id { get; set; }
    public int CardNum { get; set; }
    public int PIN { get; set; }
    public byte[] PinHash { get; set; }
    public byte[] PinSalt { get; set; }
}
