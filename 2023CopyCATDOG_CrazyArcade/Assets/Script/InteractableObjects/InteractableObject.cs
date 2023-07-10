using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [HideInInspector] public TileInfo.State state;
    [HideInInspector] public Vector2Int cell_index;

    public InteractableObject() { }
    public InteractableObject(TileInfo.State state, Vector2Int cell_index)
    {
        this.state = state;
        this.cell_index = cell_index;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
