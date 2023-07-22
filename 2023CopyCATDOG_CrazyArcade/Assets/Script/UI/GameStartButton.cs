using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : SelectButton
{
    protected override void ButtonAction()
    {
        base.ButtonAction();
        switch (GameManager.Inst.currentMap.a)
        {
            case 0:
                GameManager.Inst.LoadGameScene1();
                break;
            case 1:
                GameManager.Inst.LoadGameScene2();
                break;
            case 2:
                GameManager.Inst.LoadGameScene3();
                break;
        }
    }

}
