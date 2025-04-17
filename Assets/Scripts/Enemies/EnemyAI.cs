using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] protected float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    protected bool canAttack = true;

    protected enum State { Roaming, Attacking }

    private Vector2 roamPosition;
    private float timeRoaming = 0f;
    protected State state;
    protected EnemyPathfinding enemyPathfinding;
    protected Transform playerTarget;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    protected virtual void Start()
    {
        roamPosition = GetRoamingPosition();    
        if (PlayerController.Instance != null)
    {
        playerTarget = PlayerController.Instance.transform;
    }
        //FindPlayer();
    }

    private void Update()
    {
        // Coba cari player jika belum ada
        //if (playerTarget == null)
        //{
        //    FindPlayer();
        //    return;
        //}

        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                Roaming();
                break;

            case State.Attacking:
                Attacking();
                break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        enemyPathfinding.MoveTo(roamPosition);

        if (playerTarget != null && Vector2.Distance(transform.position, playerTarget.position) < attackRange)
        {
            state = State.Attacking;
        }

        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    protected virtual void Attacking()
    {
        if (playerTarget == null)
        {
            state = State.Roaming;
            return;
        }

        if (Vector2.Distance(transform.position, playerTarget.position) > attackRange)
        {
            state = State.Roaming;
            return; // added return to exit early
        }

        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            if(enemyType != null && enemyType is IEnemy enemy)
            {
                enemy.Attack();
            }
            else
            {
                Debug.Log("enemyType not set or doesn't implement IEnemy!", this);
            }

            if (stopMovingWhileAttacking)
                enemyPathfinding.StopMoving();
            else
                enemyPathfinding.MoveTo(roamPosition);

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void FindPlayer()
    {
        if (PlayerController.Instance != null)
        {
            playerTarget = PlayerController.Instance.transform;
        }
    }

    private void SetPlayerTarget(Transform target)
    {
        playerTarget = target;
    }

}
