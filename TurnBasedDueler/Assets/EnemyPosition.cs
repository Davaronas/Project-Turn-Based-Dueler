using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPosition : MonoBehaviour
{
    public EnemyCombat enemyInThisPosition;
    public EnemyPosition leftNeighbour;
    public EnemyPosition rightNeighbour;

    public void SetEnemy(EnemyCombat _enemy)
    {
        enemyInThisPosition = _enemy;
    }

    public EnemyCombat GetLeftNeighbour()
    {
        if(leftNeighbour != null)
        {
            return leftNeighbour.enemyInThisPosition;
        }
        else
        {
            return null;
        }

        
       

        
    }

    public EnemyCombat GetRightNeighbour()
    {
        if (rightNeighbour != null)
        {
            return rightNeighbour.enemyInThisPosition;
        }
        else
        {
            return null;
        }
    }
}
