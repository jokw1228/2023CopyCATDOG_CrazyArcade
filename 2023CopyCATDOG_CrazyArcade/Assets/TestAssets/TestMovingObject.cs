using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovingObject : MonoBehaviour
{
    Rigidbody2D rigid_body_2D;
    // Start is called before the first frame update
    void Start()
    {
        rigid_body_2D = gameObject.GetComponent<Rigidbody2D>();
        rigid_body_2D.AddForce(new Vector2(0, 1));
        GetComponent<WaterBombGenerator>().Generate(new Vector2(0.5f, 0.5f), 3);
    }
}
