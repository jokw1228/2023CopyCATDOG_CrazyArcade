using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]public struct ItemPrefabPair
{
    public Item item;
    public GameObject prefab;
    public int stock;
}

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] List<ItemPrefabPair> item_prefabs;
    Dictionary<Item, GameObject> item_prefab_dict = new Dictionary<Item, GameObject>();
    static Dictionary<Item, int> item_stock = new Dictionary<Item, int>();

    [Space]
    public GameObject airplane_prefab;
    public float airplane_generate_cycle;
    float timer = 0f;

    public static Item random_item
    {
        get
        {
            int stock_sum = 0;

            foreach (int s in item_stock.Values)
            {
                stock_sum += s;
            }

            int rand_int = UnityEngine.Random.Range(0, stock_sum);

            foreach (Item i in item_stock.Keys)
            {
                if (item_stock[i] > rand_int)
                    return i;
                rand_int -= item_stock[i];
            }

            return Item.none;
        }
    }
    private void Awake()
    {
        foreach (ItemPrefabPair pair in item_prefabs) {
            item_prefab_dict.Add(pair.item, pair.prefab);
            item_stock.Add(pair.item, pair.stock);
        }
    }
    private void Update()
    {
        if(timer > airplane_generate_cycle)
        {
            if (!MapManager.instance.ItemOrBoxExist())
            {
                timer = 0;
                GenerateAirplane();
            }
        }
        timer += Time.deltaTime;
    }

    public void GenerateItem(Item item, Vector2Int cell_index)
    {
        if (item == Item.none)
            return;

        IItem i_item = Instantiate<GameObject>(item_prefab_dict[item], MapManager.instance.GetCellPosition(cell_index), Quaternion.identity).GetComponent<IItem>();
        MapManager.instance.GetTileInfo(cell_index).AddItem(i_item);
    }

    public void GenerateAirplane()
    {
        int item_cnt = 2; //UnityEngine.Random.Range(1, 3);
        bool is_from_right = true; // UnityEngine.Random.Range(0, 2) == 0;
        Vector2 pos = new();
        pos.x = is_from_right ? MapManager.instance.right_tile_x : MapManager.instance.left_tile_x;
        pos.y = UnityEngine.Random.Range(MapManager.instance.bottom_tile_y, MapManager.instance.top_tile_y);

        GameObject airplane = Instantiate<GameObject>(airplane_prefab, MapManager.instance.GetClosestCellPosition(pos), Quaternion.identity);
        airplane.GetComponent<Airplane>().SetAirplane(is_from_right, item_cnt);
    }
}
