using UnityEngine;
using System.Collections;

public class GroundTester : MonoBehaviour
{
    internal int hits;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsGround(other))
        {
            hits++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (IsGround(other))
        {
            hits--;
        }
    }

    bool IsGround(Collider2D other)
    {
        return other.transform != transform.parent && other.gameObject.layer != LayerMask.NameToLayer("Bullet");
    }
}
