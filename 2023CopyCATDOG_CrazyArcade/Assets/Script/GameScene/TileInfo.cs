using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;


public class TileInfo
{
    public enum State
    {
        none       = 0b0000_0000,
        water_bomb = 0b0000_0001,
        water_ray  = 0b0000_0010,
        box        = 0b0000_0100,

        item1      = 0b0000_1000, bubble = 0b0000_1000, inventory_size_up  = 0b0000_1000,
        item2      = 0b0001_0000, roller = 0b0001_0000, speed_up           = 0b0001_0000,
        item3      = 0b0010_0000, fluid  = 0b0010_0000, water_ray_range_up = 0b0010_0000,

        items = item1 + item2 + item3,
    }

    State state = State.none;
    IItem item = null;

    public bool HaveItem()
    {
        return (state & State.items) != State.none;
    }

    public void UseItem(Player1 player)
    {
        Debug.Log(item.GetType());

        item.OnGetItem(player);

        item.Remove();
        item = null;
        state &= ~State.items;
    }

    //���� Ÿ�� ������ Ư�� State�� �߰�
    public void AddState(State s)
    {
        this.state |= s;
    }
    public void AddItem(State s, IItem item)
    {
        state |= s;
        this.item = item;
    }
    //���� Ÿ�� ������ Ư�� State�� ����
    public void DelState(State s)
    {
        this.state &= ~s;
    }
    //���� Ÿ�� ������ Ư�� State�� �ִ��� Ȯ��
    public bool GetState(State s)
    {
        return (state & s) == s;
    }
}
