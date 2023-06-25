using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct PerkStatus
{
    [Header("Juggernaut Perks")]
    [Space]
    public GameData.PerkData.Juggernaut.Unbreakable.status unbreakable;
    public GameData.PerkData.Juggernaut.Vitality.status vitality;
    public GameData.PerkData.Juggernaut.FastLearner.status fastLearner;
    public GameData.PerkData.Juggernaut.BlockMastery.status blockMastery;
    public GameData.PerkData.Juggernaut.LastingMemory.status lastingMemory;
    public GameData.PerkData.Juggernaut.ArmorMastery.status armorMastery;
    public GameData.PerkData.Juggernaut.SkillfulDefender.status skillfulDefender;
    [Space]
    [Space]
    [Header("Warrior Perks")]
    [Space]
    public GameData.PerkData.Warrior.AttackMastery.status attackMastery;
    public GameData.PerkData.Warrior.Breakthrough.status breakthrough;
    public GameData.PerkData.Warrior.Lacerate.status lacerate;
    public GameData.PerkData.Warrior.Revenge.status revenge;
    public GameData.PerkData.Warrior.Savage.status savage;
    [Space]
    [Space]
    [Header("Scoundrel Perks")]
    [Space]
    public GameData.PerkData.Scoundrel.CriticalHit.status criticalHit;
    public GameData.PerkData.Scoundrel.DeceptiveTechnique.status deceptiveTechnique;
    public GameData.PerkData.Scoundrel.FoulStance.status foulStance;
    public GameData.PerkData.Scoundrel.KeenEye.status keenEye;
    public GameData.PerkData.Scoundrel.Skip.status skip;
    public GameData.PerkData.Scoundrel.Slippery.status slippery;
}


public enum CombatDirection {TopLeft, TopRight,BottomLeft, BottomRight }

[System.Serializable]
public struct AgentBaseStats
{
    public int Strength;
    public int Endurance;
    public int Reflex;
    public int Speed;
    public int Awareness;
    [Space]
    public PerkStatus perks;
}

[System.Serializable]
public struct AttackDirectionStats
{
     public int predictionPoints;
     public int adaptionPoints;
     public int adaptionDecayTurns;
     public bool isPredicted;
}

[System.Serializable]
public struct Damage
{
     public int cut;
     public int pierce;
     public int blunt;

    public static Damage operator* (Damage _d, float _multiplier)
    {
        Damage _newDamage = _d;
        _newDamage.cut = Mathf.FloorToInt((float)_d.cut * _multiplier);
        _newDamage.pierce = Mathf.FloorToInt((float)_d.pierce * _multiplier);
        _newDamage.blunt = Mathf.FloorToInt((float)_d.blunt * _multiplier);
        return _d;
    }

    public bool IsZero()
    {
        if(cut == 0 && pierce == 0 && blunt == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetToZero()
    {
        cut = 0;
        pierce = 0;
        blunt = 0;
    }
}



[CreateAssetMenu(fileName = "Agent", menuName = "PresetObjects/Agent", order = 1)]
public class CombatAgent : ScriptableObject
{
    public static Action<CombatAgent> OnAgentDeath;

    public string agentName;
    [Space]
    public AgentBaseStats baseStats;
    [Space]
    public int health;
    public int currentHealth;
    [Space]
    public int armorPoints;
    public int currentArmorPoints;
    [Space]
    [Space]
    [Space]
    public AttackDirectionStats TL;
    [Space]
    public AttackDirectionStats TR;
    [Space]
    public AttackDirectionStats BL;
    [Space]
    public AttackDirectionStats BR;
    [Space]
    [Space]
    [Space]

    public int maxSideDefensePoints;
    public int predictionSideDefensePoints;
    public int currentPredictionSideDefensePoints;
    public int predictionSideDefenseGain;
    public int increasedArmorHitChance;
    public int adaptionPointsGainedPerHit;
    public int adaptionPointsDecayInTurns;
    [Space]
    public int makeEnemySkipChance;
    public bool agentSkipsThisRound;
    public int ignoreEnemySideDefenseChance;
    public int critChance;
    public int bonusBlockRoll;
    public int bonusAttackRoll;
    public bool revengeActive;
    public int reflectChance;
    public int reflectFullDamagePotentialPercent;
    [Space]
    public int cleanAttacksLandedThisFight;
    public Damage bonusDamage;
    public int increasedStrengthMultiplier;
    public CombatDirection attackDirection;
    public int attackRollDoubledChance;
    public int lacerateMultiplier;
    [Space]
    [Space]
    [Space]
    public Armor armor;
    public Weapon mainHand;
    public Weapon offHand;


    public void DealDamage(Vector2Int _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage.x, 0, health);
        currentArmorPoints = Mathf.Clamp(currentArmorPoints - _damage.y, 0, armorPoints);

        if(currentHealth <= 0)
        {
            OnAgentDeath?.Invoke(this);
        }
    }


}
