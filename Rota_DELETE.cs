using TasksApi.Models;
using Microsoft.EntityFrameworkCore;

public static class ROTA_DELETE
{
    public static void MapDeleteRoutes(this WebApplication app)
    {
        app.MapDelete("/api/tasks/{id}", async (int id, TaskContext db) =>
        {
            var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
                return Results.NotFound("Tarefa não encontrada.");

            db.Tasks.Remove(task);
            await db.SaveChangesAsync();

            return Results.Ok("Tarefa excluída com sucesso.");
        });
    }
}
