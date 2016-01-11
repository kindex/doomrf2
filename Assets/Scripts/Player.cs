using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text hp;
    Unit unit;

    void Awake()
    {
        unit = GetComponent<Unit>();
        The.players.Add(unit);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            unit.Jump();
        }

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetMouseButton(0))
        {
            unit.Fire();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            unit.MoveLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            unit.MoveRight();
        }
        else
        {
            unit.StopMove();
        }


        if (hp != null)
        {
            hp.text = string.Format("{0}", unit.hp);
        }
    }
}
