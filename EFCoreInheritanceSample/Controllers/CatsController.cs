using EFCoreInheritanceSample.DbContext;
using EFCoreInheritanceSample.Entities;
using EFCoreInheritanceSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreInheritanceSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatsController(AnimalDbContext context) : ControllerBase
    {

        [HttpGet]
        public IEnumerable<CatModel> Get()
        {
            //return Enumerable.Range(1, 5).Select(index => new Cat
            //{
            //    Id = index,
            //    Name = $"Cat {index}",
            //    MiceCaughtCount = Random.Shared.Next(0, 10),
            //    ParentId = index % 2 == 0 ? null : (int?)index - 1
            //});

            var cat1 = context.Cats
                .Include(x => x.Parent)
                .ToList();

            //var dtos = context.Cats
            //    .Include(x => x.Parent)
            //    .Include(x => x.Children).ToList();

            // TPT kullanım örneği
            using (var context = new AnimalDbContext())
            {
                // Migration oluştur ve veritabanını güncelle
                // Add-Migration InitialCreateTPT
                // Update-Database

                // Parent cat oluştur
                var parentCat = new Cat
                {
                    Name = "Fluffy",
                    MiceCaughtCount = 10
                };

                context.Cats.Add(parentCat);
                context.SaveChanges();

                // Child cat oluştur
                var childCat = new Cat
                {
                    Name = "Mittens",
                    MiceCaughtCount = 3,
                    ParentId = parentCat.Id
                };

                context.Cats.Add(childCat);
                context.SaveChanges();

                // Parent dog oluştur
                var parentDog = new Dog
                {
                    Name = "Rex",
                    BonesBuriedCount = 15
                };

                context.Dogs.Add(parentDog);
                context.SaveChanges();

                // Child dog oluştur
                var childDog = new Dog
                {
                    Name = "Buddy",
                    BonesBuriedCount = 5,
                    ParentId = parentDog.Id
                };

                context.Dogs.Add(childDog);
                context.SaveChanges();

                // Parent ve child'larını getir
                var catsWithChildren = context.Cats
                    .Include(c => c.Children)
                    .Where(c => c.ParentId == null)
                    .ToList();

                foreach (var cat in catsWithChildren)
                {
                    Console.WriteLine($"Parent Cat: {cat.Name}, Children: {cat.Children.Count}");
                    foreach (var child in cat.Children)
                    {
                        Console.WriteLine($"  Child: {child.Name}");
                    }
                }

                // Specific bir animal'ın parent'ını getir
                var animalWithParent = context.Animals
                    .Include(a => a.Parent)
                    .FirstOrDefault(a => a.Name == "Mittens");

                if (animalWithParent?.Parent != null)
                {
                    Console.WriteLine($"{animalWithParent.Name}'s parent is {animalWithParent.Parent.Name}");
                }
            }

            return [];
        }
    }
}
