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

    public static bool GameIsPaused = false;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;

    public Character[] characters1;
    public Character[] characters2;
    public Map[] selectmap;

    public Character currentPlayer1Character;
    public Character currentPlayer2Character;
    public Map currentMap;

    public AudioSource Bgm;
    public AudioClip[] BgmList;
    private void Start()
    {
        if (characters1.Length > 0)
        {
            currentPlayer1Character = characters1[0];
        }
        if (characters2.Length > 0)
        {
            currentPlayer2Character = characters2[0];
        }
        if (selectmap.Length > 0)
        {
            currentMap = selectmap[0];
        }
    }
    public void BgmSoundPlay(AudioClip clip)
    {
        Bgm.clip = clip;
        Bgm.loop = true;
        Bgm.volume = 0.1f;
        Bgm.Play();
    }

    public void SetPlayer1Character(Character character)
    {
        currentPlayer1Character = character;
    }
    public void SetPlayer2Character(Character character)
    {
        currentPlayer2Character = character;
    }
    public void SetMap(Map map)
    {
        currentMap = map;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        PauseMenu.SetActive(true);
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        GameOverMenu.SetActive(true);
    }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    
    public void LoadMenuScene()
    {
        BgmSoundPlay(BgmList[0]);
        if (!CheckCoroutine)
            StartCoroutine("Fade",0);
    }

    public void LoadGameScene1()
    {
        BgmSoundPlay(BgmList[2]);
        if (!CheckCoroutine)
            StartCoroutine("Fade",1);
    }
    public void LoadGameScene2()
    {
        BgmSoundPlay(BgmList[2]);
        if (!CheckCoroutine)
            StartCoroutine("Fade", 2);
    }
    public void LoadGameScene3()
    {
        BgmSoundPlay(BgmList[2]);
        if (!CheckCoroutine)
            StartCoroutine("Fade", 3);
    }

    public void LoadRuleScene()
    {
        if(!CheckCoroutine)
            StartCoroutine("Fade",4);  
    }

    public void LoadCharacterScene()
    {
        BgmSoundPlay(BgmList[1]);
        if (!CheckCoroutine)
            StartCoroutine("Fade", 5);
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
                SceneManager.LoadScene("GameScene1");
                break;
            case 2:
                SceneManager.LoadScene("GameScene2");
                break;
            case 3:
                SceneManager.LoadScene("GameScene3");
                break;
            case 4:
                SceneManager.LoadScene("RuleScene");
                break;
            case 5:
                SceneManager.LoadScene("CharacterScene");
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
