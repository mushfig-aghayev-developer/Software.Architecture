using OptionsPattern.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
 * When you want to bind configuration to a class and register it as a singleton service, you can do so using the following code:
 * From Controller you can inject AccountServiceConfig directly instead of IOptions<AccountServiceConfig>;
 builder.Services.AddSingleton(y =>
{
    AccountServiceConfig accountServiceConfig = new AccountServiceConfig();
    builder.Configuration.GetSection(AccountServiceConfig.AccountService).Bind(accountServiceConfig);
    return accountServiceConfig;
});
 */

builder.Services.Configure<AccountServiceConfig>(builder.Configuration.GetSection(AccountServiceConfig.AccountService));

builder.Services.AddOptions<AccountServiceConfig>()
    .Bind(builder.Configuration.GetSection(AccountServiceConfig.AccountService))
    .ValidateDataAnnotations()
    .ValidateOnStart() // Validate configuration on application startup
    .Validate(u =>
    {
        if (!u.Url.StartsWith("https:"))
            return false;
        return true;

    }, "Url must start : https://");


var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the root path
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
