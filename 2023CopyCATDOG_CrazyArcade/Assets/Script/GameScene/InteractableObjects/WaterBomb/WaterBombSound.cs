using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterBombSound: MonoBehaviour
{

	public AudioSource boomsound;
	public AudioClip[] boomclip;

	private float timer = 0;

	public void SoundPlay(AudioClip clip)
	{
		boomsound.clip = clip;
		boomsound.loop = false;
		boomsound.volume = 0.1f;
		boomsound.Play();
	}


	void Start()
	{
		SoundPlay(boomclip[0]);
	}

	void Update()
	{
		timer += Time.deltaTime;
		if (timer > 1)
		{
			Destroy(gameObject);
		}
	}
}