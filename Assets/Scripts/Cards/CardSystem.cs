using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    public static CardSystem Instance;

    public List<Card> availableCards; // List of all possible cards in the game
    public Transform[] spawnPoints; // Points where units will spawn

    public GameObject cardUIPrefab; // Prefab for the card UI
    public Transform cardUIParent; // Parent object to hold the cards in the UI

    private void Awake()
    {
        // Set up singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize with 8 random cards
        InitializeDeck();
    }

    // Initialize 8 cards to be displayed in the UI
    public void InitializeDeck()
    {
        for (int i = 0; i < 2; i++)
        {
            SpawnCardInUI(availableCards[Random.Range(0, availableCards.Count)]);
        }
    }

    // Spawn a card in the UI
    public void SpawnCardInUI(Card Card)
    {
        GameObject cardUI = Instantiate(cardUIPrefab, cardUIParent);
        CardDisplay cardDisplay = cardUI.GetComponent<CardDisplay>();
        cardDisplay.SetupCardDisplay(Card);
    }

    // Play the card and spawn the associated unit
    public void PlayCard(Card Card)
    {
        // Spawn the unit at a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //Instantiate(Card.unitPrefab, spawnPoint.position, Quaternion.identity);

        Debug.Log($"Spawned {Card.characterName} at {spawnPoint.position}");
    }
}
