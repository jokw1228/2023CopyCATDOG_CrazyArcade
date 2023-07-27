using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemAirplane : Airplane
{
    override protected void Drop()
    {
        MapManager.instance.item_generator.GenerateItem(ItemGenerator.random_item, MapManager.instance.GetClosestCellIndex(transform.position));
    }
}
