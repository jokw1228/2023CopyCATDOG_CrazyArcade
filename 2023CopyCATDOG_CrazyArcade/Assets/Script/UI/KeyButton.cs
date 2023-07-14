using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyButton : SelectButton
{
    // Start is called before the first frame update
    public int num;
    protected override void ButtonAction()
    {
        base.ButtonAction();
        KeyManager.Inst.ChangeKey(num);
    }
}
