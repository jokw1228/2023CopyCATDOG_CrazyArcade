using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class WaterBomb : InteractableObject
{
    public static int all_water_bomb_cnt = 0;
    public static int player1_water_bomb_cnt = 0;
    public static int player2_water_bomb_cnt = 0;

    void IncreaseWaterBombCnt()
    {
        switch (generate_by) 
        {
            case GenerateBy.player1:
                player1_water_bomb_cnt++;
                break;
            case GenerateBy.player2:
                player2_water_bomb_cnt++;
                break;
        }
        all_water_bomb_cnt++;
    }
    void DecreaseWaterBombCnt()
    {
        switch (generate_by)
        {
            case GenerateBy.player1:
                player1_water_bomb_cnt--;
                break;
            case GenerateBy.player2:
                player2_water_bomb_cnt--;
                break;
        }
        all_water_bomb_cnt--;
    }

    public enum GenerateBy { other ,player1, player2 }
    public GenerateBy generate_by = GenerateBy.other;

    public float bomb_time;
    public int bomb_range;
    public float water_ray_spread_span;
    public float water_ray_life_span;
    
    public WaterRay water_ray_center;
    public WaterRay water_ray_up;
    public WaterRay water_ray_right;
    public WaterRay water_ray_down;
    public WaterRay water_ray_left;

    Dictionary<MapManager.Direction, WaterRay> water_rays = new();
    
    Collider2D collider2d;
    SpriteRenderer Sprite_renderer;

    Dictionary<MapManager.Direction, bool> isDirectionBlocked = new Dictionary<MapManager.Direction, bool>()
    {
        { MapManager.Direction.up, false },
        { MapManager.Direction.right, false },
        { MapManager.Direction.down, false },
        { MapManager.Direction.left, false },
    };

    bool is_bombed = false;

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
        IncreaseWaterBombCnt();

        water_rays.Add(MapManager.Direction.up, water_ray_up);
        water_rays.Add(MapManager.Direction.right, water_ray_right);
        water_rays.Add(MapManager.Direction.down, water_ray_down);
        water_rays.Add(MapManager.Direction.left, water_ray_left);

        foreach (WaterRay water_ray in water_rays.Values)
        {
            water_ray.life_span = water_ray_life_span;
        }

        Invoke("Bomb", bomb_time);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null) {
            collider2d.isTrigger = false;
        }
    }

    public GameObject waterbombsound; 
    public void Bomb()
    {
        if (!is_bombed)
        {
            GameObject BombSound = Instantiate<GameObject>(waterbombsound, new Vector3(0, 0, 0), Quaternion.identity);
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

            GenerateWaterRay(cell_index + Vector2Int.up * i, MapManager.Direction.up);
            GenerateWaterRay(cell_index + Vector2Int.right * i, MapManager.Direction.right);
            GenerateWaterRay(cell_index + Vector2Int.down * i, MapManager.Direction.down);
            GenerateWaterRay(cell_index + Vector2Int.left * i, MapManager.Direction.left);
        }

        DecreaseWaterBombCnt();
        Destroy(gameObject);
        yield break;
    }
    void GenerateWaterRay(Vector2Int target_cell_index, MapManager.Direction d)
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
