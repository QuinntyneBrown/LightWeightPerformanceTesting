using LightWeightPerformanceTesting.Core.Models;
using System;

namespace LightWeightPerformanceTesting.API.Features.Cards
{
    public class CardDto
    {        
        public Guid CardId { get; set; }
        public string Name { get; set; }
        public CardType CardType { get; set; }
        public static CardDto FromCard(Card card)
            => new CardDto
            {
                CardId = card.CardId,
                Name = card.Name,
                CardType = card.CardType
            };
    }
}
