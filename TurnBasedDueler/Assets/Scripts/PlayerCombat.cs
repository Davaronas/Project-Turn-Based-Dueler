using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private PlayerUI playerUI;

    private CombatManager combatManager;

    public CombatAgent playerAgent;

    private EnemyCombat[] currentEnemies;
    public EnemyCombat currentTarget;

    void Start()
    {
        combatManager = GetComponent<CombatManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerUI = GetComponent<PlayerUI>();
        playerUI.SetWidgetState(false);

        

        Cursor.lockState = CursorLockMode.Locked;

        playerAgent = GameLogic.CalculateAgentStatsFromBaseStats(playerAgent);

        CombatManager.OnBarAndEnemyNameRefresh?.Invoke(playerAgent, (CombatAgent)ScriptableObject.CreateInstance("CombatAgent"));
        playerUI.SetBarState(true, false);

        SwitchOpponentButton.OnOpponentSwitch += SwitchToOpponent;
        CombatAgent.OnAgentDeath += AgentDeath;
    }

    private void OnDestroy()
    {
        SwitchOpponentButton.OnOpponentSwitch -= SwitchToOpponent;
        CombatAgent.OnAgentDeath -= AgentDeath;
    }

    private void AgentDeath(CombatAgent _ca)
    {
        if (_ca == playerAgent)
        {
            combatManager.PlayerDeath();
        }
    }

    void Update()
    {
        
    }

    public bool TargetLeftNeighbourExists()
    {
        return currentTarget.enemyPosition.GetLeftNeighbour() != null;
    }

    public bool TargetRightNeighbourExists()
    {
        return currentTarget.enemyPosition.GetRightNeighbour() != null;
    }

    public void CombatInitiated(Transform _targetTransform,CombatTrigger _trigger)
    {
        playerMovement.SetPositionAndRotation(_targetTransform);
        playerMovement.enabled = false;
        playerUI.SetWidgetState(true);
        Cursor.lockState = CursorLockMode.None;

        currentEnemies = _trigger.enemies.ToArray();
        currentTarget = currentEnemies[0];
        //transform.LookAt(currentEnemies[0].transform);

        playerUI.SetSwitchButtonsState(true);

       // combatManager.CombatInitiated(_enemy);
    }

    private void SwitchToOpponent(SwitchOpponentButton.SwitchToOpponent _side)
    {
        if(_side == SwitchOpponentButton.SwitchToOpponent.Left)
        {
            currentTarget = currentTarget.enemyPosition.GetRightNeighbour();
          //  transform.LookAt(currentTarget.transform);
        }
        else
        {
            currentTarget = currentTarget.enemyPosition.GetLeftNeighbour();
         //   transform.LookAt(currentTarget.transform);
        }

        playerMovement.SetRotation(Quaternion.LookRotation(currentTarget.enemyPosition.transform.position - transform.position),8);
        CombatManager.OnBarAndEnemyNameRefresh?.Invoke(playerAgent,currentTarget.enemyAgent);

        playerUI.SetSwitchButtonsState(true); // check for neighbours
    }

    public void PlayerTurn()
    {
        // Awareness checks


        playerUI.SetWidgetState(true);
        playerUI.SetBarState(true,true);
        playerUI.SetSwitchButtonsState(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideInput()
    {
        playerUI.SetWidgetState(false);
        playerUI.SetSwitchButtonsState(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CombatEnded()
    {
        playerMovement.enabled = true;
        playerUI.SetWidgetState(false);
        playerUI.SetBarState(true,false);
        Cursor.lockState = CursorLockMode.Locked;

        currentEnemies = null;
        currentTarget = null;
    }
}
