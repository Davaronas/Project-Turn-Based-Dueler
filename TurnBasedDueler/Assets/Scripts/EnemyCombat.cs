using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
   public CombatAgent enemyAgent;
    public EnemyPosition enemyPosition;
    private CombatManager combatManager;

    // Start is called before the first frame update
    void Awake()
    {
        enemyAgent = GameLogic.CalculateAgentStatsFromBaseStats(enemyAgent);
        combatManager = FindObjectOfType<CombatManager>();


        CombatAgent.OnAgentDeath += AgentDeath;
    }


    private void OnDestroy()
    {
        CombatAgent.OnAgentDeath -= AgentDeath;
    }



    private void AgentDeath(CombatAgent _ca)
    {
        if(_ca == enemyAgent)
        {
            combatManager.EnemyDeath(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAgentPosition(EnemyPosition _pos)
    {
        enemyPosition = _pos;
    }


  

}
