using BlackJack.DataAccess.Entities;
using BlackJack.DataAccess.Entities.Enums;
using System;
using System.Collections.Generic;

namespace BlackJack.BusinessLogic.Helpers
{
    public class DealCardsHelper
    {
        private const byte _maxCardsPoints = 10;
        public  Random random = new Random();

        public void GetAllCards(ref List<Card> deck)
        {
            long idIterator = 0;
            foreach (var suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (var faceValue in Enum.GetValues(typeof(CardValue)))
                {
                    if ((CardSuit)suit != CardSuit.None && (CardValue)faceValue != CardValue.None)
                    {
                        idIterator++;
                        Card card = new Card
                        {
                            CardSuit = (CardSuit)suit,
                            FaceValue = (CardValue)faceValue,
                            CardPoints = GetCardPoints((CardValue)faceValue),
                            Id = idIterator
                        };
                        card.Name = $"{card.FaceValue} of {card.CardSuit}";
                        deck.Add(card);
                    }
                }
            }
        }

        public Card GetCard(List<Card> deckOfCard)
        {          
            var cardToUser = random.Next(0, deckOfCard.Count);
            Card cardToAdd = deckOfCard[cardToUser];
            return cardToAdd;
        }

        private byte GetCardPoints(CardValue cardValue)
        {
            if (cardValue == CardValue.Jack || cardValue == CardValue.Queen || cardValue == CardValue.King)
            {
                return _maxCardsPoints;
            }
            return (byte)cardValue;
        }
    }
}
