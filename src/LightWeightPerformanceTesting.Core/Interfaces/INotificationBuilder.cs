using System.Collections.Generic;

namespace LightWeightPerformanceTesting.Core.Interfaces
{
    public interface INotificationBuilder
    {
        (List<string> emailToDistributionList, List<string> emailCcDistributionList, string subject, string body) Build(string notificationName);
    }
}
