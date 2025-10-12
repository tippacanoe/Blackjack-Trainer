using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
//using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    [SerializeField] private Card[] allCards;
    private List<Card> cards;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shuffle();
    } // Start

    // Update is called once per frame
    void Update()
    {
        
    } // Update

    // grab random card data from deck, remove card from deck, and then return that card
    public Card draw()
    {
        Card card = cards[Random.Range(0, cards.Count)];
        cards.Remove(card);
        return card;
    } // draw

    // reset deck
    public void shuffle()
    {
        cards.Clear();
        for (int i = 0; i < allCards.Length; i++)
        {
            cards.Add(allCards[i]);
        }

    } // shuffle
} // Deck
