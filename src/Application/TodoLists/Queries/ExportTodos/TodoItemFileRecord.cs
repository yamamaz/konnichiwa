using Konnichiwa.Application.Common.Mappings;
using Konnichiwa.Domain.Entities;

namespace Konnichiwa.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
