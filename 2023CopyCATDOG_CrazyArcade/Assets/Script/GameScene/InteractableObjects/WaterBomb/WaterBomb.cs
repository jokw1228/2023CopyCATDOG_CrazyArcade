using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class WaterBomb : InteractableObject
{
    public static int num_of_cur_water_bomb = 0;

    public float bomb_time;
    public int bomb_range;
    public float water_ray_spread_span;
    public float water_ray_life_span;
    
    public WaterRay water_ray_center;
    public WaterRay water_ray_up;
    public WaterRay water_ray_right;
    public WaterRay water_ray_down;
    public WaterRay water_ray_left;

    Dictionary<WaterRay.Direction, WaterRay> water_rays = new();
    
    Collider2D collider2d;
    SpriteRenderer Sprite_renderer;

    Dictionary<WaterRay.Direction, bool> isDirectionBlocked = new Dictionary<WaterRay.Direction, bool>()
    {
        { WaterRay.Direction.up, false },
        { WaterRay.Direction.right, false },
        { WaterRay.Direction.down, false },
        { WaterRay.Direction.left, false },
    };

    bool is_bombed = false;

    public AudioSource sound;
    public AudioClip[] audiolists;

    public void SoundPlay(AudioClip clip)
    {
        sound.clip = clip;
        sound.loop = false;
        sound.volume = 0.5f;
        sound.Play();
    }

    private void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        Sprite_renderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cell_index = MapManager.instance.GetClosestCellIndex(new Vector2(transform.position.x, transform.position.y));
        state = TileInfo.State.water_bomb;

        MapManager.instance.tile_infos[cell_index.x, cell_index.y].AddWaterBomb(this);
        num_of_cur_water_bomb++;

        water_rays.Add(WaterRay.Direction.up, water_ray_up);
        water_rays.Add(WaterRay.Direction.right, water_ray_right);
        water_rays.Add(WaterRay.Direction.down, water_ray_down);
        water_rays.Add(WaterRay.Direction.left, water_ray_left);

        foreach (WaterRay water_ray in water_rays.Values)
        {
            water_ray.life_span = water_ray_life_span;
        }

        Invoke("Bomb", bomb_time);
        SoundPlay(audiolists[0]);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null) {
            collider2d.isTrigger = false;
        }
    }

    public void Bomb()
    {
        if (!is_bombed)
        {
            StartCoroutine(BombCoroutine());
        }
    }

    IEnumerator BombCoroutine()
    {
        is_bombed = true;
        collider2d.enabled = false;
        Sprite_renderer.enabled = false;
        MapManager.instance.GetTileInfo(cell_index).DelWaterBomb();
        water_ray_center.Gernerate(MapManager.instance.GetCellPosition(cell_index));

        WaitForSeconds wait =  new WaitForSeconds(water_ray_spread_span);
        for (int i = 1; i <= bomb_range; i++)
        {        
            yield return wait;

            GenerateWaterRay(cell_index + Vector2Int.up * i, WaterRay.Direction.up);
            GenerateWaterRay(cell_index + Vector2Int.right * i, WaterRay.Direction.right);
            GenerateWaterRay(cell_index + Vector2Int.down * i, WaterRay.Direction.down);
            GenerateWaterRay(cell_index + Vector2Int.left * i, WaterRay.Direction.left);
        }

        num_of_cur_water_bomb--;
        Destroy(gameObject);
        yield break;
    }
    void GenerateWaterRay(Vector2Int target_cell_index, WaterRay.Direction d)
    {
        if (isDirectionBlocked[d])
        {
            return;
        }

        TileInfo tile_info = MapManager.instance.GetTileInfo(target_cell_index);
        if (tile_info.CheckState(TileInfo.State.wall))
        {
            isDirectionBlocked[d] = true;
        }
        else if(tile_info.CheckState(TileInfo.State.box))
        {
            isDirectionBlocked[d] = true;
            tile_info.get_box.Remove();
        }
        else if(tile_info.CheckState(TileInfo.State.water_bomb))
        {
            isDirectionBlocked[d] = true;
            tile_info.get_water_bomb.Bomb();   
        }
        else if (tile_info.CheckState(TileInfo.State.item))
        {
            tile_info.get_item.Remove();
            water_rays[d].Gernerate(MapManager.instance.GetCellPosition(target_cell_index));
        }
        else
        {
            water_rays[d].Gernerate(MapManager.instance.GetCellPosition(target_cell_index));
        }
    }

    
}
