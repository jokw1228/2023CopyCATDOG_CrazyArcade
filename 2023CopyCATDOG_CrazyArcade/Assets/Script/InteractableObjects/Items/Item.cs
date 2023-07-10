using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item { bubble, roller, fluid }

public interface IItem
{
    void OnGetItem(Player1 player);
    void Remove();
}
