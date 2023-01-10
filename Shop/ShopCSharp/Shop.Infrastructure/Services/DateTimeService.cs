using Shop.Application.Common.Interfaces;
using System;

namespace Shop.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
