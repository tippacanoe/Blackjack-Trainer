using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]

public class Card : ScriptableObject
{
    [SerializeField] private string suit;
    [SerializeField] private int rank;
    
    public int getRank()
    {
        return rank;
    } // getRank

} // Card
