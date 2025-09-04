using TimeToRESTFromTodo.Models;
using TimeToRESTFromTodo.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

var tasks = new List<TaskItem>();



app.MapGet("/tasks", () => tasks);

app.MapGet("/tasks/{id}", (Guid id) =>
    tasks.FirstOrDefault(t => t.Id == id) is TaskItem task 
    ? Results.Ok(task) 
    : Results.NotFound());


app.MapPost("/tasks", (TaskCreateDTO TaskRequest) =>
{
    TaskItem NewTask = new TaskItem(TaskRequest.Title, TaskRequest.Description);
    tasks.Add(NewTask);
    return Results.Created($"/tasks/{NewTask.Id}", NewTask);
});


app.MapPatch("/task/{id}", (Guid id, TaskPatchDTO PatchDTO) =>
{
    TaskItem? task = tasks.FirstOrDefault(t => t.Id == id);
    if (task is null) Results.NotFound();

    if (PatchDTO.Title is not null)
        task.Title = PatchDTO.Title;

    if (PatchDTO.Description is not null)
        task.Description = PatchDTO.Description;

    if (PatchDTO.IsCompleted.HasValue)
        task.IsCompleted = PatchDTO.IsCompleted.Value;

    return Results.Ok(task);
});



app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();




//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();

//app.MapStaticAssets();
//app.MapRazorPages()
//   .WithStaticAssets();

app.Run();
