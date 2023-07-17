using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterButton : SelectButton
{
    protected override void ButtonAction()
    {
        base.ButtonAction();
        GameManager.Inst.LoadCharacterScene();
    }

}
