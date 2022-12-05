using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Paginacao.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    public class TodoController : Controller
    {   
        [HttpGet("load")]
        public async Task<IActionResult> LoadAsync
        ([FromServices]AppDbContext context)
        {
            for (int i = 0; i < 100; i++)
            {
                var todo = new Todo()
                {
                    Id = i + 1,
                    Done = false,
                    Title = $"Tarefa {i}",
                    CreatedAt = DateTime.Now
                };
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpGet("skip/{skip:int}/take/{take:int}")]
        public async Task<IActionResult> GetAsync
        ([FromServices]AppDbContext context,
        [FromRoute] int skip = 0,
        [FromRoute] int take = 25)
        {
            var total = await context.Todos.CountAsync();
            var todos = await context
            .Todos
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync();

            return Ok(
                new
                {
                    total,
                    skip,
                    take,
                    data = todos
                }
            );
        }
    }
}