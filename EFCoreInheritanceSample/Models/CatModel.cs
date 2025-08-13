namespace EFCoreInheritanceSample.Models;

public class CatModel
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MiceCaughtCount { get; set; }
    public CatModel? Parent { get; set; }
    public ICollection<CatModel>? Children { get; set; }
}
