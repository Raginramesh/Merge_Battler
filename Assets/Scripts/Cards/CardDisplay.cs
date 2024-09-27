using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    [Header("UI Elements")]
    public Image characterImage;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text healthText;
    public TMP_Text hitRangeText;
    public TMP_Text hitSpeedText;
    public TMP_Text deploySpeedText;
    public TMP_Text enemyTargetText;
    public Transform abilitiesParent;
    public Transform upgradesParent;

    [Header("Prefab References")]
    public GameObject abilityPrefab;
    public GameObject upgradePrefab;

    private Card characterCard;

    // Method to set up the display based on the character card
    public void SetupCardDisplay(Card card)
    {
        characterCard = card;

        // Set basic info
        characterImage.sprite = characterCard.characterImage;
        nameText.text = characterCard.characterName;
        descriptionText.text = characterCard.description;

        // Set stats info
        healthText.text = $"Health: {characterCard.health}";
        hitRangeText.text = $"Range: {characterCard.hitRange}";
        hitSpeedText.text = $"Hit Speed: {characterCard.hitSpeed}";
        deploySpeedText.text = $"Deploy Speed: {characterCard.deploySpeed}";
        enemyTargetText.text = $"Target: {characterCard.enemyTarget}";

        // Display abilities
        foreach (Transform child in abilitiesParent)
        {
            Destroy(child.gameObject);
        }
        foreach (var ability in characterCard.abilities)
        {
            GameObject abilityGO = Instantiate(abilityPrefab, abilitiesParent);
            //abilityGO.GetComponent<AbilityDisplay>().SetupAbility(ability);
        }

        // Display upgrades
        foreach (Transform child in upgradesParent)
        {
            Destroy(child.gameObject);
        }
        foreach (var upgrade in characterCard.upgrades)
        {
            GameObject upgradeGO = Instantiate(upgradePrefab, upgradesParent);
            //upgradeGO.GetComponent<UpgradeDisplay>().SetupUpgrade(upgrade);
        }
    }
}

