using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;


public class TileInfo
{
    public enum State
    {
        none = 0b0000_0000,
        player = 0b0000_0001,
        wall = 0b0000_0010,
        box = 0b0000_0100,
        water_bomb = 0b0000_1000,
        water_ray = 0b0001_0000,
        item = 0b0010_0000
    }

    State state = State.none;

    Box box = null;
    IItem item = null;
    WaterBomb water_bomb = null;

    public Box get_box { get { return box; } }
    public IItem get_item { get { return item; } }
    public WaterBomb get_water_bomb { get { return water_bomb; } }

    public bool is_empty { get { return state == State.none; } }

    public void UseItem(Player player)
    {
        Debug.Log(item.GetType());

        item.OnGetItem(player);

        item.Remove();
        item = null;
        state &= ~State.item;
    }

    //���� Ÿ�� ������ Ư�� State�� �߰�
    public void AddState(State s)
    {
        this.state |= s;
    }
    //���� Ÿ�� ������ Ư�� State�� ����
    public void DelState(State s)
    {
        this.state &= ~s;
    }
    //���� Ÿ�� ������ Ư�� State�� �ִ��� Ȯ��
    public bool CheckState(State s)
    {
        return (state & s) != State.none;
    }
    public void AddBox(Box box)
    {
        AddState(State.box);
        this.box = box;
    }
    public void DelBox()
    {
        DelState(State.box);
        this.box = null;
    }
    public void AddItem(IItem item)
    {
        AddState(State.item);
        this.item = item;
    }
    public void DelItem()
    {
        DelState(State.item);
        this.item = null;
    }
    public void AddWaterBomb(WaterBomb water_bomb)
    {
        AddState(State.water_bomb);
        this.water_bomb = water_bomb;
    }
    public void DelWaterBomb()
    {
        DelState(State.water_bomb);
        this.water_bomb = null;
    }
}
