using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : SelectButton
{
    protected override void ButtonAction()
    {
        base.ButtonAction();
        GameManager.Inst.LoadMenuScene();
    }
}
