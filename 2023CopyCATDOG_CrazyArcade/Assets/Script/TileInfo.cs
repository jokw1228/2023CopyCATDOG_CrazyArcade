using System.Collections;
using System.Collections.Generic;
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
        item3      = 0b0010_0000, fluid  = 0b0010_0000, water_ray_range_up = 0b0010_0000
    }

    State state = State.none;

    //현재 타일 정보의 특정 State를 추가
    public void AddState(State s)
    {
        state |= s;
    }
    //현재 타일 정보의 특정 State를 제거
    public void DelState(State s)
    {
        state &= ~s;
    }
    //현재 타일 정보에 특정 State가 있는지 확인
    public bool GetState(State s)
    {
        return (state & s) == s;
    }
}
