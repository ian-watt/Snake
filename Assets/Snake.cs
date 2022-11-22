using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Snake : MonoBehaviour
{
    public SpriteRenderer snakeRenderer;
    public GameObject bodyPrefab;
    public List<GameObject> bodyParts = new List<GameObject>();
    private Vector2 direction;
    private float timeSinceMove;
    private Vector2 currentDirection;
    private void Start()
    {
        bodyParts.Clear();
        bodyParts.Add(gameObject);
        transform.position = Vector3Int.FloorToInt(GameManager.instance.bounds / 2);
        direction = new Vector2(0, 1);
    }

    private void Update()
    {
        timeSinceMove += Time.deltaTime;

        if (timeSinceMove > GameManager.instance.gameSpeed)
        {
            for (int i = bodyParts.Count - 1; i >= 1; i--)
            {
                bodyParts[i].transform.position = bodyParts[i - 1].transform.position;
            }
            currentDirection = direction;
            transform.position += (Vector3)direction * GameManager.instance.tile;
            Vector3 p = transform.position;
            if(p.x >= GameManager.instance.bounds.x || p.y >= GameManager.instance.bounds.y || p.x < 0 || p.y < 0)
            {
                transform.position = new Vector3(5, 5, -1);
                GameManager.instance.GameOver();
                Debug.Log("Out of bounds, goodbye");
            }

            for(int i = 1; i < bodyParts.Count; i++)
            {
                if(transform.position == bodyParts[i].transform.position)
                {
                    Debug.Log("Died");
                    GameManager.instance.GameOver();
                }
            }
            timeSinceMove = 0;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentDirection != Vector2.left)
            {
                direction = new Vector2(1, 0);

            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentDirection != Vector2.down)
            {
                direction = new Vector2(0, 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentDirection != Vector2.up)
            {
                direction = new Vector2(0, -1);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentDirection != Vector2.right)
            {
                direction = new Vector2(-1, 0);
            }
        }

    }

    public void SnakeEmbiggen()
    {
        GameObject newPart = Instantiate(bodyPrefab);
        newPart.transform.position = bodyParts[bodyParts.Count - 1].transform.position;
        bodyParts.Add(newPart);
    }

}
