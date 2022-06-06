var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<EmailService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();

app.UseStaticFiles();

app.MapGet("/env", () =>
{
    return new
    {
        value = Environment.GetEnvironmentVariable("CurrentConfig")
    };
});

app.MapPost("/send", async (Email e, EmailService es) =>
{

    var res = await es.Send(e.To, "subject", e.Content);

    return res;
});

app.MapGet("/emailstatus/{id}", (Guid id) =>
{
    return new EmailDeliveryStatus(id, "delivered");
});

app.Run();

public record Email (string To, string Content);

public record EmailDeliveryStatus(Guid id, string status);
