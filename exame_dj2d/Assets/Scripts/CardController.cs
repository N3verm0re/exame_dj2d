using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public Card cardInfo;

    public int manaCost;
    public int attack;
    public int health;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;
    public Image artwork;

    private void Start()
    {
        if (cardInfo != null)
        {
            GetCardInfo();
        }
        else
        {
            Destroy(this.gameObject);
        }

        TakeDamage(2);
    }
    public void GetCardInfo()
    {
        nameText.text = cardInfo.name;
        descriptionText.text = cardInfo.description;
        manaText.text = cardInfo.manaCost.ToString();
        manaCost = cardInfo.manaCost;
        attackText.text = cardInfo.attack.ToString();
        attack = cardInfo.attack;
        healthText.text = cardInfo.health.ToString();
        health = cardInfo.health;
        artwork.sprite = cardInfo.cardSprite;

        if (cardInfo.name.Length > 11)
        {
            nameText.fontSize = 28f;
        }
        else
        {
            nameText.fontSize = 35f;
        }
    }

    public void UpdateCardInfo()
    {
        manaText.text = manaCost.ToString();
        attackText.text = attack.ToString();
        healthText.text = health.ToString();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateCardInfo();
        if (health <= 0) { Die(); }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
