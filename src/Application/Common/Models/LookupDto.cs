using Konnichiwa.Application.Common.Mappings;
using Konnichiwa.Domain.Entities;

namespace Konnichiwa.Application.Common.Models;

// Note: This is currently just used to demonstrate applying multiple IMapFrom attributes.
public class LookupDto : IMapFrom<TodoList>, IMapFrom<TodoItem>
{
    public int Id { get; set; }

    public string? Title { get; set; }
}
