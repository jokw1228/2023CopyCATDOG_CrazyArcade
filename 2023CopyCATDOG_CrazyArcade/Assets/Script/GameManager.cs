using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public Image Panel;
    float time = 0f;
    float f_time = 1f;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    
    public void LoadMenuScene()
    {
        StartCoroutine("Fade",0);
    }

    public void LoadGameScene()
    {
        StartCoroutine("Fade",1);
    }

    public void LoadRuleScene()
    {
        StartCoroutine("Fade",2);  
    }


    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator Fade(int n)
    {
        time = 0f;
        Color alpha = Panel.color;
        yield return new WaitForSeconds(1f);
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        switch (n)
        {
            case 0:
                SceneManager.LoadScene("MenuScene");
                break;
            case 1:
                SceneManager.LoadScene("GameScene");
                break;
            case 2:
                SceneManager.LoadScene("RuleScene");
                break;
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
    }
    IEnumerator FadeOut()
    {
        time = 0f;
        Color alpha = Panel.color;
        yield return new WaitForSeconds(1f);
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
    }



}
