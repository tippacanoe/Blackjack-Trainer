using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HandDisplay : MonoBehaviour
{

    [SerializeField] private Transform cardContainer;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Sprite cardBackSprite;
    private float cardSpacing = 3f;
    private List<GameObject> cardObjects = new List<GameObject>();
    private bool isDealer;

    public void Initialize(bool isDealer)
    {
        this.isDealer = isDealer;
    } // Initialize

    // Add a card visual to the display
    public void addCardVisual(Card card)
    {
        GameObject cardObj = Instantiate(cardPrefab, cardContainer);
        Image cardImage = cardObj.GetComponent<Image>();

        // For dealer's first card, show back of card
        if (isDealer && cardObjects.Count == 0)
        {
            cardImage.sprite = cardBackSprite;
        }
        else
        {
            cardImage.sprite = card.getSprite();
        }

        cardObjects.Add(cardObj);
        positionCards();
    } // addCardVisual

    // Reveal the dealer's first card
    public void revealDealerCard(Card card)
    {
        if (isDealer)
        {
            Image cardImage = cardObjects[0].GetComponent<Image>();
            cardImage.sprite = card.getSprite();
        }
    } // revealDealerCard

    // Position all cards with spacing
    private void positionCards()
    {
        float totalWidth = (cardObjects.Count - 1) * cardSpacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < cardObjects.Count; i++)
        {
            RectTransform rect = cardObjects[i].GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(startX + (i * cardSpacing), 0);
        }
    } // positionCards

    // Clear all card visuals
    public void clearCards()
    {
        foreach (GameObject cardObj in cardObjects)
        {
            Destroy(cardObj);
        }
        cardObjects.Clear();
    } // clearCards

} // HandDisplay