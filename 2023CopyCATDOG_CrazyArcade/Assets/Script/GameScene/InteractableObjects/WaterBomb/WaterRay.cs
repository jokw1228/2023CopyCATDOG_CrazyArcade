using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterRay : InteractableObject
{
    public double life_span;
    double timer = 0.0;

    private void Start()
    {
        cell_index = MapManager.instance.GetClosestCellIndex(new Vector2(transform.position.x, transform.position.y));
        state = TileInfo.State.water_ray;

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
