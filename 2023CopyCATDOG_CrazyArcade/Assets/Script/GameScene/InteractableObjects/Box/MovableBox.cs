using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBox : Box
{
    Vector2Int next_cell_index;
    Rigidbody2D rigidbody2d;
    Vector2 cur_velocity = Vector2.zero;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        next_cell_index = MapManager.instance.GetClosestCellIndex(new Vector2(transform.position.x, transform.position.y));
        if(cell_index != next_cell_index)
        {
            MapManager.instance.GetTileInfo(cell_index).DelBox();
            MapManager.instance.GetTileInfo(next_cell_index).AddBox(this);
            cell_index = next_cell_index;
        }
    }
}
