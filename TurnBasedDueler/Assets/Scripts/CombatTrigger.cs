using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{


    [SerializeField] private CombatAgent enemyAgentBlueprint;
    [SerializeField] private AgentPositions agentPositions;
    [SerializeField] public List<EnemyCombat> enemies = new List<EnemyCombat>();
    [SerializeField] private GameObject enemyPrefab;


    private CombatManager combatManager = null;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < agentPositions.enemyPositions.Length; i++)
        {
           
                
               EnemyCombat _newEnemy = Instantiate(enemyPrefab, agentPositions.enemyPositions[i].transform.position,
                Quaternion.LookRotation(agentPositions.playerPositionTransform.position - agentPositions.enemyPositions[i].transform.position)).GetComponent<EnemyCombat>();
            _newEnemy.SetAgentPosition(agentPositions.enemyPositions[i]);
            agentPositions.enemyPositions[i].SetEnemy(_newEnemy);

            enemies.Add(_newEnemy);


        }


        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCombat>().CombatInitiated(agentPositions.playerPositionTransform,this);
            combatManager.CombatInitiated(enemies.ToArray());
        }
    }

}
