using Microsoft.EntityFrameworkCore;
using TasksApi.Models;


var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Configuração do banco de dados SQLite
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlite("Data Source=task.db"));
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

var app = builder.Build();

// Ativa o CORS
app.UseCors("AllowAll");

// Suas rotas (preservadas)
app.MapGetRoutes();
app.MapPostRoutes();
app.MapPutRoutes();
app.MapDeleteRoutes();

void PopularBancoDeDados(IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<TaskContext>();

    context.Database.Migrate(); // Aplica migrations automaticamente
    
    if (!context.Tasks.Any())
    {
        var taskIniciais = new List<TaskModel>
{
    new() { Id = 1, Name = "Trabalho para dia 30/05", Descricao = "Trabalho sobre API para entregar no dia 30 de maio", Status = StatusTarefa.EmAndamento, Prazo = DateTime.ParseExact("30/05/2025", "dd/MM/yyyy", null),CreatedAt = DateTime.Now },

    new() { Id = 2, Name = "Estudar para prova de matemática", Descricao = "Revisar os capítulos 5 a 8 do livro", Status = StatusTarefa.EmEspera, Prazo = DateTime.ParseExact("02/06/2025", "dd/MM/yyyy", null),CreatedAt = DateTime.Now },

    new() { Id = 3, Name = "Atualizar currículo", Descricao = "Adicionar novos cursos e experiências recentes", Status = StatusTarefa.Concluida, Prazo = DateTime.ParseExact("25/05/2025", "dd/MM/yyyy", null),CreatedAt = DateTime.Now },

    new() { Id = 4, Name = "Organizar arquivos do computador", Descricao = "Limpar área de trabalho e pastas de downloads", Status = StatusTarefa.EmAndamento, Prazo = DateTime.ParseExact("05/06/2025", "dd/MM/yyyy", null),CreatedAt = DateTime.Now },

    new() { Id = 5, Name = "Enviar e-mail para professor", Descricao = "Tirar dúvidas sobre o projeto de TCC", Status = StatusTarefa.Concluida, Prazo = DateTime.ParseExact("26/05/2025", "dd/MM/yyyy", null),CreatedAt = DateTime.Now },

    new() { Id = 6, Name = "Fazer compras da semana", Descricao = "Comprar frutas, legumes, arroz, feijão e carne", Status = StatusTarefa.EmEspera, Prazo = DateTime.ParseExact("01/06/2025", "dd/MM/yyyy", null),CreatedAt = DateTime.Now },

    new() { Id = 7, Name = "Ler artigo científico", Descricao = "Ler artigo sobre inteligência artificial e anotar pontos principais", Status = StatusTarefa.EmAndamento, Prazo = DateTime.ParseExact("29/05/2025", "dd/MM/yyyy", null),CreatedAt = DateTime.Now },

    new() { Id = 8, Name = "Backup do projeto", Descricao = "Fazer backup no GitHub do sistema de biblioteca", Status = StatusTarefa.Concluida, Prazo = DateTime.ParseExact("27/05/2025", "dd/MM/yyyy", null),CreatedAt = DateTime.Now },

    new() { Id = 9, Name = "Agendar consulta médica", Descricao = "Consulta de rotina no clínico geral", Status = StatusTarefa.EmEspera, Prazo = DateTime.ParseExact("03/06/2025", "dd/MM/yyyy", null), CreatedAt = DateTime.Now},

    new() { Id = 10, Name = "Montar apresentação de slides", Descricao = "Apresentação para seminário de quarta-feira", Status = StatusTarefa.EmAndamento, Prazo = DateTime.ParseExact("28/05/2025", "dd/MM/yyyy", null), CreatedAt = DateTime.Now }
};

        try
        {

            context.Tasks.AddRange(taskIniciais);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

// Executa o seed ao iniciar
PopularBancoDeDados(app);

app.Run();
