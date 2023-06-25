using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;



public class PlayerInput
{
    public PlayerInput (CombatDirection _direction, EnemyCombat _target)
    {
        direction = _direction;
        target = _target;
    }

    public CombatDirection direction;
    public EnemyCombat target;
}

public class CombatManager : MonoBehaviour
{
    private PlayerCombat player;
    private List<EnemyCombat> enemies = new List<EnemyCombat>();
    private bool isCombatActive = false;

    private PlayerInput playerInput = null;
    private List<CombatDirection> enemyAttackDirections = new List<CombatDirection>();

    public static Action<int> OnPredictPointsChange;
    public static Action<CombatAgent, CombatAgent> OnBarAndEnemyNameRefresh;

    public static Action<string> OnEventTextCreation;
    public static Action<string,int, int> OnNumbersEventTextCreation;
    public static Action OnClearEventDisplay;


    void Start()
    {
        player = GetComponent<PlayerCombat>();
        CombatWidgetButton.OnCombatWidgetButtonPressedAttack += PlayerAttackInput;
        CombatWidgetButton.OnCombatWidgetButtonPressedPredict += PlayerPredictInput;

        //CombatAgent.OnAgentDeath += AgentDeath;
    }

    private void OnDestroy()
    {
        CombatWidgetButton.OnCombatWidgetButtonPressedAttack -= PlayerAttackInput;
        CombatWidgetButton.OnCombatWidgetButtonPressedPredict -= PlayerPredictInput;

      //  CombatAgent.OnAgentDeath -= AgentDeath;
    }


    void Update()
    {
        
    }

    public void CombatInitiated(EnemyCombat[] _enemies)
    {
        for (int i = 0; i < _enemies.Length; i++)
        {
            enemies.Add(_enemies[i]);
        }
        
        isCombatActive = true;
        OnPredictPointsChange?.Invoke(player.playerAgent.currentPredictionSideDefensePoints);
        OnBarAndEnemyNameRefresh?.Invoke(player.playerAgent, player.currentTarget.enemyAgent);
        StartCoroutine(Combat());
    }



    private void PlayerPredictInput(CombatDirection _direction)
    {
        switch(_direction)
        {
            case CombatDirection.BottomLeft:
                
                if (!player.playerAgent.BL.isPredicted)
                {
                    if(player.playerAgent.currentPredictionSideDefensePoints > 0)
                    {
                        player.playerAgent.BL.predictionPoints += player.playerAgent.predictionSideDefenseGain;
                        player.playerAgent.BL.isPredicted = true;

                        player.playerAgent.currentPredictionSideDefensePoints--;
                    }
                }
                else
                {
                    player.playerAgent.BL.predictionPoints -= player.playerAgent.predictionSideDefenseGain;
                    player.playerAgent.BL.isPredicted = false;

                    player.playerAgent.currentPredictionSideDefensePoints++;
                }

                break;
            case CombatDirection.BottomRight:

                if (!player.playerAgent.BR.isPredicted)
                {
                    if (player.playerAgent.currentPredictionSideDefensePoints > 0)
                    {
                        player.playerAgent.BR.predictionPoints += player.playerAgent.predictionSideDefenseGain;
                        player.playerAgent.BR.isPredicted = true;

                        player.playerAgent.currentPredictionSideDefensePoints--;
                    }
                }
                else
                {
                    player.playerAgent.BR.predictionPoints -= player.playerAgent.predictionSideDefenseGain;
                    player.playerAgent.BR.isPredicted = false;

                    player.playerAgent.currentPredictionSideDefensePoints++;
                }


                break;
            case CombatDirection.TopLeft:

                if (!player.playerAgent.TL.isPredicted)
                {
                    if (player.playerAgent.currentPredictionSideDefensePoints > 0)
                    {
                        player.playerAgent.TL.predictionPoints += player.playerAgent.predictionSideDefenseGain;
                        player.playerAgent.TL.isPredicted = true;

                        player.playerAgent.currentPredictionSideDefensePoints--;
                    }
                }
                else
                {
                    player.playerAgent.TL.predictionPoints -= player.playerAgent.predictionSideDefenseGain;
                    player.playerAgent.TL.isPredicted = false;

                    player.playerAgent.currentPredictionSideDefensePoints++;
                }


                break;
            case CombatDirection.TopRight:

                if (!player.playerAgent.TR.isPredicted)
                {
                    if (player.playerAgent.currentPredictionSideDefensePoints > 0)
                    {
                        player.playerAgent.TR.predictionPoints += player.playerAgent.predictionSideDefenseGain;
                        player.playerAgent.TR.isPredicted = true;

                        player.playerAgent.currentPredictionSideDefensePoints--;
                    }
                }
                else
                {
                    player.playerAgent.TR.predictionPoints -= player.playerAgent.predictionSideDefenseGain;
                    player.playerAgent.TR.isPredicted = false;

                    player.playerAgent.currentPredictionSideDefensePoints++;
                }


                break;
                
        }

        OnPredictPointsChange?.Invoke(player.playerAgent.currentPredictionSideDefensePoints);
    }

