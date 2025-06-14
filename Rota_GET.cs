using TasksApi.Models;
using Microsoft.EntityFrameworkCore;

public static class ROTA_GET
{
    public static void MapGetRoutes(this WebApplication app)
    {
        // Mensagem inicial da API
        app.MapGet("/", () => "API de Tarefas em funcionamento!");

        // Buscar todas as tarefas e converter status string para enum
        app.MapGet("/api/tasks", async (TaskContext db) =>
        {
            var tasks = await db.Tasks.ToListAsync();

            var tasksComStatusEnum = tasks.Select(t => new
            {
                t.Id,
                t.Name,
                t.Descricao,
                t.Status,
                t.Prazo,
                t.CreatedAt
            });

            return Results.Ok(tasksComStatusEnum);
        });

        // Buscar tarefa por ID e converter status string para enum
        app.MapGet("/api/tasks/{id}", async (int id, TaskContext db) =>
        {
            var t = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (t == null) return Results.NotFound("Tarefa não encontrada.");

            var tarefaComStatusEnum = new
            {
                t.Id,
                t.Name,
                t.Descricao,
                t.Status,
                t.Prazo
            };

            return Results.Ok(tarefaComStatusEnum);
        });

    }
}
