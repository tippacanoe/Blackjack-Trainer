using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
//using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    [SerializeField] private Card[] allCards;
    private List<Card> cards;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize()
    {
        cards = new List<Card>();
        resetDeck();
    } // Initialize

    // grab random card data from deck, remove card from deck, and then return that card
    public Card draw()
    {
        Card card = cards[Random.Range(0, cards.Count)];
        cards.Remove(card);
        return card;
    } // draw

    // reset deck state and add all base cards to deck
    public void shuffle()
    {
        if (cards != null)
        {
            cards.Clear();
        }

        resetDeck();

    } // shuffle

    // adds all base cards to deck
    private void resetDeck()
    {
        for (int i = 0; i < allCards.Length; i++)
        {
            cards.Add(allCards[i]);
        }
    } // resetDeck

    // check how many cards are left in the deck
    public int cardsRemaining()
    {
        return cards.Count;
    } // cardsRemaining

} // Deck
