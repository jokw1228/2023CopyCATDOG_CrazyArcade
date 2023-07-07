using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameRuleButton : SelectButton
{
    public Image Panel;
    float time = 0f;
    float f_time = 1f;
    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        GameManager.Inst.LoadRuleScene();
    }
    protected override void ButtonAction()
    {
        base.ButtonAction();
        StartCoroutine("FadeFlow");
    }
}