    private void PlayerAttackInput(CombatDirection _direction, EnemyCombat _target)
    {
        playerInput = new PlayerInput(_direction, _target);
        player.HideInput();
    }

    
    public void EnemyDeath(EnemyCombat _enemy)
    {
        if(_enemy != player.playerAgent)
        {
            enemies.Remove(_enemy);
            Destroy(_enemy.gameObject);

            if (enemies.Count > 0)
            {
                player.currentTarget = enemies[0];
            }
            else
            {
                CombatEnded();
            }
        }
    }


    public void PlayerDeath()
    {
        Debug.Log("Player died");
        isCombatActive = false;
    }

    private void CombatEnded()
    {
        isCombatActive = false;
        OnClearEventDisplay?.Invoke();

        // Clear side defense points, and isPredicted!
        // Clear revenge, lacerate
        player.CombatEnded();

        player.playerAgent.cleanAttacksLandedThisFight = 0;
        player.playerAgent.revengeActive = false;
        player.playerAgent.agentSkipsThisRound = false;
        player.playerAgent.bonusDamage.SetToZero();

        player.playerAgent.BL.adaptionDecayTurns = 0;
        player.playerAgent.BL.adaptionPoints = 0;
        player.playerAgent.BL.predictionPoints = 0;
        player.playerAgent.BL.isPredicted = false;


        player.playerAgent.BR.adaptionDecayTurns = 0;
        player.playerAgent.BR.adaptionPoints = 0;
        player.playerAgent.BR.predictionPoints = 0;
        player.playerAgent.BR.isPredicted = false;


        player.playerAgent.TL.adaptionDecayTurns = 0;
        player.playerAgent.TL.adaptionPoints = 0;
        player.playerAgent.TL.predictionPoints = 0;
        player.playerAgent.TL.isPredicted = false;

        player.playerAgent.TR.adaptionDecayTurns = 0;
        player.playerAgent.TR.adaptionPoints = 0;
        player.playerAgent.TR.predictionPoints = 0;
        player.playerAgent.TR.isPredicted = false;
    }


