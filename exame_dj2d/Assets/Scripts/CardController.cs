using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public GameObject minionTemplate;

    public Card cardInfo;

    public int manaCost;
    public int attack;
    public int health;
    public Sprite artwork;
    public string description;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;
    public Image artworkImage;

    private void Start()
    {
        /*
        if (cardInfo != null)
        {
            GetCardInfo();
        }
        else
        {
            Destroy(this.gameObject);
        }
        */

        GetCardInfo();
        UpdateCardInfo();
    }
    public void GetCardInfo()
    {
        nameText.text = cardInfo.name;
        description = cardInfo.description;
        manaCost = cardInfo.manaCost;
        attack = cardInfo.attack;
        health = cardInfo.health;
        artwork = cardInfo.cardSprite;

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
        descriptionText.text = description;
        artworkImage.sprite = artwork;
    }

    public void PlayCard()
    {
        Transform playerBoard = GameObject.FindGameObjectWithTag("PlayerBoard").transform;

        GameObject minion = Instantiate(minionTemplate, playerBoard);
        minion.tag = "PlayerMinion";

        minion.GetComponent<MinionController>().health = this.health;
        minion.GetComponent<MinionController>().attack = this.attack;
        minion.GetComponent<MinionController>().artwork = this.artwork;

        Destroy(this.gameObject);
    }
}
