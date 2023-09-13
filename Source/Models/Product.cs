using Source.Models.Abstract;

namespace Source.Models;

public class Product : BaseEntity
{
    public float Price { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
}
