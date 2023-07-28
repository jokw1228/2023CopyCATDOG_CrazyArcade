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

    Collider2D collider2d;

    override protected void Update()
    {
        base.Update();

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
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.ITEM1]))
            {
                switch (active_item_slot)
                {
                    case ActiveItem.wind:
                        GenerateWind(direction);
                        active_item_slot = ActiveItem.none;
                        break;
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

            if ((Input.GetKeyDown(KeySetting.keys[KeyAction.ITEM1])) && active_item_slot == ActiveItem.needle) // case1: �z��
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
            Player.player2.player_state = State.Endgame;
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
            speed = basic_speed + speed_item * speed_increase;
            collider2d.isTrigger = false;
            animator.SetBool("IsImprisoned", false);

            ballon_timer += Time.deltaTime;

            if (ballon_timer >= 1)
            {
                ballon_timer = 0;
                player_state = State.Playing;
            }
        }

        else if (player_state == State.Turtle)     //�ź��� �÷���
        {
            speed = 2;

            GetComponent<SpriteRenderer>().color = Color.green;

            this.turtle.SetActive(true);

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

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.ITEM1]))
            {
                switch (active_item_slot)
                {
                    case ActiveItem.wind:
                        GenerateWind(direction);
                        active_item_slot = ActiveItem.none;
                        break;
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
                    {
                    //��ǥ ��ġ�� ��, ����, ����ź�� ���� ��쿡�� ����ź ��ġ
                    if (!MapManager.instance.GetClosestTileInfo(pposition).CheckState((TileInfo.State)((int)TileInfo.State.wall + (int)TileInfo.State.box + (int)TileInfo.State.water_bomb)))
                        GenerateWaterBomb(pposition, ballon_range + range_item);//��ǳ�� ������Ʈ ����<-ppostion+object ���� ��ɾ�
                        SoundPlay(audiolists[1]);
                    }
                }
                else
                {
                    //����ź�� �ʵ忡 �ִ� ������ŭ ��ġ�Ǿ� ���� ����� �ڵ�
                }
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.ITEM1]))
            {
                switch (active_item_slot)
                {
                    case ActiveItem.wind:
                        GenerateWind(direction);
                        active_item_slot = ActiveItem.none;
                        break;
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
            if (UP1_key)
            {
                animator.speed = 1;
                animator.SetTrigger("UP1");
                direction = MapManager.Direction.up;
                Vector2 move = new Vector2(0, 1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (DOWN1_key)
            {
                animator.speed = 1;
                animator.SetTrigger("DOWN1");
                direction = MapManager.Direction.down;
                Vector2 move = new Vector2(0, -1);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (RIGHT1_key)
            {
                animator.speed = 1;
                animator.SetTrigger("RIGHT1");
                direction = MapManager.Direction.right;
                Vector2 move = new Vector2(1, 0);
                transform.Translate(move * Time.deltaTime * speed);
            }
            if (LEFT1_key)
            {
                animator.speed = 1;
                animator.SetTrigger("LEFT1");
                direction = MapManager.Direction.left;
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