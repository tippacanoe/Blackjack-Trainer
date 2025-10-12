using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    private int handTotalValue;
    private List<Card> cards;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    } // Start

    // Update is called once per frame
    void Update()
    {
        
    } // Update

    private void updateHandTotalValue()
    {

        // initialize counter
        int total = 0;

        for (int i = 0; i < cards.Count; i++)
        {
            // check if ace
            if (cards[i].getRank() == 11)
            {
                // check if ace value of 11 busts player hand
                if (total + cards[i].getRank() > 21)
                {
                    total += 1;
                } else
                {
                    total += cards[i].getRank();
                }
            } else
            {
                total += cards[i].getRank();
            }
        }

        setHandTotalValue(total);

    } // updateHandTotalValue

    private void setHandTotalValue(int value)
    {
        handTotalValue = value;
    } // setHandTotalValue

    public int getHandTotalValue()
    {
        return handTotalValue;
    } // getHandtotalValue

    // add card to hand, and update hand total
    public void addCard(Card newCard)
    {
        cards.Add(newCard);
        updateHandTotalValue();
    } // addCard

    /*
    // list cards in hand
    public List<Card> getCards()
    {
        cards;
    } // getCards
    */
} // Hand
