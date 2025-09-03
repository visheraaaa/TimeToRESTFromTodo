using TimeToRESTFromTodo.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

var tasks = new List<TaskItem>();



app.MapGet("/", () =>
{
    return Results.Ok("everything is OK");
});

app.MapPost("/tasks", (TaskItem task) =>
{
    task.Id = tasks.Count + 1;
    tasks.Add(task);
    return Results.Created($"/tasks/{task.Id}", task);
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
