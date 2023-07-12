using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeySetting.keys[KeyAction.UP1]))
        {
            Debug.Log("Up1");
        }
        else if (Input.GetKey(KeySetting.keys[KeyAction.DOWN1]))
        {
            Debug.Log("Down1");
        }
        else if (Input.GetKey(KeySetting.keys[KeyAction.LEFT1]))
        {
            Debug.Log("Left1");
        }
        else if (Input.GetKey(KeySetting.keys[KeyAction.RIGHT1]))
        {
            Debug.Log("Right1");
        }
        else if (Input.GetKey(KeySetting.keys[KeyAction.ITEM1]))
        {
            Debug.Log("Item1");
        }
        else if (Input.GetKey(KeySetting.keys[KeyAction.BALL1]))
        {
            Debug.Log("Ball1");
        }

        if (Input.GetKey(KeySetting.keys[KeyAction.UP2]))
        {
            Debug.Log("Up2");
        }
        else if (Input.GetKey(KeySetting.keys[KeyAction.DOWN2]))
        {
            Debug.Log("Down2");
        }
        else if (Input.GetKey(KeySetting.keys[KeyAction.LEFT2]))
        {
            Debug.Log("Left2");
        }
        else if (Input.GetKey(KeySetting.keys[KeyAction.RIGHT2]))
        {
            Debug.Log("Right2");
        }
        else if (Input.GetKey(KeySetting.keys[KeyAction.ITEM2]))
        {
            Debug.Log("Item2");
        }
        else if (Input.GetKey(KeySetting.keys[KeyAction.BALL2]))
        {
            Debug.Log("Ball2");
        }
    }
}
