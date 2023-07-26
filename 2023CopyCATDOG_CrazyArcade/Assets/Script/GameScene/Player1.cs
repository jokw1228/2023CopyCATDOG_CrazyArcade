using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Player
{

    // Start is called before the first frame update
    void Start()
    {
        Player.player1 = this;
        animator = GetComponent<Animator>();
    }

    bool UP1_key = false;
    bool DOWN1_key = false;
    bool RIGHT1_key = false;
    bool LEFT1_key = false;

    private Animator animator;

    private Player2 Opposite;


    void Update()
    {
        if (player_state == State.Playing)          //�÷���
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

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL1]))                     // ��ǳ�� Ű �Է�
            {

                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //��ǥ ��ġ�� ������Ʈ(��, ����, ����ź, ���ٱ�, ������ ��)�� ���� ��쿡�� ����ź ��ġ, �ش� ���� Ȯ�� �ʿ�
                    if (MapManager.instance.GetClosestTileInfo(pposition).is_empty)
                        GenerateWaterBomb(pposition, ballon_range + range_item);//��ǳ�� ������Ʈ ����<-ppostion+object ���� ��ɾ�
                }
                else
                {
                                                                                        //����ź�� �ʵ忡 �ִ� ������ŭ ��ġ�Ǿ� ���� ����� �ڵ�
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

        else if (player_state == State.Imprisoned)  //��ǳ�� ����
        {

            ballon_timer += Time.deltaTime;

            GetComponent<SpriteRenderer>().color = Color.blue;

            if ((Input.GetKeyDown(KeySetting.keys[KeyAction.ITEM1])) && needle >= 1) // case1: �z��
            {
                needle -= 1;
                GetComponent<SpriteRenderer>().color = Color.white;
                player_state = State.Immune;
                ballon_timer = 0;
            }

            if (ballon_touched == true)                                             // case2: ���
            {
                player_state = State.Destroying;
            }
            if (ballon_timer > 3)
            {
                player_state = State.Destroying;
            }
        }

        else if (player_state == State.Destroying)  //���
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            animator.SetTrigger("DIE1");
            Player.player2.player_state = State.Endgame;
            death_timer += Time.deltaTime;
            if (death_timer >= 1)
            {
                GameManager.Inst.GameOver();
                Destroy(gameObject);
            }
        }

        else if (player_state == State.Immune)      //����=>��ǳ�� Ż�� �� ���
        {
            ballon_timer += Time.deltaTime;

            if (ballon_timer >= 1)
            {
                ballon_timer = 0;
                player_state = State.Playing;
            }
        }

        else if (player_state == State.Turtle)     //�ź��� �÷���
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

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL1]))                     // ��ǳ�� Ű �Է�
            {
                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //��ǥ ��ġ�� ������Ʈ(��, ����, ����ź, ���ٱ�, ������ ��)�� ���� ��쿡�� ����ź ��ġ, �ش� ���� Ȯ�� �ʿ�
                    if (MapManager.instance.GetClosestTileInfo(pposition).is_empty)
                        GenerateWaterBomb(pposition, ballon_range + range_item);//��ǳ�� ������Ʈ ����<-ppostion+object ���� ��ɾ�
                }
                else
                {
                    //����ź�� �ʵ忡 �ִ� ������ŭ ��ġ�Ǿ� ���� ����� �ڵ�
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

        else if (player_state == State.Pirate)     //���� �ź��� �÷���
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

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL1]))                     // ��ǳ�� Ű �Է�
            {
                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //��ǥ ��ġ�� ������Ʈ(��, ����, ����ź, ���ٱ�, ������ ��)�� ���� ��쿡�� ����ź ��ġ, �ش� ���� Ȯ�� �ʿ�
                    if (MapManager.instance.GetClosestTileInfo(pposition).CheckState(TileInfo.State.none))
                        GenerateWaterBomb(pposition, ballon_range + range_item);//��ǳ�� ������Ʈ ����<-ppostion+object ���� ��ɾ�
                }
                else
                {
                    //����ź�� �ʵ忡 �ִ� ������ŭ ��ġ�Ǿ� ���� ����� �ڵ�
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

        else if (player_state == State.Endgame) //���̵�
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
}