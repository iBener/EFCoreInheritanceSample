using EFCoreInheritanceSample.DbContext;
using EFCoreInheritanceSample.Entities;
using EFCoreInheritanceSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreInheritanceSample.Controllers;

[ApiController]
[Route("[controller]")]
public class DogsController(AnimalDbContext context) : ControllerBase
{

    [HttpGet]
    public IEnumerable<DogModel> Get()
    {
        var dtos = context.Dogs
                .AsSplitQuery()
                .Include(x => x.Parent)
                .Include(x => x.Children)
                .Select(dto => new DogModel
                {
                    Id = dto.Id,
                    ParentId = dto.ParentId,
                    Children = dto.Children
                        .Select(x => new DogModel
                        {
                            Id = dto.Id,
                            ParentId = x.ParentId,
                        }).ToList(),
                }
                ).ToList();

        return dtos;
    }

}
