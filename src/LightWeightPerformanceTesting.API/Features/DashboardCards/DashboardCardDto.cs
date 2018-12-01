using LightWeightPerformanceTesting.API.Features.Cards;
using LightWeightPerformanceTesting.Core.Models;
using Newtonsoft.Json;
using System;

namespace LightWeightPerformanceTesting.API.Features.DashboardCards
{
    public class DashboardCardDto
    {        
        public Guid DashboardCardId { get; set; }
        public Guid DashboardId { get; set; }
        public Guid CardId { get; set; }
        public CardDto Card { get; set; }
        public dynamic Options { get; set; }
        public static DashboardCardDto FromDashboardCard(DashboardCard dashboardCard) {
            var dto = new DashboardCardDto
            {
                DashboardCardId = dashboardCard.DashboardCardId,
                DashboardId = dashboardCard.DashboardId,
                CardId = dashboardCard.CardId
            };

            switch(dashboardCard.CardId) {
                default:
                dto.Options = JsonConvert.DeserializeObject<DashboardCardLightWeightPerformanceTestOptionsDto>(dashboardCard.Options);
                break;
            }

            return dto;
        }

        public class OptionsDto
        {
            public int Top { get; set; } = 1;
            public int Left { get; set; } = 1;
            public int Height { get; set; } = 1;
            public int Width { get; set; } = 1;
        }
    }
}
