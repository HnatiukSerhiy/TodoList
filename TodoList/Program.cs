using TodoList.interfaces;
using TodoList.DataAccess;
using GraphQL;
using GraphQL.Types;
using TodoList.GraphQLBlocks;
using GraphQL.MicrosoftDI;
using GraphQL.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton<IDataProviderResolver, DataProviderResolver>();

builder.Services.AddTransient(option => new TodoSqlDataProvider(connectionString));
builder.Services.AddTransient(option => new CategorySqlDataProvider(connectionString));
builder.Services.AddTransient<TodoXmlDataProvider>();
builder.Services.AddTransient<CategoryXmlDataProvider>();

builder.Services.AddScoped<ISchema, AppSchema>(services => new AppSchema(new SelfActivatingServiceProvider(services)));

builder.Services.AddGraphQL(option =>
{
    option.EnableMetrics = true;
}).AddSystemTextJson();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseGraphQL<ISchema>();
app.UseGraphQLAltair();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern:"{controller}/{action}/{id?}",
    defaults: new { controller = "Home", action = "Index"}
    );

app.Run();