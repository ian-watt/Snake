using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject applePrefab;
    public Snake snake;
    public GameObject currentApple;
    public float gameSpeed;

    public Vector2 bounds;
    public float tile;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnApple();  
    }

    private void Update()
    {
       if((Vector2)snake.transform.position == (Vector2)currentApple.transform.position)
        {
            Destroy(currentApple);
            snake.SnakeEmbiggen();
            SpawnApple();
            //make snake big
        }
    }

    private void SpawnApple()
    {
        currentApple = Instantiate(applePrefab);
        currentApple.transform.position = Vector3Int.FloorToInt(new Vector3(Random.Range(0, bounds.x), Random.Range(0, bounds.y), -1));
    }

    public void GameOver()
    {
        snake.enabled = false;
        //Display game over text
    }

}
