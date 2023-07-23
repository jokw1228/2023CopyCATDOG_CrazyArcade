using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Airplane : MonoBehaviour
{
    float dest_x;
    Vector3 direction;
    int item_cnt;
    bool is_from_right;

    public float speed;

    private void Update()
    {
        if (is_from_right)
        {
            if (transform.position.x > dest_x)
            {
                transform.position += direction * Time.deltaTime * speed;
                if (item_cnt > 0 && transform.position.x < 0 && MapManager.instance.GetClosestTileInfo(transform.position).is_empty)
                {

                    MapManager.instance.item_generator.GenerateItem(ItemGenerator.random_item, MapManager.instance.GetClosestCellIndex(transform.position));
                    item_cnt--;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.position.x < dest_x)
            {
                transform.position += direction * Time.deltaTime * speed;
                if (item_cnt > 0 && transform.position.x > 0 && MapManager.instance.GetClosestTileInfo(transform.position).is_empty)
                {
                    MapManager.instance.item_generator.GenerateItem(ItemGenerator.random_item, MapManager.instance.GetClosestCellIndex(transform.position));
                    item_cnt--;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetAirplane(bool is_from_right, int item_count)
    {
        this.item_cnt = item_count;
        this.is_from_right = is_from_right;

        if (is_from_right)
        {
            dest_x = MapManager.instance.left_tile_x;
            direction = Vector3.left;
        }
        else
        {
            dest_x = MapManager.instance.right_tile_x;
            direction = Vector3.right;
        }
    }
}
