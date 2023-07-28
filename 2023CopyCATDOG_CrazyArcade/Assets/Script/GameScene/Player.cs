using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject waterbomb_prefab;                                 //¹°Ç³¼± ÇÁ¸®ÆÕ
    public WaterBomb GenerateWaterBomb(Vector2 position, int bomb_range)     //¹°Ç³¼± »ý¼º ÇÔ¼ö
    {
        WaterBomb water_bomb = Instantiate<GameObject>(waterbomb_prefab, position, Quaternion.identity).GetComponent<WaterBomb>();
        
        water_bomb.bomb_range = bomb_range;

        return water_bomb;
    }

    //º¯¼öµé
    public enum State
    {
        Standby=0, Imprisoned=1, Destroying=2, Immune=3, Turtle=4, Pirate=5, Playing=6, Endgame=7
    }

    public State player_state = State.Standby; //state

    public static Player player1;
    public static Player player2;

    public int ballon_range = 1;
    public int range_item = 0;

    public int water_bomb_max = 1;
    public int balloon_item = 0;

    public const float basic_speed = 5;
    public int speed_item = 0;
    public float speed = basic_speed;

    public const float speed_increase = 0.5f;

    public float ballon_timer = 0;
    public float Standby_timer = 0;
    public float death_timer = 0;

    public int needle = 1;//enum ActiveItem { none, needle, wind, raser}

    public bool ballon_touched =false;

    //public float bush_timer = 0;

    public Vector2 pposition;
    public Vector2Int pindex;
    public TileInfo cur_tile_info = null;

    Vector2Int cell_index;
    protected virtual void Start()
    {
        cell_index = MapManager.instance.GetClosestCellIndex(transform.position);
        MapManager.instance.GetTileInfo(cell_index).AddState(TileInfo.State.player);
    }
    protected virtual void Update()
    {
        if(cell_index != MapManager.instance.GetClosestCellIndex(transform.position))
        {
            MapManager.instance.GetTileInfo(cell_index).DelState(TileInfo.State.player);
            cell_index = MapManager.instance.GetClosestCellIndex(transform.position);
            MapManager.instance.GetTileInfo(cell_index).AddState(TileInfo.State.player);
        }
    }

    public void IncreaseWaterBombMax()
    {
        balloon_item++;
    }
    public void IncreaseWaterBombRange()
    {
        range_item++;
    }
    public void SpeedUp()
    {
        speed_item++;
    }
    public void NeedleIncrease()
    {
        needle++;  
    }
    public void Turtle()
    {
        player_state = State.Turtle;
    }
    public void Pirate()
    {
        player_state = State.Pirate;
    }
}
