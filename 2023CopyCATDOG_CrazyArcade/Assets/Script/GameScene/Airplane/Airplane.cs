using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

abstract public class Airplane : MonoBehaviour
{
    float dest_x;
    Vector3 direction;
    int drop_cnt;
    bool is_from_right;

    public float speed;

    private void Update()
    {
        if (is_from_right)
        {
            if (transform.position.x > dest_x)
            {
                transform.position += direction * Time.deltaTime * speed;
                if (drop_cnt > 0 && transform.position.x < 0 && MapManager.instance.GetClosestTileInfo(transform.position).is_empty)
                {
                    Drop();
                    drop_cnt--;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            if (transform.position.x < dest_x)
            {
                transform.position += direction * Time.deltaTime * speed;
                if (drop_cnt > 0 && transform.position.x > 0 && MapManager.instance.GetClosestTileInfo(transform.position).is_empty)
                {
                    Drop();
                    drop_cnt--;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetAirplane(bool is_from_right, int drop_count)
    {
        this.drop_cnt = drop_count;
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
    abstract protected void Drop();
}
