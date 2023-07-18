using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Box : InteractableObject
{
    //public Item item;
    void Start()
    {
        cell_index = MapManager.instance.GetClosestCellIndex(new Vector2(transform.position.x, transform.position.y));
        state = TileInfo.State.box;

        MapManager.instance.GetTileInfo(cell_index).AddBox(this);
    }

    public void Remove()
    {
        MapManager.instance.item_generator.GenerateItem(ItemGenerator.random_item, cell_index);
        MapManager.instance.GetTileInfo(cell_index).DelBox();
        Destroy(gameObject);
    }
}
