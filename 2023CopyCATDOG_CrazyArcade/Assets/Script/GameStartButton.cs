using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : SelectButton
{
    protected override void ButtonAction()
    {
        base.ButtonAction();
        GameManager.Inst.LoadGameScene();
    }
}
