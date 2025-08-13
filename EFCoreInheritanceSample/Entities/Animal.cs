namespace EFCoreInheritanceSample.Entities;

public abstract class Animal : IAnimal<Animal>
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string Name { get; set; } = String.Empty;

    public virtual Animal? Parent { get; set; }

    public virtual ICollection<Animal>? Children { get; set; }
}

public interface IAnimal<T> where T : Animal
{
    T? Parent { get; set; }
    ICollection<T>? Children { get; set; }
}
