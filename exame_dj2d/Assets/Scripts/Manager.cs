using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    //Control Variables
    [Range(0,10)]
    public int playerMana;
    [Range(1, 10)]
    public int playerMaxMana;
    public int playerHealth;

    //Object Variables
    //public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerManaText;
    public Image[] playerManaCrystals;

    #region Singleton
    private static Manager instance;
    public static Manager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    private void Update()
    {
        //UpdateManaDisplay
        playerManaText.text = $"{playerMana}/{playerMaxMana}";
        for (int i = 0; i < playerManaCrystals.Length; i++)
        {
            if (playerMana >= i+1)
            {
                playerManaCrystals[i].color = new Color(playerManaCrystals[i].color.r, playerManaCrystals[i].color.g, playerManaCrystals[i].color.b, 1f);
            }
            else
            {
                playerManaCrystals[i].color = new Color(playerManaCrystals[i].color.r, playerManaCrystals[i].color.g, playerManaCrystals[i].color.b, 0.3f);
            }

            if (playerMaxMana < i+1)
            {
                playerManaCrystals[i].color = new Color(playerManaCrystals[i].color.r, playerManaCrystals[i].color.g, playerManaCrystals[i].color.b, 0f);
            }
        }

        //UpdateHealthDisplay
    }
}
