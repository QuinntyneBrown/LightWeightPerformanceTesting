using System.Collections.Generic;

namespace LightWeightPerformanceTesting.Core.Interfaces
{
    public interface ICommand<TResponse> 
    {
        string Key { get; }        
        IEnumerable<string> SideEffects { get; }
    }
}
