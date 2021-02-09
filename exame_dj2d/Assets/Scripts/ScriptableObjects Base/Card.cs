using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public new string name;
    [TextArea]
    public string description;
    [Range(0, 10)]
    public int manaCost;
    public int health;
    public int attack;

    public Sprite cardSprite;
}
