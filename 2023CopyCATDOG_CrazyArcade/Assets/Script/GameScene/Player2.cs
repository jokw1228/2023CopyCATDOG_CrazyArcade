using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Player
{

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        Player.player2 = this;
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();
    }

    bool UP2_key = false;
    bool DOWN2_key = false;
    bool RIGHT2_key = false;
    bool LEFT2_key = false;

    private Player1 Opposite;

    private Animator animator;

    Collider2D collider2d;
    public GameObject turtle;
    public GameObject pirate_turtle;

    public AudioSource sound;
    public AudioClip[] audiolists;

    public void SoundPlay(AudioClip clip)
    {
        sound.clip = clip;
        sound.loop = false;
        sound.volume = 0.1f;
        sound.Play();
    }

    override protected void Update()
    {
        base.Update();

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

            pposition = MapManager.instance.GetClosestCellPosition(transform.position); //player position -> pposition
            cur_tile_info = MapManager.instance.GetClosestTileInfo(transform.position); //current tile information

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL2]))                     // ��ǳ�� Ű �Է�
            {

                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //��ǥ ��ġ�� ��, ����, ����ź�� ���� ��쿡�� ����ź ��ġ
                    if (!MapManager.instance.GetClosestTileInfo(pposition).CheckState((TileInfo.State)((int)TileInfo.State.wall + (int)TileInfo.State.box + (int)TileInfo.State.water_bomb)))
                    {
                        GenerateWaterBomb(pposition, ballon_range + range_item);//��ǳ�� ������Ʈ ����<-ppostion+object ���� ��ɾ�
                        SoundPlay(audiolists[1]);
                    }
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
                SoundPlay(audiolists[0]);
            }
        }

        else if (player_state == State.Imprisoned)  //��ǳ�� ����
        {

            ballon_timer += Time.deltaTime;

            //GetComponent<SpriteRenderer>().color = Color.blue;
            animator.SetBool("IsImprisoned", true);

            collider2d.isTrigger = true;

            if ((Input.GetKeyDown(KeySetting.keys[KeyAction.ITEM2])) && active_item_slot == ActiveItem.needle) // case1: �z��
            {
                active_item_slot = ActiveItem.none;
                GetComponent<SpriteRenderer>().color = Color.white;
                player_state = State.Immune;
                SoundPlay(audiolists[2]);
                ballon_timer = 0;
            }

            if (ballon_touched == true)                                             // case2: ���
            {
                player_state = State.Destroying;
                SoundPlay(audiolists[3]);
            }
            if (ballon_timer > 3)
            {
                player_state = State.Destroying;
                SoundPlay(audiolists[3]);
            }
        }

        else if (player_state == State.Destroying)  //���
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            animator.SetBool("IsImprisoned", false);
            animator.SetTrigger("DIE1");
            Player.player1.player_state = State.Endgame;
            death_timer += Time.deltaTime;
            if (death_timer >= 1)
            {
                death_timer = 0;
                GameManager.Inst.GameOver();
                Destroy(gameObject);
            }
        }

        else if (player_state == State.Immune)      //����=>��ǳ�� Ż�� �� ���
        {
            collider2d.isTrigger = false;
            animator.SetBool("IsImprisoned", false);

            ballon_timer += Time.deltaTime;

            if (ballon_timer >= 1)
            {
                player_state = State.Playing;
            }
        }

        else if (player_state == State.Turtle)     //�ź��� �÷���
        {
            speed = 2;

            GetComponent<SpriteRenderer>().color = Color.green;

            this.turtle.SetActive(true);

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

            pposition = MapManager.instance.GetClosestCellPosition(transform.position); //player position -> pposition
            cur_tile_info = MapManager.instance.GetClosestTileInfo(transform.position); //current tile information

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL2]))                     // ��ǳ�� Ű �Է�
            {
                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //��ǥ ��ġ�� ��, ����, ����ź�� ���� ��쿡�� ����ź ��ġ
                    if (!MapManager.instance.GetClosestTileInfo(pposition).CheckState((TileInfo.State)((int)TileInfo.State.wall + (int)TileInfo.State.box + (int)TileInfo.State.water_bomb)))
                    {
                        SoundPlay(audiolists[1]);
                        GenerateWaterBomb(pposition, ballon_range + range_item);//��ǳ�� ������Ʈ ����<-ppostion+object ���� ��ɾ�
                    }
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
                this.turtle.SetActive(false);
                this.pirate_turtle.SetActive(false);
            }
            if (cur_tile_info.CheckState(TileInfo.State.item))
            {
                cur_tile_info.UseItem(this);
                SoundPlay(audiolists[0]);
            }
        }

        else if (player_state == State.Pirate)     //���� �ź��� �÷���
        {
            speed = 8;

            GetComponent<SpriteRenderer>().color = Color.red;

            this.pirate_turtle.SetActive(true);

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

            pposition = MapManager.instance.GetClosestCellPosition(transform.position); //player position -> pposition
            cur_tile_info = MapManager.instance.GetClosestTileInfo(transform.position); //current tile information

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.BALL2]))                     // ��ǳ�� Ű �Է�
            {
                if (WaterBomb.num_of_cur_water_bomb < water_bomb_max + balloon_item)
                {
                    //��ǥ ��ġ�� ��, ����, ����ź�� ���� ��쿡�� ����ź ��ġ
                    if (!MapManager.instance.GetClosestTileInfo(pposition).CheckState((TileInfo.State)((int)TileInfo.State.wall + (int)TileInfo.State.box + (int)TileInfo.State.water_bomb)))
                    {
                        SoundPlay(audiolists[1]);
                        GenerateWaterBomb(pposition, ballon_range + range_item);//��ǳ�� ������Ʈ ����<-ppostion+object ���� ��ɾ�
                    }
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
                this.turtle.SetActive(false);
                this.pirate_turtle.SetActive(false);
            }
            if (cur_tile_info.CheckState(TileInfo.State.item))
            {
                cur_tile_info.UseItem(this);
                SoundPlay(audiolists[0]);
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

        else if (player_state == State.Endgame) 
        {

        }

    }

    void FixedUpdate()
    {
        if ((player_state == State.Playing) || (player_state == State.Turtle) || (player_state == State.Pirate))
        {
            if (UP2_key)
            {
                animator.speed = 1;
                animator.SetTrigger("UP1");
                Vector2 move = new Vector2(0, 1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (DOWN2_key)
            {
                animator.speed = 1;
                animator.SetTrigger("DOWN1");
                Vector2 move = new Vector2(0, -1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (RIGHT2_key)
            {
                animator.speed = 1;
                animator.SetTrigger("RIGHT1");
                Vector2 move = new Vector2(1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (LEFT2_key)
            {
                animator.speed = 1;
                animator.SetTrigger("LEFT1");
                Vector2 move = new Vector2(-1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if ((!UP2_key) && (!DOWN2_key) && (!RIGHT2_key) && (!LEFT2_key))
            {
                animator.speed = 0;
            }
        }
        else if ((player_state == State.Standby) || (player_state == State.Imprisoned))
        {
            animator.speed = 0;
        }
        else if ((player_state == State.Destroying))
        {
            animator.speed = 1;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            if (Vector3.Distance(other.transform.position, transform.position) < 0.5f)
                if(player_state == State.Imprisoned)
                    player_state = State.Destroying;
    }
}