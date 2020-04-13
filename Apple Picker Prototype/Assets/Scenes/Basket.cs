using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    [Header("Set Dynamically")]
    public Text scoreGT;

    // Start is called before the first frame update
    void Start()
    {
        // Find a reference to the ScoreCounter GameObject
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        // Get the text component of that GameObject
        scoreGT = scoreGO.GetComponent<Text>();
        // Set the starting number of points to 0
        scoreGT.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;       // Get the current position of the mouse
        mousePos2D.z = -Camera.main.transform.position.z;   // The cameras z position sets how far to push the mouse into 3D
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);  // Convert the point from 2D screen space to 3D game space
        Vector3 pos = this.transform.position;  // Move the x position of this basket to the x position of the mouse
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter (Collision col1) {
        // Find out what hit the basket
        GameObject collidedWith = col1.gameObject;
        if (collidedWith.tag == "Apple") {
            Destroy(collidedWith);
        }

        // Parse the text of scoreGT into an int
        int score = int.Parse(scoreGT.text);
        // Add points for catching the apple
        score += 10;
        // Convert the score back into a string and display it
        scoreGT.text = score.ToString();

        // Track the high score
        if (score > HighScore.score) {
            HighScore.score = score;
        }
    }
}
