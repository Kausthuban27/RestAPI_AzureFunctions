using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Todo_CRUD.Interface;
using Todo_CRUD.Model;
using Todo_CRUD.Service;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<ITodo, TodoService>();
        var configuration = context.Configuration;
        string connectionString = configuration.GetSection("ConnectionStrings:defaultConnection").Value!;
        services.AddDbContext<TodoContext>(options =>
            options.UseSqlServer(connectionString));
    })
    .Build();

host.Run();
