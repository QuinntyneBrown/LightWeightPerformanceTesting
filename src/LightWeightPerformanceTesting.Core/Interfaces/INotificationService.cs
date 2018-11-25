using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightWeightPerformanceTesting.Core.Interfaces
{
    public interface INotificationService
    {
        Task Send(List<string> emailToDistributionList, List<string> emailCcDistributionList, string subject, string body);
    }
}
