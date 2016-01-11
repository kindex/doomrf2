using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public enum ItemKind
{
    None = 0,
    Pistol = 1,
    SMG = 2,
    Shotgun = 3,
    RocketLauncher = 4,
    Rocket,
    mm9,
    mm12
}

public class Unit : MonoBehaviour
{
    public ItemKind weapon;
    public Transform barrelLeft;
    public Transform barrelRight;
    public int hp = 100;

    new Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    GroundTester groundTester;
    internal bool right;
    float speed;
    float allowedShootTime;
    internal bool alive = true;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        groundTester = GetComponentInChildren<GroundTester>();
    }

    public void MoveLeft()
    {
        speed = -1;
        right = false;
        animator.SetBool("Walk", true);
        SetSide();
    }

    public void MoveRight()
    {
        speed = +1;
        right = true;
        animator.SetBool("Walk", true);
        SetSide();
    }

    public void StopMove()
    {
        speed = 0;
        animator.SetBool("Walk", false);
    }

    void SetSide()
    {
        spriteRenderer.flipX = right;
    }

    public void Jump()
    {
        if (groundTester.hits > 0)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, +2);
        }
    }

    public void Update()
    {
        rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
    }

    public void Fire()
    {
        if (Time.time > allowedShootTime)
        {
            var f = 0.05f;
            switch (weapon)
            {
                case ItemKind.Pistol:
                    CreateBullet(new Vector2(2 + Random.Range(-f, +f), Random.Range(-f, +f)), GamePrefabs.instance.bullet12mm, ItemKind.mm9, 1f);
                    break;
                case ItemKind.SMG:
                    CreateBullet(new Vector2(2 + Random.Range(-f, +f), Random.Range(-f, +f)), GamePrefabs.instance.bullet12mm, ItemKind.mm9, 0.1f);
                    break;
                case ItemKind.Shotgun:
                    for (int i = 0; i < 8; i++)
                    {
                        CreateBullet(new Vector2(2 + Random.Range(-0.4f, +0.4f), Random.Range(-0.4f, +0.4f)), GamePrefabs.instance.bullet12mm, ItemKind.mm12, 1);
                    }
                    break;
                case ItemKind.RocketLauncher:
                    CreateBullet(new Vector2(2, 0), GamePrefabs.instance.rocket, ItemKind.Rocket, 1);
                    break;
            }
        }
    }

    void CreateBullet(Vector2 speed, GameObject prefab, ItemKind bulletKind, float delay)
    {
        var go = Instantiate(prefab);

        go.transform.position = right ? barrelRight.position : barrelLeft.position;

        go.GetComponent<Rigidbody2D>().velocity = new Vector2(right ? speed.x : -speed.x + rigidbody.velocity.x, speed.y);
        go.GetComponent<SpriteRenderer>().flipX = !right;
        go.GetComponent<Bullet>().owner = this;
        go.GetComponent<Bullet>().bulletKind = bulletKind;


        allowedShootTime = Time.time + delay;
    }

    public void Hit(Unit owner, ItemKind bulletKind)
    {
        switch (bulletKind)
        {
            case ItemKind.Rocket:
                Damage(100);
                break;
            case ItemKind.mm9:
                Damage(5);
                break;
            case ItemKind.mm12:
                Damage(4);
                break;
        }
    }

    void Damage(int damage)
    {
        hp = hp - damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        alive = false;
        animator.SetBool("Death", true);
        gameObject.layer = LayerMask.NameToLayer("Dead");
        StopMove();
    }
}
