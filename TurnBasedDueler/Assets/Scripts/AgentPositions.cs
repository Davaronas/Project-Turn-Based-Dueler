using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPositions : MonoBehaviour
{
    public Transform playerPositionTransform;
    public EnemyPosition[] enemyPositions;

    [Space]
    [SerializeField] private float gizmoRadius = 5f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,gizmoRadius);
    }

}
