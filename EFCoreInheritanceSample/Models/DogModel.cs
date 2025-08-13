namespace EFCoreInheritanceSample.Models;

public class DogModel
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MiceCaughtCount { get; set; }
    public DogModel? Parent { get; set; }
    public ICollection<DogModel>? Children { get; set; }
}
