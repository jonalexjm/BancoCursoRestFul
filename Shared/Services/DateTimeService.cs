using Application.Interfaces;
using System;

namespace Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        
        DateTime IDateTimeService.NowUtc { get => DateTime.UtcNow; set => throw new NotImplementedException(); }
    }
}
