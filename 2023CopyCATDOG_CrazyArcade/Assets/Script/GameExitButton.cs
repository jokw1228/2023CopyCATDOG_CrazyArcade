using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameExitButton : SelectButton
{
    protected override void ButtonAction()
    {
        base.ButtonAction();
        GameManager.Inst.Exit();
    }
}
