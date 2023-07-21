using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : InteractableObject, IItem
{
    void IItem.OnGetItem(Player player)
    {
        player.Bush();
    }
    void IItem.Remove()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        cell_index = MapManager.instance.GetClosestCellIndex(new Vector2(transform.position.x, transform.position.y));
        state = TileInfo.State.item;

        MapManager.instance.GetTileInfo(cell_index).AddItem(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
