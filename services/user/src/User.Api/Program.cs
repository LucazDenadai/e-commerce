var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/users/health", (HttpContext ctx) =>
{
    return Results.Ok(new
    {
        status = "ok",
        service = "user-service"
    });
});

app.MapGet("/users/profile", () =>
{
    // depois isso vem do banco / auth, por enquanto é mock
    return Results.Ok(new
    {
        id = "user-123",
        name = "John Doe",
        email = "john.doe@example.com"
    });
});

app.Run();

app.Run();

