using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    protected MapManager.Direction direction = MapManager.Direction.down;
    public GameObject waterbomb_prefab;                                 //¹°Ç³¼± ÇÁ¸®ÆÕ
    protected void GenerateWaterBomb(Vector2 position, int bomb_range)     //¹°Ç³¼± »ý¼º ÇÔ¼ö
    {
        GameObject bomb = Instantiate<GameObject>(waterbomb_prefab, position, Quaternion.identity);
        bomb.GetComponent<WaterBomb>().bomb_range = bomb_range;
    }
    public GameObject wind;
    protected void GenerateWind(MapManager.Direction direction)
    {
        Debug.Log("use wind");
        GameObject wind_instance;
        wind_instance = Instantiate<GameObject>(wind, MapManager.instance.GetCellPosition(cell_index), Quaternion.identity);
        wind_instance.GetComponent<Wind>().direction = direction;
    }
    public GameObject Laser;
    protected  void GenerateLaser(Vector2 position, MapManager.Direction direction)
    {

    }
    protected void Dash(MapManager.Direction direction)
    {
        Debug.Log("use dash");
        //GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + MapManager.GetVector2(direction) * 2);
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

    protected enum ActiveItem { none, needle, wind, laser ,dash}
    protected ActiveItem active_item_slot = ActiveItem.none;

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
    public void Turtle()
    {
        player_state = State.Turtle;
    }
    public void Pirate()
    {
        player_state = State.Pirate;
    }
    public void GainNeedle()
    {
        active_item_slot = ActiveItem.needle;
    }
    public void GainWind()
    {
        active_item_slot = ActiveItem.wind;
    }
    public void GainrLaser()
    {
        active_item_slot = ActiveItem.laser;
    }
    public void GainDash()
    {
        active_item_slot = ActiveItem.dash;
    }
}
