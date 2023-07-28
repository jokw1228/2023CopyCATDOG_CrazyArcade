using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovableBox : Box
{
    Vector2Int next_cell_index;
    Rigidbody2D rigidbody2d;
    Vector2 cur_velocity = Vector2.zero;

    const float moving_tirigger_distance = 0.19f;
    Vector2 displacement = Vector2.zero;

    const float moving_speed = 2.5f;

    bool is_moving = false;


    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!is_moving)
        {
            next_cell_index = MapManager.instance.GetClosestCellIndex(new Vector2(transform.position.x, transform.position.y));

            if (cell_index != next_cell_index)
            {
                MapManager.instance.GetTileInfo(cell_index).DelBox();
                MapManager.instance.GetTileInfo(next_cell_index).AddBox(this);
                cell_index = next_cell_index;
            }

            displacement = rigidbody2d.position - (Vector2)MapManager.instance.GetCellPosition(cell_index);
            if (displacement.x > moving_tirigger_distance) { MoveTo(cell_index + Vector2Int.right); }
            else if(displacement.x < -moving_tirigger_distance) { MoveTo(cell_index + Vector2Int.left); }
            else if (displacement.y > moving_tirigger_distance) { MoveTo(cell_index + Vector2Int.up); }
            else if (displacement.y < -moving_tirigger_distance) { MoveTo(cell_index + Vector2Int.down); }
            else //if(displacement.magnitude > 0.05f)
            {
                ResetPosition();
            }
        }

    }
    void MoveTo(Vector2Int target_cell_index)
    {
        if (!MapManager.instance.CheckTileInfo(target_cell_index))
        {
            ResetPosition();
            return;
        }
        if (MapManager.instance.GetTileInfo(target_cell_index).CheckState((TileInfo.State)((int)TileInfo.State.player + (int)TileInfo.State.wall + (int)TileInfo.State.box + (int)TileInfo.State.water_bomb)))
        {
            ResetPosition();
            return;
        }
        if(!is_moving)
        {
            is_moving = true;
            rigidbody2d.bodyType = RigidbodyType2D.Static;
            StartCoroutine(Moving(MapManager.instance.GetCellPosition(target_cell_index)));
        }
        //rigidbody2d. MovePosition(MapManager.instance.GetCellPosition(target_cell_index));
    }

    IEnumerator Moving(Vector3 target_position)
    {
        MapManager.instance.GetTileInfo(cell_index).DelBox();

        Vector3 step = target_position - transform.position;
        while (step.magnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target_position, moving_speed * Time.deltaTime);
            step = target_position - transform.position;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        transform.position = target_position;

        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;

        cell_index = MapManager.instance.GetClosestCellIndex(new Vector2(transform.position.x, transform.position.y));
        MapManager.instance.GetTileInfo(cell_index).AddBox(this);


        is_moving = false;
        yield break;
    }
    private void ResetPosition()
    {
        transform.position = MapManager.instance.GetCellPosition(cell_index);
        rigidbody2d.velocity = Vector3.zero;
    }
}
