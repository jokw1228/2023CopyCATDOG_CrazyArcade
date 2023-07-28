using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    public AudioSource sound;
    public AudioClip[] soundclip;

    public void SoundPlay(AudioClip clip)
    {
        sound.clip = clip;
        sound.loop = false;
        sound.volume = 0.1f;
        sound.Play();
    }

    public void SoundPause(AudioClip clip)
    {
        sound.clip = clip;
        sound.Pause();
    }


    void Update()
    {
        if (Player.player1.player_state == Player.State.Standby)
        {
            SoundPlay(soundclip[0]);
        }
        if ((Player.player1.player_state == Player.State.Imprisoned) || (Player.player2.player_state == Player.State.Imprisoned))
        {
            SoundPlay(soundclip[1]);
        }
        if ((Player.player1.player_state == Player.State.Immune) || (Player.player2.player_state == Player.State.Immune))
        {
            SoundPause(soundclip[1]);
        }
    }
}