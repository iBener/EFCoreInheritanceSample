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

            var dtos = context.Cats
                .AsSplitQuery()
                .Include(x => x.Parent)
                .Include(x => x.Children).ToList();

            return [];
        }
    }
}
