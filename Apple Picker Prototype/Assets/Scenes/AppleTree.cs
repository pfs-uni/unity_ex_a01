using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject applePrefab;                  // Prefab for instatiating apples
    public float speed = 1f;                        // Speed at which the AppleTree moves
    public float leftAndRightEdge=10f;              // Distance where the AppleTree turns around
    public float chanceToChangeDirections = 0.1f;   // Chance that AppleTree will change directions
    public float secondsBetweenAppleDrops = 1f;     // Rate at which Apples will be instatiated

    // Start is called before the first frame update
    void Start()
    {
        // Dropping apples every second
        Invoke("DropApple", 2f);
        
    }

    void DropApple () {
        GameObject apple  = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops);
    }

    // Update is called once per frame
    void Update()
    {
        // Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing Directions

        if (pos.x < -leftAndRightEdge){
            speed = Mathf.Abs(speed);               // Move right
        } else if (pos.x > leftAndRightEdge){
            speed = -Mathf.Abs(speed);              // Move left
        }
    }

    void FixedUpdate () {
        if (Random.value < chanceToChangeDirections) {
            speed *= -1;                            // Change directions
        }
    }
    
}
