using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Player
{

    // Start is called before the first frame update
    void Start()
    {
        Vector2 vector = new Vector2(0, 0);
    }

    bool UP1_key = false;
    bool DOWN1_key = false;
    bool RIGHT1_key = false;
    bool LEFT1_key = false;

    void Update()
    {
        if (player_state == State.Playing)          //플레이
        {
            speed = basic_speed + speed_item * speed_increase;

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.UP1]))       //UP1
            {
                UP1_key = true;
                DOWN1_key = false;
                RIGHT1_key = false;
                LEFT1_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.UP1])){
                UP1_key=false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN1]))       //DOWN1
            {
                UP1_key = false;
                DOWN1_key = true;
                RIGHT1_key = false;
                LEFT1_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.DOWN1])){
                DOWN1_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.RIGHT1]))       //RIGHT1
            {
                UP1_key = false;
                DOWN1_key = false;
                RIGHT1_key = true;
                LEFT1_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.RIGHT1])){
                RIGHT1_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.LEFT1]))      //LEFT1
            {
                UP1_key = false;
                DOWN1_key = false;
                RIGHT1_key = false;
                LEFT1_key = true;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.LEFT1])){
                LEFT1_key = false;
            }

            if (UP1_key)
            {
                Vector2 move = new Vector2(0, 1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (DOWN1_key)
            {
                Vector2 move = new Vector2(0, -1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (RIGHT1_key)
            {
                Vector2 move = new Vector2(1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (LEFT1_key)
            {
                Vector2 move = new Vector2(-1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }

            pposition = MapManager.instance.GetClosestCellPosition(transform.position); //player position -> pposition
            cur_tile_info = MapManager.instance.GetClosestTileInfo(transform.position); //current tile information

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL1]))                     // 물풍선 키 입력
            {

                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //목표 위치에 오브젝트(벽, 상자, 물폭탄, 물줄기, 아이템 등)가 없을 경우에만 물폭탄 설치, 해당 조건 확인 필요
                    if (MapManager.instance.GetClosestTileInfo(pposition).CheckState(TileInfo.State.none))
                        GenerateWaterBomb(pposition, ballon_range + range_item);//물풍선 오브젝트 생성<-ppostion+object 생성 명령어
                }
                else
                {
                                                                                        //물폭탄이 필드에 최대 갯수만큼 설치되어 있을 경우의 코드
                }
            }

            if (cur_tile_info.CheckState(TileInfo.State.water_ray)) 
            {
                player_state = State.Imprisoned;
                Debug.Log("Hit");
            }
            if (cur_tile_info.CheckState(TileInfo.State.item))
            {
                cur_tile_info.UseItem(this);
            }
        }

        else if (player_state == State.Imprisoned)  //물풍선 감옥
        {

            ballon_timer += Time.deltaTime;

            GetComponent<SpriteRenderer>().color = Color.blue;

            if ((Input.GetKeyDown(KeySetting.keys[KeyAction.ITEM1])) && needle >= 1) // case1: 탚출
            {
                needle -= 1;
                GetComponent<SpriteRenderer>().color = Color.white;
                player_state = State.Immune;
                ballon_timer = 0;
            }

            if (ballon_touched == true)                                             // case2: 사망
            {
                player_state = State.Destroying;
            }
            if (ballon_timer > 3)
            {
                player_state = State.Destroying;
            }
        }

        else if (player_state == State.Destroying)  //사망
        { 
            Destroy(gameObject);
        }

        else if (player_state == State.Immune)      //무적=>물풍선 탈출 시 사용
        {
            ballon_timer += Time.deltaTime;

            if (ballon_timer >= 1)
            {
                player_state = State.Playing;
            }
        }

        else if (player_state == State.Turtle)     //거북이 플레이
        {
            speed = basic_speed + speed_item * speed_increase;

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.UP1]))       //UP1
            {
                UP1_key = true;
                DOWN1_key = false;
                RIGHT1_key = false;
                LEFT1_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.UP1]))
            {
                UP1_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN1]))       //DOWN1
            {
                UP1_key = false;
                DOWN1_key = true;
                RIGHT1_key = false;
                LEFT1_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.DOWN1]))
            {
                DOWN1_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.RIGHT1]))       //RIGHT1
            {
                UP1_key = false;
                DOWN1_key = false;
                RIGHT1_key = true;
                LEFT1_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.RIGHT1]))
            {
                RIGHT1_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.LEFT1]))      //LEFT1
            {
                UP1_key = false;
                DOWN1_key = false;
                RIGHT1_key = false;
                LEFT1_key = true;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.LEFT1]))
            {
                LEFT1_key = false;
            }

            if (UP1_key)
            {
                Vector2 move = new Vector2(0, 1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (DOWN1_key)
            {
                Vector2 move = new Vector2(0, -1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (RIGHT1_key)
            {
                Vector2 move = new Vector2(1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (LEFT1_key)
            {
                Vector2 move = new Vector2(-1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }

            pposition = MapManager.instance.GetClosestCellPosition(transform.position); //player position -> pposition
            cur_tile_info = MapManager.instance.GetClosestTileInfo(transform.position); //current tile information

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL1]))                     // 물풍선 키 입력
            {
                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //목표 위치에 오브젝트(벽, 상자, 물폭탄, 물줄기, 아이템 등)가 없을 경우에만 물폭탄 설치, 해당 조건 확인 필요
                    if (MapManager.instance.GetClosestTileInfo(pposition).CheckState(TileInfo.State.none))
                        GenerateWaterBomb(pposition, ballon_range + range_item);//물풍선 오브젝트 생성<-ppostion+object 생성 명령어
                }
                else
                {
                    //물폭탄이 필드에 최대 갯수만큼 설치되어 있을 경우의 코드
                }
            }

            if (cur_tile_info.CheckState(TileInfo.State.water_ray))
            {
                player_state = State.Immune;
                Debug.Log("Hit");
            }
            if (cur_tile_info.CheckState(TileInfo.State.item))
            {
                cur_tile_info.UseItem(this);
            }
        }
    

    }
}