    private void AgentAction(CombatAgent _attacker, CombatAgent _receiver)
    {
        // Lacerate
        int _lacerate = _attacker.cleanAttacksLandedThisFight * _attacker.lacerateMultiplier;
        if (_lacerate != 0)
        {
            _receiver.DealDamage(new Vector2Int(_lacerate,0));
          //  _receiver.currentHealth -= _attacker.cleanAttacksLandedThisFight * _attacker.lacerateMultiplier;
            OnEventTextCreation?.Invoke(GameData.PerkData.Warrior.Lacerate.perkName);
            OnNumbersEventTextCreation?.Invoke(_receiver.agentName, _attacker.cleanAttacksLandedThisFight * _attacker.lacerateMultiplier, 0);
        }

        //---------- KILL CHECK ----------

#region Reflect
        int _reflectRandom = Random.Range(1, 101);
        if(_reflectRandom <= _receiver.reflectChance)
        {
            float _reflectPercent = ((float)_receiver.reflectFullDamagePotentialPercent / 100);
            Vector2Int _reflectDamage = GameLogic.CalculateRawDamage(_attacker, _receiver);
            _reflectDamage.x = Mathf.FloorToInt((float)_reflectDamage.x * _reflectPercent);
            _reflectDamage.y = Mathf.FloorToInt((float)_reflectDamage.y * _reflectPercent);

            OnEventTextCreation?.Invoke(GameData.PerkData.Scoundrel.FoulStance.perkName);
            OnNumbersEventTextCreation?.Invoke(_attacker.agentName, _reflectDamage.x, _reflectDamage.y);

            _attacker.DealDamage(_reflectDamage);

          //  _attacker.currentHealth -= _reflectDamage.x;
           // _attacker.currentArmorPoints -= _reflectDamage.y;
            //---------- KILL CHECK ----------
        }
#endregion


#region Side Defense
        switch (_attacker.attackDirection) // sides are opposite, top right attack is top left for the receiver
        {
            case CombatDirection.BottomLeft:
                _receiver.BR.adaptionPoints += _receiver.adaptionPointsGainedPerHit;
                _receiver.BR.adaptionDecayTurns = _receiver.adaptionPointsDecayInTurns + 1; // at the end of this turn one gets subtracted
                
           
                
                break;
            case CombatDirection.BottomRight:
                _receiver.BL.adaptionPoints += _receiver.adaptionPointsGainedPerHit;
                _receiver.BL.adaptionDecayTurns = _receiver.adaptionPointsDecayInTurns + 1;

            

                
                break;
            case CombatDirection.TopLeft:
                _receiver.TR.adaptionPoints += _receiver.adaptionPointsGainedPerHit;
                _receiver.TR.adaptionDecayTurns = _receiver.adaptionPointsDecayInTurns + 1;

             

                break;
            case CombatDirection.TopRight:
                _receiver.TL.adaptionPoints += _receiver.adaptionPointsGainedPerHit;
                _receiver.TL.adaptionDecayTurns = _receiver.adaptionPointsDecayInTurns + 1;

              

                break;
        }
#endregion


        int _attack = GameLogic.CalculateAttackRoll(_attacker);
        bool _ignoreSidedefense = false;

        if(!GameLogic.DodgeCheck(_attack, _receiver))
        {
            if(!GameLogic.BlockCheck(_attack,_attacker,_receiver,out _ignoreSidedefense))
            {
                Vector2Int _damage = GameLogic.CalculateDamage(_attacker, _receiver);

                OnEventTextCreation("Clean Hit");
                OnNumbersEventTextCreation(_receiver.agentName, _damage.x, _damage.y);

                _receiver.DealDamage(_damage);

               // _receiver.currentHealth -= _damage.x;
               // _receiver.currentArmorPoints -= _damage.y;

               

#region Revenge
                switch (_receiver.baseStats.perks.revenge)
                {
                    case GameData.PerkData.Warrior.Revenge.status.levelOne:
                        _receiver.bonusDamage *= GameData.PerkData.Warrior.Revenge.LevelOneData.damagePercentStored;
                        _receiver.bonusAttackRoll += GameData.PerkData.Warrior.Revenge.LevelOneData.attackRollIncreased;
                        _receiver.revengeActive = true;
                        break;

                    case GameData.PerkData.Warrior.Revenge.status.levelTwo:
                        _receiver.bonusDamage *= GameData.PerkData.Warrior.Revenge.LevelTwoData.damagePercentStored;
                        _receiver.bonusAttackRoll += GameData.PerkData.Warrior.Revenge.LevelTwoData.attackRollIncreased;
                        _receiver.revengeActive = true;
                        break;
                }


                if (_attacker.revengeActive)
                {
                    switch (_attacker.baseStats.perks.revenge)
                    {
                        case GameData.PerkData.Warrior.Revenge.status.levelOne:
                            _receiver.bonusDamage.SetToZero();
                            _receiver.bonusAttackRoll -= GameData.PerkData.Warrior.Revenge.LevelOneData.attackRollIncreased;
                            _receiver.revengeActive = false;
                            break;

                        case GameData.PerkData.Warrior.Revenge.status.levelTwo:
                            _receiver.bonusDamage.SetToZero();
                            _receiver.bonusAttackRoll -= GameData.PerkData.Warrior.Revenge.LevelTwoData.attackRollIncreased;
                            _receiver.revengeActive = false;
                            break;
                    }
                }
#endregion

                // Lacerate
                if (_attacker.lacerateMultiplier > 0)
                {
                    _attacker.cleanAttacksLandedThisFight++;
                }

                //---------- KILL CHECK ----------

               
            }
            else
            {
                float _blockPercent = 1 - (float)GameLogic.BlockProtectionCalculation(_attack, _attacker, _receiver,_ignoreSidedefense) / 100;
                Vector2Int _damage = GameLogic.CalculateDamage(_attacker,_receiver);
                _damage.x = Mathf.FloorToInt((float)_damage.x * _blockPercent);
                _damage.y = Mathf.FloorToInt((float)_damage.y * _blockPercent);

                OnEventTextCreation?.Invoke("Block "+ (1 - _blockPercent) * 100 + " %");
                OnNumbersEventTextCreation?.Invoke(_receiver.agentName, _damage.x, _damage.y);

                _receiver.DealDamage(_damage);

              //  _receiver.currentHealth -= _damage.x;
              //  _receiver.currentArmorPoints -= _damage.y;

                // CHeck if kill, try to switch target if not player

                
            }
        }
        else
        {
            OnEventTextCreation?.Invoke("Dodge");
            
        }

        // Collect all perk activations and display on screen
        // Damage to health and armor display on screen

    }


