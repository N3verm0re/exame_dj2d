using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Deck playerDeck;
    public GameObject cardTemplate;

    //Control Variables
    [Range(0,10)]
    public int playerMana;
    [Range(1, 10)]
    public int playerMaxMana;
    public int playerHealth;
    float roundTimer = 0f;
    [SerializeField] float timer = 90f;
    int fatigueDamage = 1;
    public List<Card> playerDeckCards;

    //Object Variables
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerManaText;
    public Image[] playerManaCrystals;
    public GameObject playerHand;
    public TextMeshProUGUI timerText;

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

    private void Start()
    {
        playerHand = GameObject.FindGameObjectWithTag("PlayerHand");

        //Assign Deck Data
        foreach (Card card in playerDeck.cards)
        {
            playerDeckCards.Add(card);
        }

        BeginGame();

        //Timer Setup
        roundTimer = timer;
    }

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
        playerHealthText.text = playerHealth.ToString();

        //Round Timer
        roundTimer -= 1 * Time.deltaTime;
        timerText.text = TimeSpan.FromSeconds(roundTimer).ToString("mm\\:ss");
        if (roundTimer <= 0f)
        {
            BeginNewRound();
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        playerHealth -= damage;
        //Implement Game Over
    }

    public void BeginGame()
    {
        playerMaxMana = 1;
        playerMana = playerMaxMana;
        playerHealth = 30;

        for (int i = 0; i < 4; i++)
        {
            DrawCard();
        }
    }

    public void BeginNewRound()
    {
        //Increase max mana if not at total 10, and refresh it
        if (playerMaxMana < 10)
        {
            playerMaxMana++;
            playerMana = playerMaxMana;
        }
        else
        {
            playerMana = playerMaxMana;
        }

        //Awake asleep minions
        MinionController[] allMinions = FindObjectsOfType<MinionController>();
        Debug.Log($"Found {allMinions.Length} minions on the board");
        foreach(MinionController minion in allMinions)
        {
            if (minion.isAsleep)
            {
                minion.isAsleep = false;
                minion.UpdateMinionInfo();
            }
        }

        //Draw new card
        DrawCard();

        //Reset Timer
        roundTimer = timer;
    }

    public void DrawCard()
    {
        if (playerDeckCards.Count < 1)
        {
            PlayerTakeDamage(fatigueDamage);
            fatigueDamage++;
            Debug.Log("Player out of cards, taking fatigue damage");
        }
        else if(playerHand.GetComponentsInChildren<CardController>().Length == 10)
        {
            System.Random r = new System.Random();
            int selectedCard = r.Next(0, playerDeckCards.Count);
            playerDeckCards.Remove(playerDeckCards[selectedCard]);
            Debug.Log("Player Hand is full, burning cards");
        }
        else
        {
            System.Random r = new System.Random();
            GameObject newCard = Instantiate(cardTemplate, playerHand.transform);
            int selectedCard = r.Next(0, playerDeckCards.Count);
            newCard.GetComponent<CardController>().cardInfo = playerDeckCards[selectedCard];
            playerDeckCards.Remove(playerDeckCards[selectedCard]);
        }
    }
}
