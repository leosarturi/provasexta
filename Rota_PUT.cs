using TasksApi.Models;
using Microsoft.EntityFrameworkCore;

public static class ROTA_PUT
{
    public static void MapPutRoutes(this WebApplication app)
    {
        app.MapPut("/api/tasks/{id}", async (int id, TaskModel tarefaAtualizada, TaskContext db) =>
  {
      var tarefa = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id);

      if (tarefa == null)
          return Results.NotFound("Tarefa não encontrada.");

      tarefa.Name = tarefaAtualizada.Name;
      tarefa.Descricao = tarefaAtualizada.Descricao;
      tarefa.Status = tarefaAtualizada.Status;  // já é enum, pode atribuir direto
      tarefa.Prazo = tarefaAtualizada.Prazo;

      await db.SaveChangesAsync();

      return Results.Ok(tarefa);
  });
    }
}
