using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterRay : InteractableObject
{
    public enum Direction
    {
         up = 0, right = 1, down = 2, left = 3
    }

    public AudioSource sound;
    public AudioClip[] audiolists;

    public void SoundPlay(AudioClip clip)
    {
        sound.clip = clip;
        sound.loop = false;
        sound.volume = 0.5f;
        sound.Play();
    }

    public double life_span;
    double timer = 0.0;

    private void Start()
    {
        cell_index = MapManager.instance.GetClosestCellIndex(new Vector2(transform.position.x, transform.position.y));
        state = TileInfo.State.water_ray;

        SoundPlay(audiolists[0]);

        MapManager.instance.GetTileInfo(cell_index).AddState(state);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > life_span)
        {
            MapManager.instance.tile_infos[cell_index.x, cell_index.y].DelState(TileInfo.State.water_ray);
            Destroy(gameObject);
        }
    }
    public void Gernerate(Vector3 pos)
    {
        GameObject water_ray = Instantiate<GameObject>(gameObject, pos, Quaternion.identity);
    }
}
