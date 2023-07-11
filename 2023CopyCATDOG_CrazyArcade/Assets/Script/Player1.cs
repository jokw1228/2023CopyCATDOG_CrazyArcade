using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player1 : MonoBehaviour
{
    public GameObject waterbomb_prefab;
    public void GenerateWaterBomb(Vector2 position, int bomb_range)
    {
        GameObject bomb = Instantiate<GameObject>(waterbomb_prefab, position, Quaternion.identity);
        bomb.GetComponent<WaterBomb>().bomb_range = bomb_range;
    }

    //state
    int player_state = 0;

    int ballon_range = 1;
    int range_item = 0;

    int water_bomb_max = 1;
    int balloon_item = 0;

    const float basic_speed = 5;
    int speed_item = 0;
    float speed = basic_speed;
    
    const float speed_increase = 1;

    float ballon_timer = 0;

    bool ballon_touched =false;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 vector=new Vector2(0, 0);
    }


    Vector2 pposition;
    Vector2Int pindex;
    TileInfo cur_tile_info = null;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player_state == 0)
        {
            speed = basic_speed + speed_item * speed_increase;
            float horizontal = Input.GetAxisRaw("Horizontal"); //Axis계열로 받아오면 후입력 우선 불가
            float vertical = Input.GetAxisRaw("Vertical");
            if (horizontal != 0)
            {
                Vector2 move = new Vector2(horizontal, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }
            else if (vertical != 0)
            {
                Vector2 move = new Vector2(0, vertical);
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
}
