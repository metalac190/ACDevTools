using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Deck <T>
{
    public event Action OnEmptyDeck = delegate { };     // fires when card count hits 0

    private List<T> _cards = new List<T>();
    public List<T> Cards
    {
        get { return _cards; }
        set { _cards = value; }
    }
    public int Count
    {
        get { return Cards.Count; }
    }

    public Deck(List<T> cards)
    {
        this._cards = cards;
    }

    public void Add(T card, bool onTop = true)
    {
        if (onTop)
        {
            _cards.Insert(0, card);
        }
        else
        {
            _cards.Add(card);
        }
    }
    public void Add(List<T> cards, bool onTop = true)
    {
        if (onTop)
        {
            _cards.InsertRange(0, cards);
        }
        else
        {
            _cards.AddRange(cards);
        }
    }
    public void Add(T card, int positionIndex)
    {
        Debug.LogWarning("Add by index not yet implemented.");
    }

    public void Remove(int cardIndex)
    {
        if (cardIndex < 0 || cardIndex > Cards.Count - 1) { return; } // index doesn't fall within range
        // find out which card to remove
        Cards.RemoveAt(cardIndex);
    }

    public List<T> Draw(int numberToDraw, bool onTop = true)
    {
        if(numberToDraw <= 0) { return new List<T>(); }   // drawing 0 cards doesn't make sense

        // if we're asked to Draw too many, only draw what we can
        if(numberToDraw > _cards.Count)
        {
            numberToDraw = _cards.Count;
        }
        // draw the cards we're able to
        List<T> drawnCards = new List<T>();
        for (int i = 0; i < numberToDraw; i++)
        {
            drawnCards.Add(_cards[0]);
            _cards.RemoveAt(0);
        }
        // check if we've ran out of cards
        if(Cards.Count == 0)
        {
            OnEmptyDeck.Invoke();
        }

        return drawnCards;
    }

    public void Shuffle()
    {
        for (int i = _cards.Count - 1; i > 0; --i)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T card = _cards[j];
            _cards[j] = _cards[i];
            _cards[i] = card;
        }
    }
}

