var builder = WebApplication.CreateBuilder(args);

// Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adiciona suporte a controles
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// adiciona suporte a razor e paginas com controladores
builder.Services.AddRazorComponents();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura o Swagger para ser exibido na rota /swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");

        c.DocumentTitle = "Api de Simulação de financias";
        
    });
}


// Configura a pagina principal do servidor
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Configura o Https
app.UseHttpsRedirection();

// Mapeia todos os controladores
app.MapControllers();

// Configura o uso de sessões
app.UseAuthorization();

// roda a aplicação
app.Run();