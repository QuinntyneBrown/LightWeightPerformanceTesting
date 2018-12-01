using LightWeightPerformanceTesting.Core.Common;
using LightWeightPerformanceTesting.Core.DomainEvents;
using System;

namespace LightWeightPerformanceTesting.Core.Models
{
    public class Card: Entity
    {
        public Card(string name, CardType cardType)
            => Apply(new CardCreated(cardType,name, CardId));

        public Guid CardId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }     
        public CardType CardType { get; set; }   
		public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case CardCreated cardCreated:
                    Name = cardCreated.Name;
                    CardId = cardCreated.CardId;
                    CardType = cardCreated.CardType;
                    break;

                case CardNameChanged cardNameChanged:
                    Name = cardNameChanged.Name;
                    break;

                case CardRemoved cardRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new CardNameChanged(name));

        public void Remove()
            => Apply(new CardRemoved());
    }
}
