using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public MapManager.Direction direction;

    public float wind_velocity;
    public float wind_force;

    Rigidbody2D rigidbody2d;

    private void Start()
    {
        Debug.Log("wind generated");

        rigidbody2d = GetComponent<Rigidbody2D>();
        
        switch (direction)
        {
            case MapManager.Direction.up:
                transform.position += Vector3.up;
                rigidbody2d.velocity = Vector2.up * wind_velocity;
                break;

            case MapManager.Direction.right:
                transform.position += Vector3.right;
                rigidbody2d.velocity = Vector2.right * wind_velocity;
                break;

            case MapManager.Direction.down:
                transform.position += Vector3.down;
                rigidbody2d.velocity = Vector2.down * wind_velocity;
                break;

            case MapManager.Direction.left:
                transform.position += Vector3.left;
                rigidbody2d.velocity = Vector2.left * wind_velocity;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MapManager.instance.GetClosestTileInfo(transform.position).CheckState((TileInfo.State)( (int)TileInfo.State.wall + (int)TileInfo.State.box )))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        Rigidbody2D rigidbody2d = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rigidbody2d == null)
            return;
        if ((collision.transform.position - transform.position).magnitude > 0.5f)
            return;

        switch (direction)
        {
            case MapManager.Direction.up:
                rigidbody2d.AddForce(Vector2.up * wind_force, ForceMode2D.Impulse);
                break;
            case MapManager.Direction.right:
                rigidbody2d.AddForce(Vector2.right * wind_force, ForceMode2D.Impulse);
                break;
            case MapManager.Direction.down:
                rigidbody2d.AddForce(Vector2.down * wind_force, ForceMode2D.Impulse);
                break;
            case MapManager.Direction.left:
                rigidbody2d.AddForce(Vector2.left * wind_force, ForceMode2D.Impulse);
                break;
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("fuck");
            Destroy (gameObject);
        }
    }
}
