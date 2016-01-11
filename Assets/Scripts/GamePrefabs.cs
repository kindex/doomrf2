using UnityEngine;
using System.Collections;

public class GamePrefabs : MonoBehaviour
{
    public static GamePrefabs instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject bullet9mm;
    public GameObject bullet12mm;
    public GameObject rocket;
    public GameObject rocketExplosion;
}