    IEnumerator Combat()
    {
        #region Enemy Predictions
        for (int i = 0; i < enemies.Count; i++)
        {
            CombatDirection _d = (CombatDirection)Random.Range(0, 4);
            CombatDirection _next = CombatDirection.BottomLeft;
            switch(_d)
            {
                case CombatDirection.BottomLeft:
                    enemies[i].enemyAgent.BL.isPredicted = true;
                    enemies[i].enemyAgent.BL.predictionPoints += enemies[i].enemyAgent.predictionSideDefenseGain;
                    _next = CombatDirection.BottomRight;
                break;
                case CombatDirection.BottomRight:
                    enemies[i].enemyAgent.BR.isPredicted = true;
                    enemies[i].enemyAgent.BR.predictionPoints += enemies[i].enemyAgent.predictionSideDefenseGain;
                    _next = CombatDirection.TopLeft;
                    break;
                case CombatDirection.TopLeft:
                    enemies[i].enemyAgent.TL.isPredicted = true;
                    enemies[i].enemyAgent.TL.predictionPoints += enemies[i].enemyAgent.predictionSideDefenseGain;
                    _next = CombatDirection.TopRight;
                    break;
                case CombatDirection.TopRight:
                    enemies[i].enemyAgent.TR.isPredicted = true;
                    enemies[i].enemyAgent.TR.predictionPoints += enemies[i].enemyAgent.predictionSideDefenseGain;
                    _next = CombatDirection.BottomLeft;
                    break;
            }
            enemies[i].enemyAgent.currentPredictionSideDefensePoints--;


            for (int j = 0; j < enemies[i].enemyAgent.currentPredictionSideDefensePoints; j++)
            {
                switch (_next)
                {
                    case CombatDirection.BottomLeft:
                        enemies[i].enemyAgent.BL.isPredicted = true;
                        enemies[i].enemyAgent.BL.predictionPoints += enemies[i].enemyAgent.predictionSideDefenseGain;
                        _next = CombatDirection.BottomRight;
                        break;
                    case CombatDirection.BottomRight:
                        enemies[i].enemyAgent.BR.isPredicted = true;
                        enemies[i].enemyAgent.BR.predictionPoints += enemies[i].enemyAgent.predictionSideDefenseGain;
                        _next = CombatDirection.TopLeft;
                        break;
                    case CombatDirection.TopLeft:
                        enemies[i].enemyAgent.TL.isPredicted = true;
                        enemies[i].enemyAgent.TL.predictionPoints += enemies[i].enemyAgent.predictionSideDefenseGain;
                        _next = CombatDirection.TopRight;
                        break;
                    case CombatDirection.TopRight:
                        enemies[i].enemyAgent.TR.isPredicted = true;
                        enemies[i].enemyAgent.TR.predictionPoints += enemies[i].enemyAgent.predictionSideDefenseGain;
                        _next = CombatDirection.BottomLeft;
                        break;
                }
                enemies[i].enemyAgent.currentPredictionSideDefensePoints--;
            }
        }
#endregion



        while (isCombatActive)
        {
            // Set enemy attack directions now, so awareness checks can run before enemy turns
            enemyAttackDirections.Clear();
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].enemyAgent.attackDirection = (CombatDirection)Random.Range(0, 4);
                enemyAttackDirections.Add(enemies[i].enemyAgent.attackDirection);
            }

            print(player.playerAgent.agentSkipsThisRound);

            if (!player.playerAgent.agentSkipsThisRound)
            {
                player.PlayerTurn();

                // Player Turn
                while (playerInput == null)
                {
                    yield return new WaitForEndOfFrame();
                }
                player.playerAgent.attackDirection = playerInput.direction;


                // PlayerAction
                AgentAction(player.playerAgent, player.currentTarget.enemyAgent);

                // Set Side defense adaption decay


                if (player.playerAgent.baseStats.perks.skip != GameData.PerkData.Scoundrel.Skip.status.notActive)
                {
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        int _skipRandom = Random.Range(1, 101);
                        if (_skipRandom <= player.playerAgent.makeEnemySkipChance)
                        {
                            enemies[i].enemyAgent.agentSkipsThisRound = true;
                        }
                    }
                }

                // side defense decay



               
                playerInput = null;

