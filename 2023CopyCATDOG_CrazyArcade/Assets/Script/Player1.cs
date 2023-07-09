using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player1 : MonoBehaviour
{
    public GameObject waterbomb_prefab;
    public void Generate(Vector2 position, int bomb_range)
    {
        GameObject bomb = Instantiate<GameObject>(waterbomb_prefab, position, Quaternion.identity);
        bomb.GetComponent<WaterBomb>().bomb_range = bomb_range;
    }

    //state
    int player_state = 0;
    int ballon_range = 1;
    int balloon_item = 0;
    int range_item = 0;
    int speed_item = 0;
    float ballon_timer = 0;
    float speed = 5;
    bool ballon_touched =false;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 vector=new Vector2(0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player_state == 0)
        {
            speed = speed + speed_item * 1;
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
            //Vector2 pposition = MapManager.instance.GetClosestCellIndex(transform.position);
            Vector2 pposition = new Vector2(0, 0);

            if (Input.GetKey(KeySetting.keys[KeyAction.BALL1]))
            {
                Generate(pposition, ballon_range + balloon_item);
                //물풍선 오브젝트 생성<-ppostion+object 생성 명령어
            }

            if (MapManager.instance.tile_infos[0, 0].GetState(TileInfo.State.water_ray))
            {
               player_state = 1;
            }
        }
        else if (player_state == 1)//물풍선 감옥
        {

            ballon_timer += Time.deltaTime;
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
}
