using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameLogic 
{
   
    public static CombatAgent CalculateAgentStatsFromBaseStats(CombatAgent _agent)
    {
        CombatAgent _newAgent = CombatAgent.Instantiate(_agent);

        // HEALTH
        _newAgent.health = _newAgent.baseStats.perks.vitality == GameData.PerkData.Juggernaut.Vitality.status.notActive ?
            _newAgent.baseStats.Endurance * GameData.ENDURANCE_HEALTH_MULTIPLIER :
            _newAgent.baseStats.Endurance *
            (GameData.ENDURANCE_HEALTH_MULTIPLIER + GameData.PerkData.Juggernaut.Vitality.LevelOneData.healthPerEnduranceIncrease);
        _newAgent.health += GameData.BASE_HEALTH;
        
        _newAgent.currentHealth = _newAgent.health;

        


        // ARMOR
        if(_newAgent.armor != null)
        {
            _newAgent.armorPoints = _newAgent.armor.armorPoints;
            _newAgent.currentArmorPoints = _newAgent.armorPoints;
        }
        else
        {
            _newAgent.armorPoints = 0;
            _newAgent.currentArmorPoints = 0;
        }




        // MAX SIDE DEFENSE                                        Unbreakable
        switch(_newAgent.baseStats.perks.unbreakable)
        {
            case GameData.PerkData.Juggernaut.Unbreakable.status.notActive:
                _newAgent.maxSideDefensePoints = GameData.BASE_MAX_SIDE_DEFENSE_POINTS;
                break;

            case GameData.PerkData.Juggernaut.Unbreakable.status.levelOne:
                _newAgent.maxSideDefensePoints = GameData.PerkData.Juggernaut.Unbreakable.LevelOneData.maxSideDefense;
                break;

            case GameData.PerkData.Juggernaut.Unbreakable.status.levelTwo:
                _newAgent.maxSideDefensePoints = GameData.PerkData.Juggernaut.Unbreakable.LevelTwoData.maxSideDefense;
                break;

            case GameData.PerkData.Juggernaut.Unbreakable.status.LevelThree:
                _newAgent.maxSideDefensePoints = GameData.PerkData.Juggernaut.Unbreakable.LevelThreeData.maxSideDefense;
                break;
        }

       



        
        // PREDICTION SIDE DEFENSE POINTS + PREDICTION SIDE DEFENSE PROTECTION                      Skillfull Defender     
        switch(_newAgent.baseStats.perks.skillfulDefender)
        {
            case GameData.PerkData.Juggernaut.SkillfulDefender.status.notActive:
                _newAgent.predictionSideDefensePoints = GameData.BASE_PREDICTION_SIDE_DEFENSE_POINTS;
                _newAgent.predictionSideDefenseGain = GameData.BASE_PREDICTION_SIDE_DEFENSE_PROTECTION;
                break;

            case GameData.PerkData.Juggernaut.SkillfulDefender.status.levelOne:
                _newAgent.predictionSideDefensePoints = GameData.PerkData.Juggernaut.SkillfulDefender.LevelOneData.predictionPoints;
                _newAgent.predictionSideDefenseGain = GameData.PerkData.Juggernaut.SkillfulDefender.LevelOneData.predictionProtection;
                break;

            case GameData.PerkData.Juggernaut.SkillfulDefender.status.levelTwo:
                _newAgent.predictionSideDefensePoints = GameData.PerkData.Juggernaut.SkillfulDefender.LevelTwoData.predictionPoints;
                _newAgent.predictionSideDefenseGain = GameData.PerkData.Juggernaut.SkillfulDefender.LevelTwoData.predictionProtection;
                break;
        }
        _newAgent.currentPredictionSideDefensePoints = _newAgent.predictionSideDefensePoints;


        // ADAPTION POINTS GAINED                                   Fast Learner
        switch(_newAgent.baseStats.perks.fastLearner)
        {
            case GameData.PerkData.Juggernaut.FastLearner.status.notActive:
                _newAgent.adaptionPointsGainedPerHit = GameData.BASE_ADAPTION_GAINED_PER_HIT;
                break;

            case GameData.PerkData.Juggernaut.FastLearner.status.levelOne:
                _newAgent.adaptionPointsGainedPerHit = GameData.PerkData.Juggernaut.FastLearner.LevelOneData.adaptionSideDefensePointsGained;
                break;

                /*
            case GameData.PerkData.Juggernaut.FastLearner.status.levelTwo:
                _newAgent.adaptionPointsGainedPerHit = GameData.PerkData.Juggernaut.FastLearner.LevelTwoData.adaptionSideDefensePointsGained;
                break;
                */
        }


        // ADAPTION POINTS DECAY TIME                           Lasting Memory
        switch(_newAgent.baseStats.perks.lastingMemory)
        {
            case GameData.PerkData.Juggernaut.LastingMemory.status.notActive:
                _newAgent.adaptionPointsDecayInTurns = GameData.BASE_ADAPTION_DECAY_TIME_IN_TURNS;
                break;
            case GameData.PerkData.Juggernaut.LastingMemory.status.levelOne:
                _newAgent.adaptionPointsDecayInTurns = GameData.PerkData.Juggernaut.LastingMemory.LevelOneData.decayTimeInTurns;
                break;
            case GameData.PerkData.Juggernaut.LastingMemory.status.levelTwo:
                _newAgent.adaptionPointsDecayInTurns = GameData.PerkData.Juggernaut.LastingMemory.LevelTwoData.decayTimeInTurns;
                break;
        }

        // SKIP CHANCE                                          Skip
        switch(_newAgent.baseStats.perks.skip)
        {
            case GameData.PerkData.Scoundrel.Skip.status.notActive:
                _newAgent.makeEnemySkipChance = 0;
                break;

            case GameData.PerkData.Scoundrel.Skip.status.levelOne:
                _newAgent.makeEnemySkipChance = GameData.PerkData.Scoundrel.Skip.LevelOneData.chanceToMakeEnemySkip;
                break;

            case GameData.PerkData.Scoundrel.Skip.status.levelTwo:
                _newAgent.makeEnemySkipChance = GameData.PerkData.Scoundrel.Skip.LevelTwoData.chanceToMakeEnemySkip;
                break;

            case GameData.PerkData.Scoundrel.Skip.status.LevelThree:
                _newAgent.makeEnemySkipChance = GameData.PerkData.Scoundrel.Skip.LevelThreeData.chanceToMakeEnemySkip;
                break;
        }



        // IGNORE SIDE DEFENSE CHANCE                           Deceptive Technique
        switch(_newAgent.baseStats.perks.deceptiveTechnique)
        {
            case GameData.PerkData.Scoundrel.DeceptiveTechnique.status.notActive:
                _newAgent.ignoreEnemySideDefenseChance = 0;
                break;

            case GameData.PerkData.Scoundrel.DeceptiveTechnique.status.levelOne:
                _newAgent.ignoreEnemySideDefenseChance = GameData.PerkData.Scoundrel.DeceptiveTechnique.LevelOneData.chanceToIgnoreSideDefensePoints;
                break;

            case GameData.PerkData.Scoundrel.DeceptiveTechnique.status.levelTwo:
                _newAgent.ignoreEnemySideDefenseChance = GameData.PerkData.Scoundrel.DeceptiveTechnique.LevelTwoData.chanceToIgnoreSideDefensePoints;
                break;

            case GameData.PerkData.Scoundrel.DeceptiveTechnique.status.LevelThree:
                _newAgent.ignoreEnemySideDefenseChance = GameData.PerkData.Scoundrel.DeceptiveTechnique.LevelThreeData.chanceToIgnoreSideDefensePoints;
                break;
        }




        // CRIT CHANCE                                                  Critical Hit
        switch(_newAgent.baseStats.perks.criticalHit)
        {
            case GameData.PerkData.Scoundrel.CriticalHit.status.notActive:
                _newAgent.critChance = 0; 
                break;

            case GameData.PerkData.Scoundrel.CriticalHit.status.levelOne:
                _newAgent.critChance = GameData.PerkData.Scoundrel.CriticalHit.LevelOneData.chanceToCrit;
                break;

            case GameData.PerkData.Scoundrel.CriticalHit.status.levelTwo:
                _newAgent.critChance = GameData.PerkData.Scoundrel.CriticalHit.LevelTwoData.chanceToCrit;
                break;

            case GameData.PerkData.Scoundrel.CriticalHit.status.LevelThree:
                _newAgent.critChance = GameData.PerkData.Scoundrel.CriticalHit.LevelThreeData.chanceToCrit;
                break;
        }


        // REFLECT CHANCE                                               Foul Stance
        switch(_newAgent.baseStats.perks.foulStance)
        {
            case GameData.PerkData.Scoundrel.FoulStance.status.notActive:
                _newAgent.reflectChance = 0;
                _newAgent.reflectFullDamagePotentialPercent = 0;
                break;
            case GameData.PerkData.Scoundrel.FoulStance.status.levelOne:
                _newAgent.reflectChance = GameData.PerkData.Scoundrel.FoulStance.LevelOneData.chanceToReflect;
                _newAgent.reflectFullDamagePotentialPercent = GameData.PerkData.Scoundrel.FoulStance.LevelOneData.damagePotentialPercentReflected;
                break;
            case GameData.PerkData.Scoundrel.FoulStance.status.levelTwo:
                _newAgent.reflectChance = GameData.PerkData.Scoundrel.FoulStance.LevelTwoData.chanceToReflect;
                _newAgent.reflectFullDamagePotentialPercent = GameData.PerkData.Scoundrel.FoulStance.LevelTwoData.damagePotentialPercentReflected;
                break;

        }


        // BONUS BLOCK ROLL                                                     Block Mastery
        switch(_newAgent.baseStats.perks.blockMastery)
        {
            case GameData.PerkData.Juggernaut.BlockMastery.status.notActive:
                _newAgent.bonusBlockRoll = 0;  
                break;

            case GameData.PerkData.Juggernaut.BlockMastery.status.levelOne:
                _newAgent.bonusBlockRoll = GameData.PerkData.Juggernaut.BlockMastery.LevelOneData.blockRollIncrease;
                break;

            case GameData.PerkData.Juggernaut.BlockMastery.status.levelTwo:
                _newAgent.bonusBlockRoll = GameData.PerkData.Juggernaut.BlockMastery.LevelTwoData.blockRollIncrease;
                break;

            case GameData.PerkData.Juggernaut.BlockMastery.status.levelThree:
                _newAgent.bonusBlockRoll = GameData.PerkData.Juggernaut.BlockMastery.LevelThreeData.blockRollIncrease;
                break;

        }


        // BONUS ATTACK ROLL                                            Attack Mastery
        switch(_newAgent.baseStats.perks.attackMastery)
        {
            case GameData.PerkData.Warrior.AttackMastery.status.notActive:
                _newAgent.bonusAttackRoll = 0; 
                break;
            case GameData.PerkData.Warrior.AttackMastery.status.levelOne:
                _newAgent.bonusAttackRoll = GameData.PerkData.Warrior.AttackMastery.LevelOneData.attackRollIncrease;
                break;
            case GameData.PerkData.Warrior.AttackMastery.status.levelTwo:
                _newAgent.bonusAttackRoll = GameData.PerkData.Warrior.AttackMastery.LevelTwoData.attackRollIncrease;
                break;
            case GameData.PerkData.Warrior.AttackMastery.status.LevelThree:
                _newAgent.bonusAttackRoll = GameData.PerkData.Warrior.AttackMastery.LevelThreeData.attackRollIncrease;
                break;
        }


        // BONUS INCREAED ARMOR HIT CHANCE                                  Armor Mastery
        switch(_newAgent.baseStats.perks.armorMastery)
        {
            case GameData.PerkData.Juggernaut.ArmorMastery.status.notActive:
                _newAgent.increasedArmorHitChance = 0;
                break;
            case GameData.PerkData.Juggernaut.ArmorMastery.status.levelOne:
                _newAgent.increasedArmorHitChance = GameData.PerkData.Juggernaut.ArmorMastery.LevelOneData.armorHitChanceIncrease;
                break;
            case GameData.PerkData.Juggernaut.ArmorMastery.status.levelTwo:
                _newAgent.increasedArmorHitChance = GameData.PerkData.Juggernaut.ArmorMastery.LevelTwoData.armorHitChanceIncrease;
                break;
        }


        // BONUS STRENGTH MULTIPLIER                                            Savage
        switch(_newAgent.baseStats.perks.savage)
        {
            case GameData.PerkData.Warrior.Savage.status.notActive:
                _newAgent.increasedStrengthMultiplier = 0;
                break;
            case GameData.PerkData.Warrior.Savage.status.levelOne:
                _newAgent.increasedStrengthMultiplier = GameData.PerkData.Warrior.Savage.LevelOneData.strengthMultiplierIncreased;
                break;
            case GameData.PerkData.Warrior.Savage.status.levelTwo:
                _newAgent.increasedStrengthMultiplier = GameData.PerkData.Warrior.Savage.LevelTwoData.strengthMultiplierIncreased;
                break;
            case GameData.PerkData.Warrior.Savage.status.LevelThree:
                _newAgent.increasedStrengthMultiplier = GameData.PerkData.Warrior.Savage.LevelThreeData.strengthMultiplierIncreased;
                break;
        }


        // ATTACK ROULL DOUBLED CHANCE                                      Breakthrough
        switch(_newAgent.baseStats.perks.breakthrough)
        {
            case GameData.PerkData.Warrior.Breakthrough.status.notActive:
                _newAgent.attackRollDoubledChance = 0;
                break;
            case GameData.PerkData.Warrior.Breakthrough.status.levelOne:
                _newAgent.attackRollDoubledChance = GameData.PerkData.Warrior.Breakthrough.LevelOneData.chanceToDoubleAttackRolls;
                break;
            case GameData.PerkData.Warrior.Breakthrough.status.levelTwo:
                _newAgent.attackRollDoubledChance = GameData.PerkData.Warrior.Breakthrough.LevelTwoData.chanceToDoubleAttackRolls;
                break;
            case GameData.PerkData.Warrior.Breakthrough.status.LevelThree:
                _newAgent.attackRollDoubledChance = GameData.PerkData.Warrior.Breakthrough.LevelThreeData.chanceToDoubleAttackRolls;
                break;
        }

        //  BLEED                                                           Lacerate 
        switch (_newAgent.baseStats.perks.lacerate)
        {
            case GameData.PerkData.Warrior.Lacerate.status.notActive:
                _newAgent.lacerateMultiplier = 0;
                break;

            case GameData.PerkData.Warrior.Lacerate.status.levelOne:
                _newAgent.lacerateMultiplier = GameData.PerkData.Warrior.Lacerate.LevelOneData.cleanAttacksCountBleedMultiplier;
                break;

            case GameData.PerkData.Warrior.Lacerate.status.levelTwo:
                _newAgent.lacerateMultiplier = GameData.PerkData.Warrior.Lacerate.LevelTwoData.cleanAttacksCountBleedMultiplier;
                break;
        }
        _newAgent.cleanAttacksLandedThisFight = 0; 




        _newAgent.agentSkipsThisRound = false;

        // Revenge
        _newAgent.bonusDamage.cut = 0;      
        _newAgent.bonusDamage.pierce = 0;
        _newAgent.bonusDamage.blunt = 0;
        _newAgent.revengeActive = false;

        return _newAgent;
    }


    public static Vector2Int CalculateRawDamage(CombatAgent _attacker, CombatAgent _receiver)
    {
        Vector2Int _damage = new Vector2Int(0, 0);

        // x - health, y - armor

        if (_attacker.mainHand == null) { Debug.LogError("No right hand weapon on agent"); return _damage; }

        int _allDamage = _attacker.mainHand.damage.cut + _attacker.mainHand.damage.pierce + _attacker.mainHand.damage.blunt;


        int _cutPercent = Mathf.FloorToInt((float)_attacker.mainHand.damage.cut / _allDamage);
        int _piercePercent = Mathf.FloorToInt((float)_attacker.mainHand.damage.pierce / _allDamage);
        int _bluntPercent = Mathf.FloorToInt((float)_attacker.mainHand.damage.blunt / _allDamage);

        int _cutDamage = _attacker.mainHand.damage.cut +
            Mathf.FloorToInt((((float)(_attacker.mainHand.strengthMultiplier + _attacker.increasedStrengthMultiplier) / 100) * _attacker.baseStats.Strength) * _cutPercent);

        int _pierceDamage = _attacker.mainHand.damage.pierce +
           Mathf.FloorToInt((((float)(_attacker.mainHand.strengthMultiplier + _attacker.increasedStrengthMultiplier) / 100) * _attacker.baseStats.Strength) * _piercePercent);

        int _bluntDamage = _attacker.mainHand.damage.blunt +
           Mathf.FloorToInt((((float)(_attacker.mainHand.strengthMultiplier + _attacker.increasedStrengthMultiplier) / 100) * _attacker.baseStats.Strength) * _bluntPercent);


        for (int i = 0; i < _cutDamage; i++)
        {
            int _randomRoll = Random.Range(1, 101);
            if (_randomRoll > GameData.BASE_ARMOR_HIT_CHANCE + _receiver.increasedArmorHitChance)
            {
                _damage.x += 2;
            }
            else
            {
                if (_receiver.currentArmorPoints < _damage.y + 1)
                {
                    _damage.x += 2;
                }
                else
                {
                    _damage.y += 1;
                }
            }
        }

        for (int i = 0; i < _pierceDamage; i++)
        {
            int _randomRoll = Random.Range(1, 101);
            if (_randomRoll > GameData.BASE_ARMOR_HIT_CHANCE - GameData.PIERCE_DAMAGE_INCREASED_HEALTH_HIT_CHANCE + _receiver.increasedArmorHitChance)
            {
                _damage.x += 1;
            }
            else
            {
                if (_receiver.currentArmorPoints < _damage.y + 1)
                {
                    _damage.x += 1;
                }
                else
                {
                    _damage.y += 1;
                }
            }
        }

        for (int i = 0; i < _bluntDamage; i++)
        {
            int _randomRoll = Random.Range(1, 101);
            if (_randomRoll > GameData.BASE_ARMOR_HIT_CHANCE + _receiver.increasedArmorHitChance)
            {
                _damage.x += 1;
            }
            else
            {
                if (_receiver.currentArmorPoints < _damage.y + 1)
                {
                    _damage.x += 1;
                }
                else
                {
                    _damage.y += 2;
                }
            }
        }


        return _damage;
    }



    public static Vector2Int CalculateDamage(CombatAgent _attacker, CombatAgent _receiver)
    {
        Vector2Int _damage = new Vector2Int(0, 0);

        // x - health, y - armor

        if(_attacker.mainHand == null) { Debug.LogError("No right hand weapon on agent"); return _damage; }
        
        int _allDamage = _attacker.mainHand.damage.cut + _attacker.mainHand.damage.pierce + _attacker.mainHand.damage.blunt;
       

        int _cutPercent = Mathf.FloorToInt((float)_attacker.mainHand.damage.cut / _allDamage);
        int _piercePercent = Mathf.FloorToInt((float)_attacker.mainHand.damage.pierce / _allDamage);
        int _bluntPercent = Mathf.FloorToInt((float)_attacker.mainHand.damage.blunt / _allDamage);

        int _cutDamage = _attacker.mainHand.damage.cut +
            Mathf.FloorToInt((((float)(_attacker.mainHand.strengthMultiplier + _attacker.increasedStrengthMultiplier) / 100) * _attacker.baseStats.Strength) * _cutPercent);

        int _pierceDamage = _attacker.mainHand.damage.pierce +
           Mathf.FloorToInt((((float)(_attacker.mainHand.strengthMultiplier + _attacker.increasedStrengthMultiplier) / 100) * _attacker.baseStats.Strength) * _piercePercent);

        int _bluntDamage = _attacker.mainHand.damage.blunt +
           Mathf.FloorToInt((((float)(_attacker.mainHand.strengthMultiplier + _attacker.increasedStrengthMultiplier) / 100) * _attacker.baseStats.Strength) * _bluntPercent);


        int _random = Random.Range(1, 101);
        if(_random <= _attacker.critChance)
        {
            _cutDamage *= 2;
            _pierceDamage *= 2;
            _bluntDamage *= 2;

            CombatManager.OnEventTextCreation?.Invoke(GameData.PerkData.Scoundrel.CriticalHit.perkName);
        }


        if(!_attacker.bonusDamage.IsZero())
        {
            CombatManager.OnEventTextCreation?.Invoke(GameData.PerkData.Warrior.Revenge.perkName);
        }
        _cutDamage += _attacker.bonusDamage.cut;
        _pierceDamage += _attacker.bonusDamage.pierce;
        _bluntDamage += _attacker.bonusDamage.blunt;


        for (int i = 0; i < _cutDamage;i++)
        {
            int _randomRoll = Random.Range(1, 101);
            if(_randomRoll > GameData.BASE_ARMOR_HIT_CHANCE + _receiver.increasedArmorHitChance)
            {
                _damage.x += 2;
            }
            else
            {
                if(_receiver.currentArmorPoints < _damage.y + 1)
                {
                    _damage.x += 2;
                }
                else
                {
                    _damage.y += 1;
                }
            }
        }

        for (int i = 0; i < _pierceDamage; i++)
        {
            int _randomRoll = Random.Range(1, 101);
            if (_randomRoll > GameData.BASE_ARMOR_HIT_CHANCE - GameData.PIERCE_DAMAGE_INCREASED_HEALTH_HIT_CHANCE + _receiver.increasedArmorHitChance)
            {
                _damage.x += 1;
            }
            else
            {
                if (_receiver.currentArmorPoints < _damage.y + 1)
                {
                    _damage.x += 1;
                }
                else
                {
                    _damage.y += 1;
                }
            }
        }

        for (int i = 0; i < _bluntDamage; i++)
        {
            int _randomRoll = Random.Range(1, 101);
            if (_randomRoll > GameData.BASE_ARMOR_HIT_CHANCE + _receiver.increasedArmorHitChance)
            {
                _damage.x += 1;
            }
            else
            {
                if (_receiver.currentArmorPoints < _damage.y + 1)
                {
                    _damage.x += 1;
                }
                else
                {
                    _damage.y += 2;
                }
            }
        }


        return _damage;
    }

    public static bool DodgeCheck(int _attackRoll, CombatAgent _receiver)
    {
        bool _dodgeSuccesful = false;
        int _maxDodgeRoll = _receiver.baseStats.Reflex;
        int _dodgeRoll = 0;

        if(_receiver.baseStats.perks.slippery == GameData.PerkData.Scoundrel.Slippery.status.levelOne)
        {
            _maxDodgeRoll += _receiver.baseStats.Awareness;
        }

        _dodgeRoll = Random.Range(1, _maxDodgeRoll);

        if(_dodgeRoll > _attackRoll)
        {
            _dodgeSuccesful = true;
        }
        else
        {
            _dodgeSuccesful = false;
        }

        return _dodgeSuccesful;
    }

    public static bool BlockCheck(int _attackRoll, CombatAgent _attacker, CombatAgent _receiver, out bool _ignoreSidedefense)
    {
        bool _blockSuccesful = false;
        int _maxBlockRoll = _receiver.baseStats.Reflex;
        int _blockRoll = 0;
        int _sideDefense = 0;
        _ignoreSidedefense = false;


        int _random = Random.Range(1, 101);
        if (_random <= _attacker.ignoreEnemySideDefenseChance)
        {
            // ignore side defense
            _ignoreSidedefense = true;
            CombatManager.OnEventTextCreation?.Invoke(GameData.PerkData.Scoundrel.DeceptiveTechnique.perkName);
        }
        else
        {
            switch (_attacker.attackDirection)
            {
                case CombatDirection.BottomLeft:
                    _sideDefense = Mathf.Clamp(_receiver.BL.predictionPoints + _receiver.BL.adaptionPoints, 0, _receiver.maxSideDefensePoints);
                    break;
                case CombatDirection.BottomRight:
                    _sideDefense = Mathf.Clamp(_receiver.BR.predictionPoints + _receiver.BR.adaptionPoints, 0, _receiver.maxSideDefensePoints);
                    break;
                case CombatDirection.TopLeft:
                    _sideDefense = Mathf.Clamp(_receiver.TL.predictionPoints + _receiver.TL.adaptionPoints, 0, _receiver.maxSideDefensePoints);
                    break;
                case CombatDirection.TopRight:
                    _sideDefense = Mathf.Clamp(_receiver.TR.predictionPoints + _receiver.TR.adaptionPoints, 0, _receiver.maxSideDefensePoints);
                    break;
            }
        }

        _maxBlockRoll += _sideDefense;
        
        if(_receiver.mainHand != null)
        {
            _maxBlockRoll += _receiver.mainHand.blockChanceRollIncrease;
        }

        if (_receiver.offHand != null)
        {
            _maxBlockRoll += _receiver.offHand.blockChanceRollIncrease;
        }

        _blockRoll = Random.Range(1, _maxBlockRoll);
        _blockRoll += _receiver.bonusBlockRoll;

        if (_blockRoll >= _attackRoll)
        {
            _blockSuccesful = true;
        }
        else
        {
            _blockSuccesful = false;
        }


       // Debug.Log("Base Max Roll: " + (_maxBlockRoll - _sideDefense) + " Max Roll: " + _maxBlockRoll + " Side Defense: " + _sideDefense + " Actual Roll: "+ _blockRoll + " Attack Roll: " + _attackRoll);
        return _blockSuccesful;
    }

    public static int BlockProtectionCalculation(int _attackRoll, CombatAgent _attacker, CombatAgent _receiver, bool _ignoreSideDefense)
    {
        int _blockPercent = 100;
        int _maxBlockProtectionRoll = Mathf.FloorToInt(_receiver.baseStats.Strength / 2) + _receiver.baseStats.Endurance;
        int _sideDefense = 0;

        
        if (_ignoreSideDefense)
        {
            // ignore side defense
        }
        else
        {
            switch (_attacker.attackDirection)
            {
                case CombatDirection.BottomLeft:
                    _sideDefense = Mathf.Clamp(_receiver.BL.predictionPoints + _receiver.BL.adaptionPoints, 0, _receiver.maxSideDefensePoints);
                    break;
                case CombatDirection.BottomRight:
                    _sideDefense = Mathf.Clamp(_receiver.BR.predictionPoints + _receiver.BR.adaptionPoints, 0, _receiver.maxSideDefensePoints);
                    break;
                case CombatDirection.TopLeft:
                    _sideDefense = Mathf.Clamp(_receiver.TL.predictionPoints + _receiver.TL.adaptionPoints, 0, _receiver.maxSideDefensePoints);
                    break;
                case CombatDirection.TopRight:
                    _sideDefense = Mathf.Clamp(_receiver.TR.predictionPoints + _receiver.TR.adaptionPoints, 0, _receiver.maxSideDefensePoints);
                    break;
            }
        }

        _maxBlockProtectionRoll += _sideDefense;

        if (_receiver.mainHand != null)
        {
            _maxBlockProtectionRoll += _receiver.mainHand.blockProtectionRollIncrease;
        }

        if (_receiver.offHand != null)
        {
            _maxBlockProtectionRoll += _receiver.offHand.blockProtectionRollIncrease;
        }

        int _blockProtection = Random.Range(1, _maxBlockProtectionRoll + 1);



        if(_attackRoll > _blockProtection) // if attack is bigger deal damage, else no damage is dealt
        {
            _blockPercent = Mathf.CeilToInt(((float)_blockProtection / _attackRoll) * 100);
        }

        return _blockPercent;
    }

    public static int CalculateAttackRoll(CombatAgent _agent)
    {
        int _maxAttackRoll = 0;
        int _attack = 0;

        if(_agent.mainHand != null)
        {
            _maxAttackRoll += _agent.mainHand.attackRollIncrease;
        }

        if(_agent.offHand != null)
        {
            _maxAttackRoll += _agent.offHand.attackRollIncrease;
        }

        _maxAttackRoll += _agent.bonusAttackRoll;

        _maxAttackRoll += Mathf.FloorToInt((float)_agent.baseStats.Strength / 2) + _agent.baseStats.Speed;

        _attack = Random.Range(1, _maxAttackRoll + 1);

        int _random = Random.Range(1, 101);
        if(_random <= _agent.attackRollDoubledChance)
        {
            _attack *= 2;
            CombatManager.OnEventTextCreation?.Invoke(GameData.PerkData.Warrior.Breakthrough.perkName);
        }

        _attack += _agent.bonusAttackRoll;

        return _attack;
    }

}
