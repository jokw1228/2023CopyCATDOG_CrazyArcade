using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<SpriteRenderer>().enabled = false; // -= new Color(0,0,0,1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<SpriteRenderer>().enabled = true; //+= new Color(0, 0, 0, 1);
    }
}
