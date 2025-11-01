using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    [SerializeField] private HandDisplay handDisplay;

    private int handTotalValue;
    private List<Card> cards;
    private bool bust;
    private bool isDealer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(bool isDealer = false)
    {
        this.isDealer = isDealer;
        cards = new List<Card>();
        bust = false;
    } // Initialize

    private void updateHandTotalValue()
    {
        // initialize counter
        int total = 0;
        int aceCounter = 0;

        for (int i = 0; i < cards.Count; i++)
        {
            // check if ace
            if (cards[i].getRank() == 11)
            {
                aceCounter++;
                total += 1;
            }
            else
            {
                total += cards[i].getRank();
            }
        }

        int counter = 0;
        while (counter < aceCounter)
        {
            // add max ace value to hand if it does not bust the hand
            if (total + 10 < 21)
            {
                total += 10;
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

        // Add card visual to display
        if (handDisplay != null)
        {
            handDisplay.addCardVisual(newCard);
        }
    } // addCard

    // resets the state of the hand
    public void resetHand()
    {
        cards.Clear();
        bust = false;

        // Clear the visual display
        if (handDisplay != null)
        {
            handDisplay.clearCards();
        }
    } // resetHand

    // return bust state of hand
    public bool isBust()
    {
        return bust;
    } // isBust

    // Reveal the dealer's hidden first card
    public void revealHiddenCard()
    {
        if (isDealer && handDisplay != null && cards.Count > 0)
        {
            handDisplay.revealDealerCard(cards[0]);
        }
    } // revealHiddenCard
} // Hand