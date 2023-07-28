using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaterBombAirplane : Airplane
{
    WaterBombGenerator water_bomb_generator;
    private void Awake()
    {
        water_bomb_generator = GetComponent<WaterBombGenerator>();
    }
    override protected void Drop()
    {
        water_bomb_generator.Generate(MapManager.instance.GetClosestCellPosition(transform.position), 50);
    }
}
