using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : IEnemyState
{
    private Enemy enemy;
    private float huntTimer;
    private float huntDuration;

    public void Enter(Enemy enemy)
    {
        if(enemy.movementSpeed <= 6)
        {
            enemy.movementSpeed += 0.05f;

        }
        this.enemy = enemy;
    }

    public void Execute()
    {
        Hunt();

        enemy.Move();

        if (enemy.Target != null && enemy.InShootRange || enemy.Target != null && enemy.InMeleeRange)
        {
            enemy.SetState();
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            enemy.Target = inGamePlayer.Instance.gameObject;
        }
    }

    private void Hunt()
    {
        huntTimer += Time.deltaTime;

        if (huntTimer >= huntDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}
