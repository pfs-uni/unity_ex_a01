using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // A static field accessible by code anywhere
    static public bool goalMet = false;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void OnTriggerEnter (Collider other) {
        // When the trigger is hit by something
        // Check to see if it's a projectile
        if (other.gameObject.tag == "Projectile") {
            // If yes, set goalMet to true
            Goal.goalMet = true;
            // Also set the alpha of the color to a higher opacity
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }
    }
    
}
