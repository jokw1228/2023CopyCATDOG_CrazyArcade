using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResumeButton : SelectButton
{   
    protected override void ButtonAction()
    {
        base.ButtonAction();
        GetComponent<SpriteRenderer>().sprite = sprUnClicked;
        GameManager.Inst.Resume();
    }
}
