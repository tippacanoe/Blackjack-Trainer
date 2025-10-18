using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Button hit;
    [SerializeField] private Button stand;
    [SerializeField] private Button playAgain;
    [SerializeField] private Button menu;
    [SerializeField] private Deck deck;
    private Hand playerHand;
    private Hand dealerHand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hit.onClick.AddListener(() => playerHit());
        stand.onClick.AddListener(() => playerStand());
        playAgain.onClick.AddListener(() => startRound());
        menu.onClick.AddListener(() => returnToMenu());

        // hide the post-round buttons
        setPostRoundButtonStates(false);

        // start game
        startRound();
    } // Start

    // add card to player hand
    private void playerHit()
    {
        Debug.Log("Player has: ");
        playerHand.addCard(deck.draw());
        if(playerHand.isBust())
        {
            // end player's turn
            playDealer();
        }
    } // playerHit

    // end player turn
    private void playerStand()
    {
        playDealer();
    } // playerStand

    // add card to dealer hand
    private void dealerHit()
    {
        Debug.Log("Dealer has: ");
        dealerHand.addCard(deck.draw());
        if (dealerHand.isBust())
        {
            endRound();
        }
    } // dealerHit

    // runs the logic for casino style dealer
    private void playDealer()
    {
        // draw to 17
        while(dealerHand.getHandTotalValue() < 17)
        {
            dealerHit();
        }

        // dealer stands once at 17 or higher, ending the round
        endRound();
    } // playDealer

    // determine hand winner
    private void compareHands()
    {
        if ((playerHand.isBust() && dealerHand.isBust()) || playerHand.getHandTotalValue() == dealerHand.getHandTotalValue())
        {
            // draw
            Debug.Log("Hand is a draw");
        } else if (dealerHand.isBust() || (!playerHand.isBust() && playerHand.getHandTotalValue() > dealerHand.getHandTotalValue()))
        {
            // player wins
            Debug.Log("Player wins");
        } else
        {
            // dealer wins
            Debug.Log("Dealer wins");
        }
    } // compareHands

    // starts a new round of the game
    private void startRound()
    {

        // hide post-round buttons
        setPostRoundButtonStates(false);

        // verify there are enough cards to play another hand
        if(deck.cardsRemaining() < 10)
        {
            deck.shuffle();
        }

        // reset state of both hands
        playerHand.resetHand();
        dealerHand.resetHand();

        // deal starting hands
        for (int i = 0; i < 2; i++)
        {
            playerHit();
            dealerHit();
        }
    } // startRound

    // ends the current round of the game
    private void endRound()
    {
        compareHands();

        // ask if player wants to play again
        setPostRoundButtonStates(true);
    } // endRound

    // show or hide post-round buttons
    private void setPostRoundButtonStates(bool state)
    {
        playAgain.gameObject.SetActive(state);
        menu.gameObject.SetActive(state);
    } // setButtonStates

    // changes game scene to main menu
    private void returnToMenu()
    {
        Debug.Log("Returning to Menu");
    } // returnToMenu

} // GameManager
