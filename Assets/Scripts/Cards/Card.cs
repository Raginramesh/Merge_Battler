using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/NewCard")]
public class Card : ScriptableObject
{
    [Header("Character Info")]
    public string characterName;
    public Sprite characterImage;
    public string description;

    [Header("Character Stats")]
    public int health;
    public float hitRange;
    public float hitSpeed;
    public float deploySpeed;
    public EnemyTargetType enemyTarget;
    public List<Ability> abilities;

    [Header("Upgrades")]
    public List<Upgrade> upgrades;

    [Header("Other Stats")]
    public int manaCost;
    public int level;

    // Add more stats as needed
}

public enum EnemyTargetType
{
    Ground,
    Air,
    Both
}

[System.Serializable]
public class Ability
{
    public string abilityName;
    public string description;
    public float cooldown;
    public float duration;
    // Add other ability properties here as needed
}

[System.Serializable]
public class Upgrade
{
    public int level;
    public int healthUpgrade;
    public float hitSpeedUpgrade;
    public float deploySpeedUpgrade;
    public int costUpgrade;
    // Add other upgrade properties here as needed
} 

