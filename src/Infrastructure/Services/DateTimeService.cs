using Konnichiwa.Application.Common.Interfaces;

namespace Konnichiwa.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
