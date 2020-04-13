using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;   // The static Point Of Interest

    [Header("Set In Inspector")]
    public float easing=0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ;              // The desired z pos of the camera


    void Awake() {
        camZ = this.transform.position.z;
    }

    void FixedUpdate(){
        /* if (POI == null) return;

        // Get the position of the POI
        Vector3 destination = POI.transform.position; */

        Vector3 destination;
        // If there is no POI, return to P:[0,0,0]
        if (POI == null) {
            destination = Vector3.zero;
        } else {
            // Get the position of the POI
            destination = POI.transform.position;

            // If POI is a Projectile, check to see if its at rest
            if (POI.tag == "Projectile") {
                // If it is sleeping (ie not moving)
                if (POI.GetComponent<Rigidbody>().IsSleeping()){
                    // Return to default view
                    POI = null;
                    // in the next update
                    return;
                } 
            }
        }

        // Limit the X & Y to minimum values;
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        // Interpolate from the current Camera position toward the destination
        destination = Vector3.Lerp(transform.position,destination, easing);

        // Force destination.z to be camZ to keep the camera far enough away
        destination.z = camZ;
        // Set the camera to the destination
        transform.position = destination;

        // Set the orthographicSize of the Camera to keep the Ground in view
        Camera.main.orthographicSize = destination.y + 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
