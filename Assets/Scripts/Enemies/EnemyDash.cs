using UnityEngine;
using System.Collections;
public class EnemyDash : EnemyAI 
{
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeedMultiplier = 3f; 
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private TrailRenderer dashTrail; // Assign di Inspector

    public bool isDashing { get; private set; }

    private float originalMoveSpeed;

   protected override void Start()
   {
      base.Start();
      originalMoveSpeed = enemyPathfinding.moveSpeed;  // capture speed
   }

// Override Attacking Behavior untuk Dash
    protected override void Attacking()
    {
        if (playerTarget == null)
        {
            state = State.Roaming;
            return;
        }

        float dist = Vector2.Distance(transform.position, playerTarget.position);
        if (dist <= attackRange && !isDashing && canAttack)
            StartCoroutine(DashRoutine());
        else if (!isDashing && dist > attackRange)
            state = State.Roaming;
    }

    public void ExecuteDash()
    {
        if (!isDashing)
        {
            StartCoroutine(DashRoutine());
        }
    }

   private IEnumerator DashRoutine()
   {
      isDashing = true;
      canAttack = false;

      //Hitung arah dash ke player
      Vector2 dashDirection = (playerTarget.position - transform.position).normalized;

      //tingkatkan kecepatan dan aktifkan trail
      enemyPathfinding.moveSpeed *= dashSpeedMultiplier;
      dashTrail.emitting = true;

      //gerakan musuh ke arah dash
      enemyPathfinding.MoveTo(dashDirection);

      yield return new WaitForSeconds(dashDuration);

      //reset kecepatan dash dan trail
        enemyPathfinding.moveSpeed = originalMoveSpeed;
        dashTrail.emitting = false;
        enemyPathfinding.StopMoving();

        // Cooldown sebelum bisa dash lagi
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
        canAttack = true;
   }

   void OnCollisionEnter2D(Collision2D other)
   {
      if (isDashing && other.gameObject.CompareTag("DestructibleWall"))
      {
         Destroy(other.gameObject);
      }
   }
}