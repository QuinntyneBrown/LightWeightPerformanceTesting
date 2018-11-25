using System;

namespace LightWeightPerformanceTesting.Core.DomainEvents
{
    public class DashboardCardRemovedFromDashboard: DomainEvent
    {
        public DashboardCardRemovedFromDashboard(Guid dashboardCardId)
            => DashboardCardId = dashboardCardId;

        public Guid DashboardCardId { get; set; }
    }
}
