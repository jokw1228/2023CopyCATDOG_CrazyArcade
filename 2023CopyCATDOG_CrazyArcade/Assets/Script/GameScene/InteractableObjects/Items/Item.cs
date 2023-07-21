using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item { none, bubble, roller, fluid, needle, turtle, pirate, bush }

public interface IItem
{
    void OnGetItem(Player1 player);
    void Remove();
}
