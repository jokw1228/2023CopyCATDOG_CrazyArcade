using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuleButton : SelectButton
{
    protected override void ButtonAction()
    {
        base.ButtonAction();
        GameManager.Inst.LoadRuleScene();
    }
}
