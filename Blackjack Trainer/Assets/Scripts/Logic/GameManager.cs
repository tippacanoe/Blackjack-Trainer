using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button hit;
    [SerializeField] private Button stand;
    [SerializeField] private Button playAgain;
    [SerializeField] private Button menu;
    [SerializeField] private Deck deckPrefab;
    [SerializeField] private Hand playerHand;
    [SerializeField] private Hand dealerHand;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private TextMeshProUGUI handTotalText;
    private Deck deck;
    private int cardCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hit.onClick.AddListener(() => playerHit(false));
        stand.onClick.AddListener(() => playerStand());
        playAgain.onClick.AddListener(() => startRound());
        menu.onClick.AddListener(() => returnToMenu());

        // hide the post-round buttons
        setPostRoundButtonStates(false);

        // initialize data structures
        deck = Instantiate(deckPrefab).GetComponent<Deck>();
        deck.Initialize();
        playerHand.Initialize(false); // false = not dealer
        dealerHand.Initialize(true);  // true = is dealer

        // set card count to 0
        cardCount = 0;

        // start game
        startRound();
    } // Start

    // add card to player hand
    private void playerHit(bool roundStarting)
    {
        playerHand.addCard(deck.draw());
        handTotalText.text = "Hand Total: " + playerHand.getHandTotalValue();

        if (!roundStarting && ModeTracker.getCurrentMode() == ModeTracker.mode.strategy)
        {
            updateStrategyTooltip();
        } else if (ModeTracker.getCurrentMode() == ModeTracker.mode.counting) {
            updateStrategyTooltip();
        }

        // if player is bust or has 21, move on to dealer's turn
        if (playerHand.isBust() || playerHand.getHandTotalValue() == 21)
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
        dealerHand.addCard(deck.draw());
        if (dealerHand.isBust())
        {
            endRound();
        }
    } // dealerHit

    // runs the logic for casino style dealer
    private void playDealer()
    {

        // hide strategy tooltip since player turn is over
        if (ModeTracker.getCurrentMode() == ModeTracker.mode.strategy) { tooltipText.gameObject.SetActive(false); }
        

        // Reveal dealer's hidden card before playing
        dealerHand.revealHiddenCard();

        // draw to 17
        while (dealerHand.getHandTotalValue() < 17)
        {
            dealerHit();
        }

        // dealer stands once at 17 or higher, ending the round
        endRound();
    } // playDealer

    // determine hand winner
    private void compareHands()
    {
        winnerText.gameObject.SetActive(true);

        if ((playerHand.isBust() && dealerHand.isBust()) || playerHand.getHandTotalValue() == dealerHand.getHandTotalValue())
        {
            // draw
            winnerText.text = "Hand is a draw";
        }
        else if (dealerHand.isBust() || (!playerHand.isBust() && playerHand.getHandTotalValue() > dealerHand.getHandTotalValue()))
        {
            // player wins
            winnerText.text = "Player wins";
        }
        else
        {
            // dealer wins
            winnerText.text = "Dealer wins";
        }
    } // compareHands

    // starts a new round of the game
    private void startRound()
    {
        // hide post-round buttons
        setPostRoundButtonStates(false);

        // hide winner text
        winnerText.gameObject.SetActive(false);

        // show strategy tooltip
        tooltipText.gameObject.SetActive(true);

        // verify there are enough cards to play another hand
        if (deck.cardsRemaining() < 10)
        {
            deck.shuffle();
            cardCount = 0;
        }

        // reset state of both hands
        playerHand.resetHand();
        dealerHand.resetHand();

        // deal starting hands
        for (int i = 0; i < 2; i++)
        {
            playerHit(true);
            dealerHit();
        }
        
        updateStrategyTooltip();

    } // startRound

    // ends the current round of the game and asks if the player wants to play again
    private void endRound()
    {
        // Make sure dealer's card is revealed
        //dealerHand.revealHiddenCard();
        updateStrategyTooltip();
        compareHands();
        setPostRoundButtonStates(true);
    } // endRound

    // show or hide post-round buttons
    private void setPostRoundButtonStates(bool state)
    {
        playAgain.gameObject.SetActive(state);
        menu.gameObject.SetActive(state);
        hit.gameObject.SetActive(!state);
        stand.gameObject.SetActive(!state);
    } // setButtonStates

    // changes game scene to main menu
    private void returnToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    } // returnToMenu

    // evaluates whether the player is advised to hit or stand based on the dealer's upcard and their total
    private void updateStrategyTooltip()
    {
        if (ModeTracker.getCurrentMode() == ModeTracker.mode.strategy)
        {
            string suggestion = "";
            string dealerCardValue = "";
            string scoreType = "";
            string playerHandValue = "";

            // soft totals
            if (playerHand.getHasSoftAce())
            {
                scoreType = "soft";

                // dealer has high card
                if (dealerHand.getShownDealerCard().getRank() >= 9)
                {
                    dealerCardValue = "high";
                    if (playerHand.getHandTotalValue() >= 19)
                    {
                        playerHandValue = "high";
                        suggestion = "stand";
                    }
                    else
                    {
                        playerHandValue = "low";
                        suggestion = "hit";
                    }
                }
                else
                {
                    dealerCardValue = "low";
                    if (playerHand.getHandTotalValue() >= 18)
                    {
                        playerHandValue = "high";
                        suggestion = "stand";
                    }
                    else
                    {
                        playerHandValue = "low";
                        suggestion = "hit";
                    }
                }
            }
            // hard totals
            else
            {
                scoreType = "hard";

                // dealer has high card
                if (dealerHand.getShownDealerCard().getRank() >= 7)
                {
                    dealerCardValue = "high";
                    if (playerHand.getHandTotalValue() >= 17)
                    {
                        playerHandValue = "high";
                        suggestion = "stand";
                    }
                    else
                    {
                        playerHandValue = "low";
                        suggestion = "hit";
                    }
                }
                // dealer has middle card
                else if (dealerHand.getShownDealerCard().getRank() >= 4 && dealerHand.getShownDealerCard().getRank() <= 6)
                {
                    dealerCardValue = "medium";
                    if (playerHand.getHandTotalValue() >= 12)
                    {
                        playerHandValue = "high";
                        suggestion = "stand";
                    }
                    else
                    {
                        playerHandValue = "low";
                        suggestion = "hit";
                    }
                }
                // dealer has low card
                else
                {
                    dealerCardValue = "low";
                    if (playerHand.getHandTotalValue() >= 13)
                    {
                        playerHandValue = "high";
                        suggestion = "stand";
                    }
                    else
                    {
                        playerHandValue = "low";
                        suggestion = "hit";
                    }
                }

            }

            // place suggestion into on screen tooltip
            tooltipText.text = "Player Score Type: " + scoreType + "\nDealer known value: " + dealerCardValue + "\nPlayer Hand Value: " + playerHandValue + "\nSuggestion: " + suggestion;

        } else if (ModeTracker.getCurrentMode() == ModeTracker.mode.counting)
        {
            tooltipText.text = "Count: " + cardCount;
        }
    } // updateStrategyTooltip

    public void increaseCount()
    {
        cardCount++;
    } // increaseCount

    public void decreaseCount()
    {
        cardCount--;
    } // decreaseCount

} // GameManager