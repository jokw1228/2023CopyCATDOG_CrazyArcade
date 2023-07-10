using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : InteractableObject, IItem
{
    const float speed_increase = 1.0f;

    void IItem.OnGetItem(Player1 player)
    {
        player.SpeedUp();
    }
    void IItem.Remove()
    {
        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        cell_index = MapManager.instance.GetClosestCellIndex(new Vector2(transform.position.x, transform.position.y));
        state = TileInfo.State.roller;

        MapManager.instance.GetTileInfo(cell_index).AddItem(state, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
