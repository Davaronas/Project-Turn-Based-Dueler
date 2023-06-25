using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData 
{
 

    public static int BASE_HEALTH = 20;

    public static int ENDURANCE_HEALTH_MULTIPLIER = 5;

    public static int BASE_MAX_SIDE_DEFENSE_POINTS = 3;
    public static int BASE_PREDICTION_SIDE_DEFENSE_POINTS = 1;
    public static int BASE_PREDICTION_SIDE_DEFENSE_PROTECTION = 1;
    public static int BASE_ARMOR_HIT_CHANCE = 100;
    public static int PIERCE_DAMAGE_INCREASED_HEALTH_HIT_CHANCE = 30;
    public static int BASE_ADAPTION_DECAY_TIME_IN_TURNS = 1;
    public static int BASE_ADAPTION_GAINED_PER_HIT = 1;

    public static class AwarenessCheckThresholds
    {
        public static int SIDE_DEFENSE_ATTACK_CHANCE_SHOWN = 12;
        public static int ENEMY_ATTACK_DIRECTION_SHOWN = 18;
    }

    [System.Serializable]
    public class PerkData
    {

        public static class Juggernaut
        {
            public static class Unbreakable
            {
               
                public enum status { notActive, levelOne,levelTwo, LevelThree}

                public static string perkName = "Unbreakable";

                public static class LevelOneData
                {
                    public static int perkTier = 1;
                    public static int maxSideDefense = 4;
                    public static string desc = "Max Side Defense points increased to 4";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 2;
                    public static int maxSideDefense = 5;
                    public static string desc = "Max Side Defense points increased to 5";
                }

                public static class LevelThreeData
                {
                    public static int perkTier = 4;
                    public static int maxSideDefense = 7;
                    public static string desc = "Max Side Defense points increased to 7";
                }
            }

            public static class SkillfulDefender
            {

                public enum status { notActive, levelOne, levelTwo }

                public static string perkName = "Skillful Defender";

                public static class LevelOneData
                {
                    public static int perkTier = 2;
                    public static int predictionPoints = 2;
                    public static int predictionProtection = 2;
                    public static string desc = $"You can now predict {predictionPoints} sides, and your Prediction" +
                        $"gives {predictionProtection} Side Defense points";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 3;
                    public static int predictionPoints = 2;
                    public static int predictionProtection = 3;
                    public static string desc = $"You can predict {predictionPoints} sides, and your Prediction" +
                        $"gives {predictionProtection} Side Defense points";
                }


            }

            public static class LastingMemory
            {

                public enum status { notActive, levelOne, levelTwo}

                public static string perkName = "Lasting Memory";

                public static class LevelOneData
                {
                    public static int perkTier = 2;
                    public static int decayTimeInTurns = 2;
                    public static string desc = $"Adaption side defense points take {decayTimeInTurns} turns to decay";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 4;
                    public static int decayTimeInTurns = 3;
                    public static string desc = $"Adaption side defense points take {decayTimeInTurns} turns to decay";
                }

               
            }

            public static class FastLearner
            {

                public enum status { notActive, levelOne } //, levelTwo }

                public static string perkName = "Fast Learner";

                public static class LevelOneData
                {
                    public static int perkTier = 2;
                    public static int adaptionSideDefensePointsGained = 2;
                    public static string desc = $"Adaption side defense points gained increased to {adaptionSideDefensePointsGained}.";
                }

                /*
                public static class LevelTwoData
                {
                    public static int perkTier = 4;
                    public static int adaptionSideDefensePointsGained = 3;
                    public static string desc = $"Adaption side defense points gained increased to {adaptionSideDefensePointsGained}.";
                }
                */


            }

            public static class ArmorMastery
            {

                public enum status { notActive, levelOne, levelTwo }

                public static string perkName = "Armor Mastery";

                public static class LevelOneData
                {
                    public static int perkTier = 1;
                    public static int armorHitChanceIncrease = 10;
                    public static string desc = $"Chance for your armor to be hit instead of your health increased by {armorHitChanceIncrease}.";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 3;
                    public static int armorHitChanceIncrease = 20;
                    public static string desc = $"Chance for your armor to be hit instead of your health increased by {armorHitChanceIncrease}.";
                }


            }

            public static class BlockMastery
            {

                public enum status { notActive, levelOne, levelTwo, levelThree }

                public static string perkName = "Block Mastery";

                public static class LevelOneData
                {
                    public static int perkTier = 2;
                    public static int blockRollIncrease = 1;
                    public static string desc = $"Every blocking roll you make is increased by {blockRollIncrease}.";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 3;
                    public static int blockRollIncrease = 2;
                    public static string desc = $"Every blocking roll you make is increased by {blockRollIncrease}.";
                }

                public static class LevelThreeData
                {
                    public static int perkTier = 4;
                    public static int blockRollIncrease = 3;
                    public static string desc = $"Every blocking roll you make is increased by {blockRollIncrease}.";
                }


            }

            public static class Vitality
            {

                public enum status { notActive, levelOne}

                public static string perkName = "Vitality";

                public static class LevelOneData
                {
                    public static int perkTier = 4;
                    public static int healthPerEnduranceIncrease = 1;
                    public static string desc = $"You get {healthPerEnduranceIncrease} more health per endurance point";
                }
            }
        }

        public static class Scoundrel
        {
            public static class DeceptiveTechnique
            {

                public enum status { notActive, levelOne, levelTwo, LevelThree }

                public static string perkName = "Deceptive Technique";

                public static class LevelOneData
                {
                    public static int perkTier = 1;
                    public static int chanceToIgnoreSideDefensePoints = 25;
                    public static string desc = $"You have a {chanceToIgnoreSideDefensePoints} percent chance to ignore the Side Defense " +
                        $"points of the enemy. This checks both the block chance and block protection roll";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 2;
                    public static int chanceToIgnoreSideDefensePoints = 50;
                    public static string desc = $"You have a {chanceToIgnoreSideDefensePoints} percent chance to ignore the Side Defense " +
                        $"points of the enemy. This checks both the block chance and block protection roll";
                }

                public static class LevelThreeData
                {
                    public static int perkTier = 3;
                    public static int chanceToIgnoreSideDefensePoints = 75;
                    public static string desc = $"You have a {chanceToIgnoreSideDefensePoints} percent chance to ignore the Side Defense " +
                        $"points of the enemy. This checks both the block chance and block protection roll";
                }
            }

            public static class CriticalHit
            {
                public enum status { notActive, levelOne, levelTwo, LevelThree }

                public static string perkName = "Critical Hit";

                public static class LevelOneData
                {
                    public static int perkTier = 1;
                    public static int chanceToCrit = 10;
                    public static string desc = $"You have a {chanceToCrit} percent chance to deal double damage on a clean hit";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 2;
                    public static int chanceToCrit = 15;
                    public static string desc = $"You have a {chanceToCrit} percent chance to deal double damage on a clean hit";
                }

                public static class LevelThreeData
                {
                    public static int perkTier = 4;
                    public static int chanceToCrit = 25;
                    public static string desc = $"You have a {chanceToCrit} percent chance to deal double damage on a clean hit";
                }
            }

            public static class FoulStance
            {
                public enum status { notActive, levelOne, levelTwo }

                public static string perkName = "Foul Stance";

                public static class LevelOneData
                {
                    public static int perkTier = 2;
                    public static int chanceToReflect = 5;
                    public static int damagePotentialPercentReflected = 50;
                    public static string desc = $"You have a {chanceToReflect} percent chance to reflect {damagePotentialPercentReflected} percent" +
                        $" of the enemy attacks full damage potential (what would be dealt in a clean hit) regardless of the outcome of the attack";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 4;
                    public static int chanceToReflect = 10;
                    public static int damagePotentialPercentReflected = 50;
                    public static string desc = $"You have a {chanceToReflect} percent chance to reflect {damagePotentialPercentReflected} percent" +
                        $" of the enemy attacks full damage potential (what would be dealt in a clean hit) regardless of the outcome of the attack";
                }

              
            }

            public static class Skip
            {
                public enum status { notActive, levelOne, levelTwo, LevelThree }

                public static string perkName = "Skip";

                public static class LevelOneData
                {
                    public static int perkTier = 2;
                    public static int chanceToMakeEnemySkip = 2;
                    public static string desc = $"You have a {chanceToMakeEnemySkip} percent chance to make the enemy skip this round";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 3;
                    public static int chanceToMakeEnemySkip = 4;
                    public static string desc = $"You have a {chanceToMakeEnemySkip} percent chance to make the enemy skip this round";
                }

                public static class LevelThreeData
                {
                    public static int perkTier = 4;
                    public static int chanceToMakeEnemySkip = 6;
                    public static string desc = $"You have a {chanceToMakeEnemySkip} percent chance to make the enemy skip this round";
                }
            }

            public static class Slippery
            {

                public enum status { notActive, levelOne }

                public static string perkName = "Slippery";

                public static class LevelOneData
                {
                    public static int perkTier = 3;
                    public static string desc = $"Dodge chance also uses Awareness in addition to Reflex";
                }
            }

            public static class KeenEye
            {

                public enum status { notActive, levelOne }

                public static string perkName = "Keen Eye";

                public static class LevelOneData
                {
                    public static int perkTier = 2;
                    public static string desc = $"Your Awareness check rolls are doubled";
                }
            }
        }

        public static class Warrior
        {
            public static class Savage
            {

                public enum status { notActive, levelOne, levelTwo, LevelThree }

                public static string perkName = "Savage";

                public static class LevelOneData
                {
                    public static int perkTier = 2;
                    public static int strengthMultiplierIncreased = 10;
                    public static string desc = $"Your Strength Multiplier is increased on every weapon by {strengthMultiplierIncreased} percent";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 3;
                    public static int strengthMultiplierIncreased = 20;
                    public static string desc = $"Your Strength Multiplier is increased on every weapon by {strengthMultiplierIncreased} percent";
                }

                public static class LevelThreeData
                {
                    public static int perkTier = 4;
                    public static int strengthMultiplierIncreased = 30;
                    public static string desc = $"Your Strength Multiplier is increased on every weapon by {strengthMultiplierIncreased} percent";
                }
            }

            public static class AttackMastery
            {

                public enum status { notActive, levelOne, levelTwo, LevelThree }

                public static string perkName = "Attack Mastery";

                public static class LevelOneData
                {
                    public static int perkTier = 1;
                    public static int attackRollIncrease = 1;
                    public static string desc = $"Every attack roll you make is increased by {attackRollIncrease}";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 2;
                    public static int attackRollIncrease = 2;
                    public static string desc = $"Every attack roll you make is increased by {attackRollIncrease}";
                }

                public static class LevelThreeData
                {
                    public static int perkTier = 3;
                    public static int attackRollIncrease = 3;
                    public static string desc = $"Every attack roll you make is increased by {attackRollIncrease}";
                }
            }

            public static class Breakthrough
            {

                public enum status { notActive, levelOne, levelTwo, LevelThree }

                public static string perkName = "Breakthrough";

                public static class LevelOneData
                {
                    public static int perkTier = 1;
                    public static int chanceToDoubleAttackRolls = 10;
                    public static string desc = $"You have {chanceToDoubleAttackRolls} percent chance to double your attack rolls";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 3;
                    public static int chanceToDoubleAttackRolls = 20;
                    public static string desc = $"You have {chanceToDoubleAttackRolls} percent chance to double your attack rolls";
                }

                public static class LevelThreeData
                {
                    public static int perkTier = 4;
                    public static int chanceToDoubleAttackRolls = 30;
                    public static string desc = $"You have {chanceToDoubleAttackRolls} percent chance to double your attack rolls";
                }
            }

            public static class Lacerate
            {

                public enum status { notActive, levelOne, levelTwo}

                public static string perkName = "Lacerate";

                public static class LevelOneData
                {
                    public static int perkTier = 1;
                    public static int cleanAttacksCountBleedMultiplier = 1;
                    public static string desc = $"The amount of clean attacks you have landed in a fight is counted " +
                        $"and that number is dealt to the enemy as health damage on every attack you make, regardless of the outcome of the attack";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 3;
                    public static int cleanAttacksCountBleedMultiplier = 2;
                    public static string desc = $"The amount of clean attacks you have landed in a fight is counted " +
                        $"and that number multiplied by {cleanAttacksCountBleedMultiplier}" +
                        $" is dealt to the enemy as health damage on every attack you make, regardless of the outcome of the attack";
                }

                
            }

            public static class Revenge
            {

                public enum status { notActive, levelOne, levelTwo }

                public static string perkName = "Revenge";

                public static class LevelOneData
                {
                    public static int perkTier = 2;
                    public static int attackRollIncreased = 1;
                    public static int damagePercentStored = 20;
                    public static string desc = $"If you have been hit clearly in the last round your attack roll is increased" +
                        $" in the next round by {attackRollIncreased}" +
                        $" and if you achieve a clean hit {damagePercentStored} percent of the damage you have taken the last round" +
                        $"is dealt to the enemy";
                }

                public static class LevelTwoData
                {
                    public static int perkTier = 4;
                    public static int attackRollIncreased = 2;
                    public static int damagePercentStored = 30;
                    public static string desc = $"If you have been hit clearly in the last round your attack roll is increased" +
                        $" in the next round by {attackRollIncreased}" +
                        $" and if you achieve a clean hit {damagePercentStored} percent of the damage you have taken the last round" +
                        $"is dealt to the enemy";
                }


            }
        }
    }

}
