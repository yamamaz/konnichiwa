using System.Globalization;
using Konnichiwa.Application.Common.Interfaces;
using Konnichiwa.Application.TodoLists.Queries.ExportTodos;
using Konnichiwa.Infrastructure.Files.Maps;
using CsvHelper;

namespace Konnichiwa.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
