using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject waterbomb_prefab;                                 //물풍선 프리팹
    public void GenerateWaterBomb(Vector2 position, int bomb_range)     //물풍선 생성 함수
    {
        GameObject bomb = Instantiate<GameObject>(waterbomb_prefab, position, Quaternion.identity);
        bomb.GetComponent<WaterBomb>().bomb_range = bomb_range;
    }

    //변수들
    public enum State
    {
        Playing=0, Imprisoned=1, Destroying=2, Immune=3, Turtle=4, Pirate=5, Standby=6, Endgame=7
    }

    public State player_state = State.Standby; //state

    public int ballon_range = 1;
    public int range_item = 0;

    public int water_bomb_max = 1;
    public int balloon_item = 0;

    public const float basic_speed = 5;
    public int speed_item = 0;
    public float speed = basic_speed;

    public const float speed_increase = 1;

    public float ballon_timer = 0;
    public int needle = 1;

    public bool ballon_touched =false;

    public float bush_timer = 0;

    public Vector2 pposition;
    public Vector2Int pindex;
    public TileInfo cur_tile_info = null;

    // Update is called once per frame
    /*void FixedUpdate()
    {
        if (player_state == 0)
        {
            speed = basic_speed + speed_item * speed_increase;
            float horizontal = Input.GetAxisRaw("Horizontal"); //Axis계열로 받아오면 후입력 우선 불가
            float vertical = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.UP1]))
            {
                Vector2 move = new Vector2(0, 1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN1]))
            {
                Vector2 move = new Vector2(0, -1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.RIGHT1]))
            {
                Vector2 move = new Vector2(1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.LEFT1]))
            {
                Vector2 move = new Vector2(-1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }

            pposition= MapManager.instance.GetClosestCellPosition(transform.position);
            cur_tile_info = MapManager.instance.GetClosestTileInfo(transform.position);

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL1]))
            {
                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                    GenerateWaterBomb(pposition, ballon_range + range_item);//물풍선 오브젝트 생성<-ppostion+object 생성 명령어
                else {
                    //물폭탄이 필드에 최대 갯수만큼 설치되어 있을 경우의 코드
                }
            }

            if (cur_tile_info.GetState(TileInfo.State.water_ray))
            {
               player_state = 1;
                Debug.Log("Hit");
            }
            if(cur_tile_info.HaveItem())
            {
                cur_tile_info.UseItem(this);
            }
        }
        else if (player_state == 1)//물풍선 감옥
        {

            ballon_timer += Time.deltaTime;

            GetComponent<SpriteRenderer>().color = Color.blue;

            if (ballon_touched==true) 
            {
                player_state = 2;
            }
            if (ballon_timer > 3)
            {
                player_state = 2;
            }
        }
        else if (player_state == 2)//사망
        {
            Destroy(gameObject);
        }
    }*/

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
    public void Bush()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        Debug.Log("bush");
        color.a = 0.0f;
        GetComponent<SpriteRenderer>().color = color;
    }
}
