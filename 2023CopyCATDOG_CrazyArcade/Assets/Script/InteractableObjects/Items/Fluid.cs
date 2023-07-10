using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluid : InteractableObject, IItem
{
    void IItem.OnGetItem(Player1 player)
    {
        player.IncreaseWaterBombRange();
    }
    void IItem.Remove()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        cell_index = MapManager.instance.GetClosestCellIndex(new Vector2(transform.position.x, transform.position.y));
        state = TileInfo.State.fluid;

        MapManager.instance.GetTileInfo(cell_index).AddItem(state, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}