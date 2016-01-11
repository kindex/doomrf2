using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    internal Unit owner;
    internal ItemKind bulletKind;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform != owner.transform && other.gameObject.layer != LayerMask.NameToLayer("Platform"))
        {
            var target = other.GetComponent<Unit>();
            if (target != null)
            {
                target.Hit(owner, bulletKind);
            }

            Die();
        }
    }

    void Die()
    {
        if (bulletKind == ItemKind.Rocket)
        {
            Instantiate(GamePrefabs.instance.rocketExplosion, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
