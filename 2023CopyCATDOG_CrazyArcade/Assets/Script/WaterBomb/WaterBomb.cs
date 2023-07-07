using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class WaterBomb : MonoBehaviour
{
    static int num_of_cur_water_bomb = 0;

    public float bomb_time;
    public int bomb_range;
    public float water_ray_spread_span;
    public float water_ray_life_span;
    public GameObject water_ray_prefab;

    Vector2Int cell_index;

    // Start is called before the first frame update
    void Start()
    {
        cell_index = MapManager.instance.GetClosestCellIndex(new Vector2(transform.position.x, transform.position.y));

        MapManager.instance.tile_infos[cell_index.x, cell_index.y].AddState(TileInfo.State.water_bomb);
        num_of_cur_water_bomb++;
        StartCoroutine(Bomb());
    }


    IEnumerator Bomb()
    {
        yield return new WaitForSeconds(bomb_time);
        gameObject.SetActive(false);

        for(int i = 1; i <= bomb_range; i++)
        {        
            water_ray_prefab.GetComponent<WaterRay>().Gernerate(transform.position + Vector3.up * MapManager.instance.tile_size * i, WaterRay.Direction.up, water_ray_life_span);
            water_ray_prefab.GetComponent<WaterRay>().Gernerate(transform.position + Vector3.right * MapManager.instance.tile_size * i, WaterRay.Direction.right, water_ray_life_span);
            water_ray_prefab.GetComponent<WaterRay>().Gernerate(transform.position + Vector3.down * MapManager.instance.tile_size * i, WaterRay.Direction.down, water_ray_life_span);
            water_ray_prefab.GetComponent<WaterRay>().Gernerate(transform.position + Vector3.left * MapManager.instance.tile_size * i, WaterRay.Direction.left, water_ray_life_span);
        }


        MapManager.instance.tile_infos[cell_index.x, cell_index.y].DelState(TileInfo.State.water_bomb);
        num_of_cur_water_bomb--;
        Destroy(gameObject);
        yield break;
    }
}
