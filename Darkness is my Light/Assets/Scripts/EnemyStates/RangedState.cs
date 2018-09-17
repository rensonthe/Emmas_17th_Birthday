using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private Enemy enemy;

    private float throwCooldown = 1;
    private bool canThrow = true;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        ShootBullet();

        if (enemy.Target != null)
        {
            if (!enemy.InShootRange)
            {
                enemy.Move();
            }
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void ShootBullet()
    {
        if(enemy.RangedTimer >= throwCooldown)
        {
            canThrow = true;
            enemy.RangedTimer = 0;
        }

        if (canThrow)
        {
            canThrow = false;
            enemy.MyAnimator.SetTrigger("shoot");
        }
    }
}
