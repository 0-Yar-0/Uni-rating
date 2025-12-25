using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/iteration")]
public class IterationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public IterationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIteration([FromBody] Iteration iteration)
    {
        _context.Iterations.Add(iteration);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Iteration created successfully." });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetIteration(int id)
    {
        var iteration = await _context.Iterations.FindAsync(id);
        if (iteration == null) return NotFound();
        return Ok(iteration);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIteration(int id, [FromBody] Iteration updatedIteration)
    {
        var iteration = await _context.Iterations.FindAsync(id);
        if (iteration == null) return NotFound();

        iteration.ClassName = updatedIteration.ClassName;
        iteration.Names = updatedIteration.Names;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Iteration updated successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIteration(int id)
    {
        var iteration = await _context.Iterations.FindAsync(id);
        if (iteration == null) return NotFound();

        _context.Iterations.Remove(iteration);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Iteration deleted successfully." });
    }
}