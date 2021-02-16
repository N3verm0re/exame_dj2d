using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinionController : MonoBehaviour
{
    public int health;
    public int attack;
    public bool isAsleep = true;
    public Sprite artwork;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;
    public GameObject sleepIndicator;
    public Image artworkImage;

    private void Start()
    {
        UpdateMinionInfo();
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
