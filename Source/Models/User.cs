using Source.Models.Abstract;

namespace Source.Models;

public class User : BaseEntity
{
    public string Username{ get; set; }
    public byte[] PassHash{ get; set; }
    public byte[] PashSalt{ get; set; }
}
