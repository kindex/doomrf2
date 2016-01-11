using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour
{
    Unit unit;
    internal HashSet<Unit> enemies = new HashSet<Unit>();
    internal Unit target;
    bool seeTarget;

    void Awake()
    {
        unit = GetComponent<Unit>();
        StartCoroutine(RunAI());
    }

    void Start()
    {
        foreach (var player in The.players)
        {
            enemies.Add(player);
        }
        StartCoroutine(RunAI());
    }

    IEnumerator RunAI()
    {
        while (unit.alive)
        {
            SetTarget();

            if (target != null)
            {
                bool toRight = target.transform.position.x > unit.transform.position.x;

                if (seeTarget && Mathf.Abs(target.transform.position.y - unit.transform.position.y) < 1)
                {
                    unit.Fire();
                    if (unit.right != toRight)
                    {
                        if (toRight)
                        {
                            unit.MoveRight();
                        }
                        else
                        {
                            unit.MoveLeft();
                        }
                    }
                    else
                    {
                        unit.StopMove();
                    }
                }
                else if (target.transform.position.x + 1 < unit.transform.position.x)
                {
                    unit.MoveLeft();
                }
                else if (target.transform.position.x - 1 > unit.transform.position.x)
                {
                    unit.MoveRight();
                }
                else
                {
                    unit.StopMove();
                }
            }
            yield return null;
        }
    }

    void SetTarget()
    {
        foreach (var enemy in enemies)
        {
            Vector2 d = enemy.transform.position - unit.transform.position;
            bool see = true;
            foreach (var hit in Physics2D.RaycastAll(unit.transform.position, d.normalized, d.magnitude))
            {
                if (hit.transform == unit.transform)
                {
                    continue;
                }
                if (hit.transform != enemy.transform)
                {
                    see = false;
                }
            }
            seeTarget = see;
            if (see)
            {
                target = enemy;
            }
        }
    }

    public void OnHit(Unit attacker)
    {
        target = attacker;
        enemies.Add(attacker);
    }
}

