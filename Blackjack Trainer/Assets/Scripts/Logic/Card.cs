using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]

public class Card : ScriptableObject
{
    [SerializeField] private string suit;
    [SerializeField] private int rank;
    [SerializeField] private Sprite sprite;
    
    public int getRank()
    {
        return rank;
    } // getRank

    public Sprite getSprite()
    {
        return sprite;
    } // getSprite

} // Card
