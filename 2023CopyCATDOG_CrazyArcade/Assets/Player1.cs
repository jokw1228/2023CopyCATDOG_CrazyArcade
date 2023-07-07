using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2 vector=new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 5;
        float horizontal=Input.GetAxisRaw("Horizontal"); //Axis계열로 받아오면 후입력 우선 불가
        float vertical=Input.GetAxisRaw("Vertical"); 
        if(horizontal!=0){
            Vector2 move = new Vector2(horizontal, 0);
            transform.Translate(move * Time.deltaTime * speed);
        }
        else if(vertical!=0){
            Vector2 move = new Vector2(0, vertical);
            transform.Translate(move * Time.deltaTime * speed);
        }
    }
}
