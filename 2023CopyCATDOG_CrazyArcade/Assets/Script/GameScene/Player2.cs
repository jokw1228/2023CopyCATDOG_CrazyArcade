using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Player
{

    // Start is called before the first frame update
    void Start()
    {
        Vector2 vector = new Vector2(0, 0);
    }

    bool UP2_key = false;
    bool DOWN2_key = false;
    bool RIGHT2_key = false;
    bool LEFT2_key = false;

    private Player1 Opposite;

    void Update()
    {
        if (player_state == State.Playing)          //�÷���
        {
            speed = basic_speed + speed_item * speed_increase;

            GetComponent<SpriteRenderer>().color = Color.white;

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.UP2]))       //UP2
            {
                UP2_key = true;
                DOWN2_key = false;
                RIGHT2_key = false;
                LEFT2_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.UP2]))
            {
                UP2_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN2]))       //DOWN2
            {
                UP2_key = false;
                DOWN2_key = true;
                RIGHT2_key = false;
                LEFT2_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.DOWN2]))
            {
                DOWN2_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.RIGHT2]))       //RIGHT2
            {
                UP2_key = false;
                DOWN2_key = false;
                RIGHT2_key = true;
                LEFT2_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.RIGHT2]))
            {
                RIGHT2_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.LEFT2]))      //LEFT2
            {
                UP2_key = false;
                DOWN2_key = false;
                RIGHT2_key = false;
                LEFT2_key = true;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.LEFT2]))
            {
                LEFT2_key = false;
            }

            if (UP2_key)
            {
                Vector2 move = new Vector2(0, 1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (DOWN2_key)
            {
                Vector2 move = new Vector2(0, -1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (RIGHT2_key)
            {
                Vector2 move = new Vector2(1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (LEFT2_key)
            {
                Vector2 move = new Vector2(-1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }

            pposition = MapManager.instance.GetClosestCellPosition(transform.position); //player position -> pposition
            cur_tile_info = MapManager.instance.GetClosestTileInfo(transform.position); //current tile information

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL2]))                     // ��ǳ�� Ű �Է�
            {

                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //��ǥ ��ġ�� ������Ʈ(��, ����, ����ź, ���ٱ�, ������ ��)�� ���� ��쿡�� ����ź ��ġ, �ش� ���� Ȯ�� �ʿ�
                    if (MapManager.instance.GetClosestTileInfo(pposition).CheckState(TileInfo.State.none))
                        GenerateWaterBomb(pposition, ballon_range + range_item);//��ǳ�� ������Ʈ ����<-ppostion+object ���� ���ɾ�
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

            if ((Input.GetKeyDown(KeySetting.keys[KeyAction.ITEM2])) && needle >= 1) // case1: �z��
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
            Destroy(gameObject);
            Opposite = GameObject.Find("Player1").GetComponent<Player1>();
            Opposite.player_state = State.Endgame;
        }

        else if (player_state == State.Immune)      //����=>��ǳ�� Ż�� �� ���
        {
            ballon_timer += Time.deltaTime;

            if (ballon_timer >= 1)
            {
                player_state = State.Playing;
            }
        }

        else if (player_state == State.Turtle)     //�ź��� �÷���
        {
            speed = basic_speed + speed_item * speed_increase;

            GetComponent<SpriteRenderer>().color = Color.green;

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.UP2]))       //UP2
            {
                UP2_key = true;
                DOWN2_key = false;
                RIGHT2_key = false;
                LEFT2_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.UP2]))
            {
                UP2_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN2]))       //DOWN2
            {
                UP2_key = false;
                DOWN2_key = true;
                RIGHT2_key = false;
                LEFT2_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.DOWN2]))
            {
                DOWN2_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.RIGHT2]))       //RIGHT2
            {
                UP2_key = false;
                DOWN2_key = false;
                RIGHT2_key = true;
                LEFT2_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.RIGHT2]))
            {
                RIGHT2_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.LEFT2]))      //LEFT2
            {
                UP2_key = false;
                DOWN2_key = false;
                RIGHT2_key = false;
                LEFT2_key = true;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.LEFT2]))
            {
                LEFT2_key = false;
            }

            if (UP2_key)
            {
                Vector2 move = new Vector2(0, 1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (DOWN2_key)
            {
                Vector2 move = new Vector2(0, -1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (RIGHT2_key)
            {
                Vector2 move = new Vector2(1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (LEFT2_key)
            {
                Vector2 move = new Vector2(-1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }

            pposition = MapManager.instance.GetClosestCellPosition(transform.position); //player position -> pposition
            cur_tile_info = MapManager.instance.GetClosestTileInfo(transform.position); //current tile information

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL2]))                     // ��ǳ�� Ű �Է�
            {
                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //��ǥ ��ġ�� ������Ʈ(��, ����, ����ź, ���ٱ�, ������ ��)�� ���� ��쿡�� ����ź ��ġ, �ش� ���� Ȯ�� �ʿ�
                    if (MapManager.instance.GetClosestTileInfo(pposition).CheckState(TileInfo.State.none))
                        GenerateWaterBomb(pposition, ballon_range + range_item);//��ǳ�� ������Ʈ ����<-ppostion+object ���� ���ɾ�
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

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.UP2]))       //UP2
            {
                UP2_key = true;
                DOWN2_key = false;
                RIGHT2_key = false;
                LEFT2_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.UP2]))
            {
                UP2_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.DOWN2]))       //DOWN2
            {
                UP2_key = false;
                DOWN2_key = true;
                RIGHT2_key = false;
                LEFT2_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.DOWN2]))
            {
                DOWN2_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.RIGHT2]))       //RIGHT2
            {
                UP2_key = false;
                DOWN2_key = false;
                RIGHT2_key = true;
                LEFT2_key = false;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.RIGHT2]))
            {
                RIGHT2_key = false;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.LEFT2]))      //LEFT2
            {
                UP2_key = false;
                DOWN2_key = false;
                RIGHT2_key = false;
                LEFT2_key = true;
            }
            else if (Input.GetKeyUp(KeySetting.keys[KeyAction.LEFT2]))
            {
                LEFT2_key = false;
            }

            if (UP2_key)
            {
                Vector2 move = new Vector2(0, 1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (DOWN2_key)
            {
                Vector2 move = new Vector2(0, -1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (RIGHT2_key)
            {
                Vector2 move = new Vector2(1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (LEFT2_key)
            {
                Vector2 move = new Vector2(-1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }

            pposition = MapManager.instance.GetClosestCellPosition(transform.position); //player position -> pposition
            cur_tile_info = MapManager.instance.GetClosestTileInfo(transform.position); //current tile information

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL2]))                     // ��ǳ�� Ű �Է�
            {
                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //��ǥ ��ġ�� ������Ʈ(��, ����, ����ź, ���ٱ�, ������ ��)�� ���� ��쿡�� ����ź ��ġ, �ش� ���� Ȯ�� �ʿ�
                    if (MapManager.instance.GetClosestTileInfo(pposition).CheckState(TileInfo.State.none))
                        GenerateWaterBomb(pposition, ballon_range + range_item);//��ǳ�� ������Ʈ ����<-ppostion+object ���� ���ɾ�
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
            ballon_timer += Time.deltaTime;

            if (ballon_timer > 5)
            {
                ballon_timer = 0;
                player_state = State.Playing;
            }
        }

        else if (player_state == State.Endgame)
        {

        }

    }
}