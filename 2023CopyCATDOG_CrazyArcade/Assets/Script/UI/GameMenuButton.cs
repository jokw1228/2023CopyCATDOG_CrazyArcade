using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuButton : SelectButton
{
    protected override void ButtonAction()
    {
        base.ButtonAction();
        GetComponent<SpriteRenderer>().sprite = sprUnClicked;
        GameManager.Inst.Resume();
        GameManager.Inst.LoadMenuScene();
    }
}
