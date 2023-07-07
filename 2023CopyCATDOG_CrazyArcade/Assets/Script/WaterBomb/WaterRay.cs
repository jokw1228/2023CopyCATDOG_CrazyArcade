using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterRay : MonoBehaviour
{
    public enum Direction
    {
        up = 0, right = 1, down = 2, left = 3
    }

    public Sprite sprite;

    public double life_span;
    double timer = 0.0;

    Vector2Int cell_index;

    private void Start()
    {
        cell_index = MapManager.instance.GetClosestCellIndex(new Vector2(transform.position.x, transform.position.y));

        MapManager.instance.tile_infos[cell_index.x, cell_index.y].AddState(TileInfo.State.water_ray);
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
    public void Gernerate(Vector3 pos, Direction d, float life_span)
    {
        gameObject.GetComponent<WaterRay>().life_span = life_span;
        GameObject water_ray = Instantiate<GameObject>(gameObject, pos, Quaternion.Euler(0,0,-90 * (int)d));
        water_ray.GetComponent<SpriteRenderer>().sprite = sprite; 
    }
}
