using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    private int handTotalValue;
    private List<Card> cards;
    private bool bust;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize()
    {
        cards = new List<Card>();
        bust = false;
    } // Initialize

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

        if (total > 21)
        {
            bust = true;
        }

        handTotalValue = total;
        Debug.Log(total);

    } // updateHandTotalValue

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

    // resets the state of the hand
    public void resetHand()
    {
        cards.Clear();
        bust = false;
    } // resetHand

    // return bust state of hand
    public bool isBust()
    {
        return bust;
    } // isBust
} // Hand
