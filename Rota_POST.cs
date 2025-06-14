using TasksApi.Models;

public static class ROTA_POST
{
    public static void MapPostRoutes(this WebApplication app)
    {
        app.MapPost("/api/tasks", async (TaskModel novaTask, TaskContext db) =>
        {
            db.Tasks.Add(novaTask);
            await db.SaveChangesAsync();
            return Results.Created($"/api/tasks/{novaTask.Id}", novaTask);
        });
    }
}
