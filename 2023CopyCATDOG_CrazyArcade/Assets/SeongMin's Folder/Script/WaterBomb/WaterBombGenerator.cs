using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBombGenerator : MonoBehaviour
{
    public GameObject waterbomb_prefab;

    public void Generate(Vector2 position, int bomb_range)
    {
        GameObject bomb = Instantiate<GameObject>(waterbomb_prefab, position, Quaternion.identity);
        bomb.GetComponent<WaterBomb>().bomb_range = bomb_range;        
    }
}
