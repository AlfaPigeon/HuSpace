using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderController : MonoBehaviour
{
    public Enemy enemy;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && enemy.enemyType == Enemy.EnemyType.Scoopy)
        {
            Debug.Log("Scoopy Hit");
        }
    }
}
