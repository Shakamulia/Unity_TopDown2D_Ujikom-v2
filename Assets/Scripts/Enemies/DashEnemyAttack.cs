using UnityEngine;

public class DashEnemyAttack : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyDash enemyDash; // Reference to your dash component
    
    public void Attack()
    {
        // This will be called when the enemy attacks
        if (enemyDash != null)
        {
            enemyDash.ExecuteDash();
        }
    }
}