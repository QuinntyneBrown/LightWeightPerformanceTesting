using LightWeightPerformanceTesting.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace LightWeightPerformanceTesting.Core.Common
{
    public class FixedDateTime: IDateTime
    {
        private readonly IConfiguration _configuration;
        public FixedDateTime(IConfiguration configuration)
            => _configuration = configuration;

        public DateTime UtcNow => DateTime.Parse(_configuration["UtcNow"]);
    }
}
