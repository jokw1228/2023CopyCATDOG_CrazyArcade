using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    Dictionary<Item, int> item_stock = new Dictionary<Item, int>();

    private void Awake()
    {
        foreach (ItemPrefabPair pair in item_prefabs) {
            item_prefab_dict.Add(pair.item, pair.prefab);
            item_stock.Add(pair.item, pair.stock);
        }
    }

    public void GenerateItem(Item item, Vector2Int cell_index)
    {
        if (item == Item.none)
            return;

        IItem i_item = Instantiate<GameObject>(item_prefab_dict[item], MapManager.instance.GetCellPosition(cell_index), Quaternion.identity).GetComponent<IItem>();
        MapManager.instance.GetTileInfo(cell_index).AddItem(i_item);
    }

    public static Item random_item
    {
        get
        {
            Dictionary<Item, int> item_stock = new Dictionary<Item, int>()
            {
                {Item.bubble, 10},
                {Item.roller, 10},
                {Item.fluid, 10},
                {Item.none, 10}
            };

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
}
