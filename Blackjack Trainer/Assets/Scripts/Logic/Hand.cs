using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    [SerializeField] private HandDisplay handDisplay;
    [SerializeField] private GameManager manager;
    private int handTotalValue;
    private List<Card> cards;
    private bool bust;
    private bool isDealer;
    private bool hasSoftAce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(bool isDealer = false)
    {
        this.isDealer = isDealer;
        cards = new List<Card>();
        bust = false;
        handDisplay.Initialize(isDealer);
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
                hasSoftAce = false;
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
            if (total + 10 <= 21)
            {
                total += 10;
                hasSoftAce = true;
            }
            counter++;
        }

        if (total > 21)
        {
            bust = true;
        }

        handTotalValue = total;
        Debug.Log(total);
    } // updateHandTotalValue

    // returns the total value of the hand
    public int getHandTotalValue()
    {
        return handTotalValue;
    } // getHandtotalValue

    // returns the face up card that the dealer is using
    public Card getShownDealerCard()
    {
        return cards[1];
    } // getShownDealerCard

    public bool getHasSoftAce()
    {
        return hasSoftAce;
    } // hasAce

    // add card to hand, and update hand total
    public void addCard(Card newCard)
    {
        cards.Add(newCard);
        updateHandTotalValue();
        
        if(!isDealer || cards.Count > 0)
        {
            checkCardCount(newCard);
        }

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
        hasSoftAce = false;

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
            checkCardCount(cards[0]);
        }
    } // revealHiddenCard

    private void checkCardCount(Card card)
    {
        if(card.getRank() < 7)
        {
            manager.increaseCount();
        } else if (card.getRank() > 9)
        {
            manager.decreaseCount();
        }
    } // checkCardCount

} // Hand