using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinionController : MonoBehaviour
{
    public Card backupMinionInfo;

    public int health = -1;
    public int attack = -1;
    public bool isAsleep = true;
    public Sprite artwork;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;
    public GameObject sleepIndicator;
    public Image artworkImage;

    private void Start()
    {
        if (health == -1 || attack == -1 || artwork == null)
        {
            GetMinionInfoFromBackup(); 
            UpdateMinionInfo();
        }
        else
        {
            UpdateMinionInfo();
        }
    }

    public void GetMinionInfoFromBackup()
    {
        health = backupMinionInfo.health;
        attack = backupMinionInfo.attack;
        artwork = backupMinionInfo.cardSprite;
    }

    public void UpdateMinionInfo()
    {
        healthText.text = health.ToString();
        attackText.text = attack.ToString();
        sleepIndicator.SetActive(isAsleep);
        artworkImage.sprite = artwork;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateMinionInfo();
        if (health <= 0) { Die(); }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
