using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
    public Transform follow;

	void LateUpdate()
	{
	    transform.position = follow.transform.position;
	}
}
