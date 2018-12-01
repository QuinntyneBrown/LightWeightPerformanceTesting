using System;
using LightWeightPerformanceTesting.Core.Models;

namespace LightWeightPerformanceTesting.Core.DomainEvents
{
    public class CardCreated: DomainEvent
    {
        public CardCreated(CardType cardType, string name, Guid cardId)
        {
            Name = name;
            CardId = cardId;
            CardType = cardType;
        }
        public string Name { get; set; }
        public Guid CardId { get; set; }
        public CardType CardType { get; set; }
    }
}
