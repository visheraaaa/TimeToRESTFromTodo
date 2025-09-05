using TimeToRESTFromTodo.Models;
using TimeToRESTFromTodo.Contracts;
using Microsoft.EntityFrameworkCore;
using TimeToRESTFromTodo.Data;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();


app.MapGet("/tasks", async(AppDbContext db, CancellationToken ct) =>
    await db.Tasks.AsNoTracking().ToListAsync());

app.MapGet("/tasks/{id:guid}", async (Guid id, AppDbContext db, CancellationToken ct) =>
{
    var task = await db.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, ct);
    return task is null ? Results.NotFound() : Results.Ok(task);
});

app.MapPost("/tasks", async (TaskCreateDTO TaskDTO, AppDbContext db, CancellationToken ct) =>
{
    TaskItem task = new TaskItem(TaskDTO.Title, TaskDTO.Description);
    db.Tasks.Add(task);
    await db.SaveChangesAsync(ct);
    return Results.Created($"/tasks/{task.Id}", task);
});

app.MapPatch("/task/{id}", async(Guid id, TaskPatchDTO PatchDTO, AppDbContext db, CancellationToken ct) =>
{
    TaskItem? task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id, ct);
    if (task is null) Results.NotFound();

    if (PatchDTO.Title is not null)
        task.Title = PatchDTO.Title;

    if (PatchDTO.Description is not null)
        task.Description = PatchDTO.Description;

    if (PatchDTO.IsCompleted.HasValue)
        task.IsCompleted = PatchDTO.IsCompleted.Value;

    return Results.Ok(task);
});

app.MapDelete("/tasks/{id:guid}", async (Guid id, AppDbContext db, CancellationToken ct) =>
{
    var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id, ct);
    if (task is null) return Results.NotFound();

    db.Tasks.Remove(task);
    await db.SaveChangesAsync(ct);
    return Results.Ok();
});


app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();
