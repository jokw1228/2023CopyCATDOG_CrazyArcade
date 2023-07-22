using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public int Map;
    // Start is called before the first frame update
    private void Start()
    {
        switch (Map)
        {
            case 0:
                Instantiate(GameManager.Inst.currentPlayer1Character.prefab, new Vector3(-5.5f, -2.5f, 0), Quaternion.identity);
                Instantiate(GameManager.Inst.currentPlayer2Character.prefab, new Vector3(5.5f, 2.5f, 0), Quaternion.identity);
                break;
            case 1:
                Instantiate(GameManager.Inst.currentPlayer1Character.prefab, new Vector3(-5.5f, -2.5f, 0), Quaternion.identity);
                Instantiate(GameManager.Inst.currentPlayer2Character.prefab, new Vector3(5.5f, 2.5f, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(GameManager.Inst.currentPlayer1Character.prefab, new Vector3(-5.5f, -2.5f, 0), Quaternion.identity);
                Instantiate(GameManager.Inst.currentPlayer2Character.prefab, new Vector3(5.5f, 2.5f, 0), Quaternion.identity);
                break;
        }

    }
}
