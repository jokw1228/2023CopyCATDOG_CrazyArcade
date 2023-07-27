using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Player
{

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        Player.player1 = this;
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();
    }

    bool UP1_key = false;
    bool DOWN1_key = false;
    bool RIGHT1_key = false;
    bool LEFT1_key = false;

    private Animator animator;

    private Player2 Opposite;

    Collider2D collider2d;

    override protected void Update()
    {
        base.Update();

        if (player_state == State.Playing)          //플레이
        {
            speed = basic_speed + speed_item * speed_increase;

            GetComponent<SpriteRenderer>().color = Color.white;

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


            pposition = MapManager.instance.GetClosestCellPosition(transform.position); //player position -> pposition
            cur_tile_info = MapManager.instance.GetClosestTileInfo(transform.position); //current tile information

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL1]))                     // 물풍선 키 입력
            {

                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //목표 위치에 벽, 상자, 물폭탄이 없을 경우에만 물폭탄 설치
                    if (!MapManager.instance.GetClosestTileInfo(pposition).CheckState((TileInfo.State)((int)TileInfo.State.wall + (int)TileInfo.State.box + (int)TileInfo.State.water_bomb)))
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

            //GetComponent<SpriteRenderer>().color = Color.blue;
            animator.SetBool("IsImprisoned", true);

            collider2d.isTrigger = true;

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
            GetComponent<SpriteRenderer>().color = Color.white;
            animator.SetBool("IsImprisoned", false);
            animator.SetTrigger("DIE1");
            Player.player2.player_state = State.Endgame;
            death_timer += Time.deltaTime;
            if (death_timer >= 1)
            {
                GameManager.Inst.GameOver();
                Destroy(gameObject);
            }
        }

        else if (player_state == State.Immune)      //무적=>물풍선 탈출 시 사용
        {
            collider2d.isTrigger = false;
            animator.SetBool("IsImprisoned", false);

            ballon_timer += Time.deltaTime;

            if (ballon_timer >= 1)
            {
                ballon_timer = 0;
                player_state = State.Playing;
            }
        }

        else if (player_state == State.Turtle)     //거북이 플레이
        {
            speed = basic_speed + speed_item * speed_increase;

            GetComponent<SpriteRenderer>().color = Color.green;

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

            pposition = MapManager.instance.GetClosestCellPosition(transform.position); //player position -> pposition
            cur_tile_info = MapManager.instance.GetClosestTileInfo(transform.position); //current tile information

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL1]))                     // 물풍선 키 입력
            {
                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //목표 위치에 벽, 상자, 물폭탄이 없을 경우에만 물폭탄 설치
                    if (!MapManager.instance.GetClosestTileInfo(pposition).CheckState((TileInfo.State)((int)TileInfo.State.wall + (int)TileInfo.State.box + (int)TileInfo.State.water_bomb)))
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

        else if (player_state == State.Pirate)     //해적 거북이 플레이
        {
            speed = basic_speed + speed_item * speed_increase;

            GetComponent<SpriteRenderer>().color = Color.red;

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


            pposition = MapManager.instance.GetClosestCellPosition(transform.position); //player position -> pposition
            cur_tile_info = MapManager.instance.GetClosestTileInfo(transform.position); //current tile information

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL1]))                     // 물풍선 키 입력
            {
                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //목표 위치에 벽, 상자, 물폭탄이 없을 경우에만 물폭탄 설치
                    if (!MapManager.instance.GetClosestTileInfo(pposition).CheckState((TileInfo.State)((int)TileInfo.State.wall + (int)TileInfo.State.box + (int)TileInfo.State.water_bomb)))
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

        else if (player_state == State.Standby)
        {
            Standby_timer += Time.deltaTime;
            Debug.Log("Stand");
            if (Standby_timer >= 1)
            {
                Standby_timer = 0;
                player_state = State.Playing;
            }
        }

        else if (player_state == State.Endgame) //더미됨
        {

        }
    }

    void FixedUpdate()
    {
        if ((player_state == State.Playing) || (player_state == State.Turtle) || (player_state == State.Pirate))
        {
            if (UP1_key)
            {
                animator.speed = 1;
                animator.SetTrigger("UP1");
                Vector2 move = new Vector2(0, 1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (DOWN1_key)
            {
                animator.speed = 1;
                animator.SetTrigger("DOWN1");
                Vector2 move = new Vector2(0, -1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (RIGHT1_key)
            {
                animator.speed = 1;
                animator.SetTrigger("RIGHT1");
                Vector2 move = new Vector2(1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (LEFT1_key)
            {
                animator.speed = 1;
                animator.SetTrigger("LEFT1");
                Vector2 move = new Vector2(-1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if ((!UP1_key) && (!DOWN1_key) && (!RIGHT1_key) && (!LEFT1_key))
            {
                animator.speed = 0;
            }
        }
        else if ((player_state == State.Standby) || (player_state == State.Imprisoned))
        {
            animator.speed = 0;
        }
        else if((player_state == State.Destroying))
        {
            animator.speed = 1;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            if(Vector3.Distance(other.transform.position, transform.position) < 0.5f)
                if (player_state == State.Imprisoned)
                    player_state = State.Destroying;
    }
}