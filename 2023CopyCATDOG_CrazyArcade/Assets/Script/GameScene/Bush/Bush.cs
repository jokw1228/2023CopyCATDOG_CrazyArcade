using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bush: MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<SpriteRenderer>().enabled = false; // -= new Color(0,0,0,1);
        foreach( SpriteRenderer spr in collision.gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if(spr.name != "Shadow")
                spr.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<SpriteRenderer>().enabled = true; //+= new Color(0, 0, 0, 1);
        foreach (SpriteRenderer spr in collision.gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if (spr.name != "Shadow")
                spr.enabled = true;
        }
    }
}
