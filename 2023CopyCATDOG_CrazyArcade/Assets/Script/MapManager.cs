using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager: MonoBehaviour
{
    //�̱��� �ν��Ͻ� -> �޸� ȿ���� ���� �� ��ȯ�� del instance ������� ��?
    public static MapManager instance;


    [HideInInspector]public float tile_size;
    Vector2Int map_size;

    Vector3 bottom_left_tile_pos;

    //���� Ÿ�� ������ ��� 2���� �迭, �ش� Ÿ�Ͽ� ���ٱ�, ������ ���� �ִ��� �����ϴ� ������ ��
    public TileInfo[,] tile_infos;
    public TileInfo GetTileInfo(Vector2Int index)
    {
        return tile_infos[index.x, index.y];
    }
    public TileInfo GetClosestTileInfo(Vector2 position)
    {
        Vector2Int index = GetClosestCellIndex(position);
        return tile_infos[index.x, index.y];
    }


    [SerializeField] Tilemap ground;
    [SerializeField] Tilemap blocks;

    //List<GameObject> boxes;
    //List<GameObject> waterbombs;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        tile_size = ground.cellSize.x;
        map_size = (Vector2Int)ground.size;

        bottom_left_tile_pos = ground.origin + new Vector3(tile_size / 2, tile_size / 2, 0);

        tile_infos = new TileInfo[map_size.x, map_size.y];
        for (int i = 0; i < map_size.y; i++)
        {
            for (int j = 0; j < map_size.x; j++)
            {
                tile_infos[j, i] = new TileInfo();
            }
        }
    }

    //��ǥ �������� ���� ����� Ÿ���� �ε����� ��ȯ
    public Vector2Int GetClosestCellIndex(Vector2 position)
    {
        Vector2Int cell_index = new Vector2Int();

        if (position.x < bottom_left_tile_pos.x)
        {
            cell_index.x = 0;
        }
        else
        {
            float distance_x = (position.x - bottom_left_tile_pos.x) % tile_size;
            if (distance_x < tile_size / 2)
                cell_index.x = Mathf.RoundToInt(((position.x - distance_x - bottom_left_tile_pos.x) / tile_size));
            else
                cell_index.x = Mathf.RoundToInt(((position.x - distance_x - bottom_left_tile_pos.x) / tile_size)) + 1;
        }

        if (position.y < bottom_left_tile_pos.y)
        {
            cell_index.y = 0;
        }
        else
        {
            float distance_y = (position.y - bottom_left_tile_pos.y) % tile_size;
            if (distance_y < tile_size / 2)
                cell_index.y = Mathf.RoundToInt(((position.y - distance_y - bottom_left_tile_pos.y) / tile_size));
            else
                cell_index.y = Mathf.RoundToInt(((position.y - distance_y - bottom_left_tile_pos.y) / tile_size)) + 1;
        }
        
        return cell_index;
    }

    //��ǥ �������� ���� ����� Ÿ���� ��ǥ�� ��ȯ
    public Vector2 GetClosestCellPosition(Vector2 position)
    {
        Vector2 cell_position = new Vector2();

        if (position.x < bottom_left_tile_pos.x)
        {
            cell_position.x = bottom_left_tile_pos.x;
        }
        else
        {
            float distance_x = (position.x - bottom_left_tile_pos.x) % tile_size;
            if (distance_x < tile_size / 2)
                cell_position.x = position.x - distance_x;
            else
                cell_position.x = position.x - distance_x + tile_size;
        }

        if (position.y < bottom_left_tile_pos.y)
        {
            cell_position.y = 0;
        }
        else
        {
            float distance_y = (position.y - bottom_left_tile_pos.y) % tile_size;
            if (distance_y < tile_size / 2)
                cell_position.y = position.y - distance_y;
            else
                cell_position.y = position.y - distance_y + tile_size;
        }

        return cell_position;
    }


    #region for_debugging
    void PrintTileInfos()
    {
        String s = new String("");
        for(int i = map_size.y - 1; i >= 0; i--)
        {
            for(int j = 0; j < map_size.x; j++)
            {
                s += tile_infos[j, i].GetState(TileInfo.State.water_bomb) + " " + tile_infos[j, i].GetState(TileInfo.State.water_ray) + "|";
            }
            s += "\n";
        }
        Debug.Log(s);
    }
    #endregion
}
