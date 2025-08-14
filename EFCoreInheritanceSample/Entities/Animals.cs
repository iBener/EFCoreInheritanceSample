namespace EFCoreInheritanceSample.Entities;

public abstract class Animal
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public int? ParentId { get; set; }

    // Navigation Properties
    public virtual Animal? Parent { get; set; }
    public virtual ICollection<Animal> Children { get; set; } = new List<Animal>();
}

public class Cat : Animal
{
    public int MiceCaughtCount { get; set; }
}

public class Dog : Animal
{
    public int BonesBuriedCount { get; set; }
}
