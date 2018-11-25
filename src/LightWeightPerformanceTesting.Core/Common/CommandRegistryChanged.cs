using LightWeightPerformanceTesting.Core.Interfaces;

namespace LightWeightPerformanceTesting.Core.Common
{
    public class CommandRegistryChanged: ICommandRegistryChanged
    {
        public CommandRegistryChanged(string partition, string key)
        {
            Partition = partition;
            Key = key;
        }
        public string Partition { get; set; }
        public string Key { get; set; }
    }
}
