using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Button hit;
    [SerializeField] private Button stand;
    private Hand playerHand;
    private Hand dealerHand;
    [SerializeField] private Deck deck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hit.onClick.AddListener(() => playerHit());
        stand.onClick.AddListener(() => playerStand());
    } // Start

    // Update is called once per frame
    void Update()
    {
        
    } // Update

    // add card to player hand
    public void playerHit()
    {
        playerHand.addCard(deck.draw());
    } // playerHit

    // end player turn
    public void playerStand()
    {

    } // playerStand

    // add card to dealer hand
    public void dealerHit()
    {
        dealerHand.addCard(deck.draw());
    } // dealerHit

    // end dealer turn
    public void dealerStand()
    {

    } // dealerStand

} // GameManager
