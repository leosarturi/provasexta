namespace TasksApi.Models
{
    public enum StatusTarefa
    {
        EmAndamento, 
        Concluida,   
        EmEspera     
    }

}

namespace TasksApi.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descricao { get; set; }
        public StatusTarefa Status { get; set; } = StatusTarefa.EmEspera;
        public DateTime Prazo { get; set; }
        public DateTime CreatedAt {get; set;}
    }
}

