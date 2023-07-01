using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float f_time = 1f;
    private void Awake()
    {
        StartCoroutine("FadeFlow");
    }
    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(0,1, time);
            Panel.color = alpha;
            yield return null;
        }
        time = 0f;
        yield return new WaitForSeconds(1f);
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        GameManager.Inst.LoadMenuScene();
    }
}
