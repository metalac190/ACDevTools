using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACDev.Systems;

namespace ACDev.Samples
{
    public class SampleDeckController : MonoBehaviour
    {
        [SerializeField] int _startingDeckSize = 10;

        [SerializeField] DeckSystem<SampleCard> _playerDeckSystem;

        private void Start()
        {
            CreatePlayerDeck(_startingDeckSize);
        }

        private void CreatePlayerDeck(int startingDeckSize)
        {
            Debug.Log("Create Player Deck");
            // first establish the group of cards to create the deck with
            List<SampleCard> startingDeckCards = new List<SampleCard>();
            // create the starting cards and add to the group
            for (int i = 0; i < startingDeckSize; i++)
            {
                SampleCard newCard = CreateCard("Card" + i, i);
                startingDeckCards.Add(newCard);
            }
            // now establish our deck with our starting card group
            _playerDeckSystem = new DeckSystem<SampleCard>(startingDeckCards);
            Debug.Log("Deck Draw Pile: " + _playerDeckSystem.DrawPile.Count);
        }

        private static SampleCard CreateCard(string cardName, int value)
        {
            SampleCard newCard = new SampleCard(cardName, value);

            return newCard;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Draw card");
                DrawCard();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Draw multiple cards");
                DrawCards();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Shuffle Cards");
                ShuffleHand();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Discard 1 Random");
                DiscardRandomFromHand();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Discard Hand");
                DiscardHand();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Print Cards");
                PrintCardsInHand();
            }
        }

        void PrintCardsInHand()
        {
            foreach(SampleCard card in _playerDeckSystem.Hand.Cards)
            {
                Debug.Log("Card 1 - Name: " + card.Name + ", Value: " + card.Value);
            }
        }

        void ShuffleHand()
        {
            _playerDeckSystem.Hand.Shuffle();
        }

        void DrawCard()
        {
            _playerDeckSystem.Draw();
        }

        void DrawCards()
        {
            _playerDeckSystem.Draw(3);
        }

        void DiscardRandomFromHand()
        {
            int randomCardIndex = UnityEngine.Random.Range(0, _playerDeckSystem.Hand.Count);
            _playerDeckSystem.Discard(randomCardIndex);
        }

        void DiscardHand()
        {
            _playerDeckSystem.DiscardHand();
        }
    }
}