                if (player.currentTarget != null)
                {
                    OnBarAndEnemyNameRefresh?.Invoke(player.playerAgent, player.currentTarget.enemyAgent);
                }
                else
                {
                    OnBarAndEnemyNameRefresh?.Invoke(player.playerAgent, new CombatAgent());
                    yield break;
                }

                // animation lenght + 1 second
            }
            else
            {
                player.playerAgent.agentSkipsThisRound = false;
                OnEventTextCreation?.Invoke(player.playerAgent.agentName + ": " + GameData.PerkData.Scoundrel.Skip.perkName);
            }

            yield return new WaitForSeconds(1f);

            OnClearEventDisplay?.Invoke();


            for (int i = 0; i < enemies.Count; i++)
            {
                player.playerMovement.SetRotation(Quaternion.LookRotation(enemies[i].enemyPosition.transform.position - transform.position), 8);
               

                if (!enemies[i].enemyAgent.agentSkipsThisRound)
                {
                    // enemies[i] Action
                    AgentAction(enemies[i].enemyAgent, player.playerAgent);

                    if (enemies[i].enemyAgent.baseStats.perks.skip != GameData.PerkData.Scoundrel.Skip.status.notActive)
                    {
                        print("Roll skip");
                        int _skipRandom = Random.Range(1, 101);
                        if (_skipRandom <= enemies[i].enemyAgent.makeEnemySkipChance)
                        {
                            player.playerAgent.agentSkipsThisRound = true;
                        }
                    }


                    OnBarAndEnemyNameRefresh?.Invoke(player.playerAgent, enemies[i].enemyAgent);


                    // animation lenght + 1 second
                }
                else
                {
                    enemies[i].enemyAgent.agentSkipsThisRound = false;
                    OnEventTextCreation?.Invoke(enemies[i].enemyAgent.agentName + ": " + GameData.PerkData.Scoundrel.Skip.perkName);
                }

                yield return new WaitForSeconds(1f);



#region Side Defense decay enemy
                enemies[i].enemyAgent.BL.adaptionDecayTurns--;
                if (enemies[i].enemyAgent.BL.adaptionDecayTurns <= 0)
                {
                    enemies[i].enemyAgent.BL.adaptionPoints = 0;
                }

                enemies[i].enemyAgent.BR.adaptionDecayTurns--;
                if (enemies[i].enemyAgent.BL.adaptionDecayTurns <= 0)
                {
                    enemies[i].enemyAgent.BL.adaptionPoints = 0;
                }


                enemies[i].enemyAgent.TL.adaptionDecayTurns--;
                if (enemies[i].enemyAgent.TL.adaptionDecayTurns <= 0)
                {
                    enemies[i].enemyAgent.TL.adaptionPoints = 0;
                }


                enemies[i].enemyAgent.TR.adaptionDecayTurns--;
                if (enemies[i].enemyAgent.TR.adaptionDecayTurns <= 0)
                {
                    enemies[i].enemyAgent.TR.adaptionPoints = 0;
                }
#endregion

                OnClearEventDisplay?.Invoke();

                //  OnBarAndEnemyNameRefresh?.Invoke(player.playerAgent, player.currentTarget.enemyAgent);

            }

            #region Side Defense decay player
            player.playerAgent.BL.adaptionDecayTurns--;
            if (player.playerAgent.BL.adaptionDecayTurns <= 0)
            {
                player.playerAgent.BL.adaptionPoints = 0;
            }

            player.playerAgent.BR.adaptionDecayTurns--;
            if (player.playerAgent.BR.adaptionDecayTurns <= 0)
            {
                player.playerAgent.BR.adaptionPoints = 0;
            }


            player.playerAgent.TL.adaptionDecayTurns--;
            if (player.playerAgent.TL.adaptionDecayTurns <= 0)
            {
                player.playerAgent.TL.adaptionPoints = 0;
            }


            player.playerAgent.TR.adaptionDecayTurns--;
            if (player.playerAgent.TR.adaptionDecayTurns <= 0)
            {
                player.playerAgent.TR.adaptionPoints = 0;
            }
#endregion

            player.playerMovement.SetRotation(Quaternion.LookRotation(player.currentTarget.enemyPosition.transform.position - transform.position), 8);
            OnBarAndEnemyNameRefresh?.Invoke(player.playerAgent, player.currentTarget.enemyAgent);

            yield return new WaitForEndOfFrame();

        }


        // check both side healths
        // if one side is zero
       // enemies = null;
        //isCombatActive = false;
    }


}
