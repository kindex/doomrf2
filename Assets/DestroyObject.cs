using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour
{
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
