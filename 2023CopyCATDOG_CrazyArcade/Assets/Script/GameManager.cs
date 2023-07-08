using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public Image Panel;
    float time = 0f;
    float f_time = 0.4f;
    bool CheckCoroutine=false;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    
    public void LoadMenuScene()
    {
        if(!CheckCoroutine)
            StartCoroutine("Fade",0);
    }

    public void LoadGameScene()
    {
        if(!CheckCoroutine)
            StartCoroutine("Fade",1);
    }

    public void LoadRuleScene()
    {
        if(!CheckCoroutine)
            StartCoroutine("Fade",2);  
    }


    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator Fade(int n)
    {
        CheckCoroutine = true;
        time = 0f;
        Color alpha = Panel.color;
        Panel.gameObject.SetActive(true);
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
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        CheckCoroutine = false;
    }
}
