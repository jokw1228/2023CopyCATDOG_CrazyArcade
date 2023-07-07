using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : SelectButton
{
    public Image Panel;
    public GameObject Panel2;
    float time = 0f;
    float f_time = 1f;
    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        Panel2.gameObject.SetActive(false);
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
        GameManager.Inst.LoadMenuScene();
    }
    protected override void ButtonAction()
    {
        base.ButtonAction();
        StartCoroutine("FadeFlow");
    }
}
