using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public Card cardInfo;

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
            UpdateInfo();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void UpdateInfo()
    {
        nameText.text = cardInfo.name;
        descriptionText.text = cardInfo.description;
        manaText.text = cardInfo.manaCost.ToString();
        attackText.text = cardInfo.attack.ToString();
        healthText.text = cardInfo.health.ToString();
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
}
