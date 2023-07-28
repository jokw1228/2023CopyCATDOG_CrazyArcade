using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item { none, bubble, roller, fluid, needle, turtle, pirate, wind, laser, dash }

public interface IItem
{
    void OnGetItem(Player player);
    void Remove();
}
