using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "PresetObjects/Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public bool isOffHand;
    [Space]
    public Damage damage;
    [Space]
    public bool isTwoHanded;
    public int attackRollIncrease;
    public int strengthMultiplier;
    public int blockChanceRollIncrease;
    public int blockProtectionRollIncrease;
}

