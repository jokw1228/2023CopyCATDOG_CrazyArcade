using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public Sprite sprUnClicked;
    public Sprite sprClicked;

    private void OnMouseUpAsButton()
    {
        ButtonAction();
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sprite = sprClicked;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().sprite = sprUnClicked;
    }

    protected virtual void ButtonAction()
    {
        StartCoroutine("FadeFlow");
        //오버라이딩 해서 쓰세요.
    }
}
