using TimeToRESTFromTodo.Models;
using TimeToRESTFromTodo.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

var tasks = new List<TaskItem>();



app.MapGet("/tasks", () => tasks);

app.MapPost("/tasks", (CreateTaskRequest TaskRequest) =>
{
    TaskItem NewTask = new TaskItem(TaskRequest.Title, TaskRequest.Description);
    tasks.Add(NewTask);
    return Results.Created($"/tasks/{NewTask.Id}", NewTask);
});




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
