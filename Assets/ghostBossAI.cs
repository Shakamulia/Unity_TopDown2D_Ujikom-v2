using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostBossAI : EnemyPathfinding
{
    public float retreatDuration = 2f;
    public float retreatSpeed = 3f;
    private bool isRetreating = false;
    private Vector2 retreatDirection;
    private float retreatTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRetreating)
        {
            transform.position += (Vector3)retreatDirection * retreatSpeed * Time.deltaTime;
            retreatTimer -= Time.deltaTime;
            if (retreatTimer <= 0)
            {
                isRetreating = false;
                // Start moving towards the target 
            }
        }
        else
        {
            //base.FixedUpdate();
        }
    }

    public void RetreatFromPlayer()
    {
        retreatDirection = (transform.position - PlayerController.Instance.transform.position).normalized;
        isRetreating = true;
        retreatTimer = retreatDuration;
    }
}
