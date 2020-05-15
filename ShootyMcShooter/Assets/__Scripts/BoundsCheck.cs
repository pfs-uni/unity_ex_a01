using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps a GameObject on screen
/// Note that this ONLY works for an orthographic Main Camera at [0,0,0]
/// </summary>

public class BoundsCheck : MonoBehaviour
{
    [Header("Set In Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Set Dynamically")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;

    void Awake() {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void LateUpdate() {
        Vector3 pos = transform.position;
        
        isOnScreen = true;
        offRight = offLeft = offUp = offDown = false;

        if (pos.x > camWidth - radius) {
            pos.x = camWidth -radius;
            //isOnScreen = false;
            offRight = true;

        }

        if (pos.x < -camWidth + radius) {
            pos.x = -camWidth + radius;
            //isOnScreen = false;
            offLeft = true;
        }

        if (pos.y > camHeight - radius) {
            pos.y = camHeight - radius;
            //isOnScreen = false;
            offUp = true;
        }

        if (pos.y < -camHeight + radius) {
            pos.y = -camHeight + radius;
            //isOnScreen = false;
            offDown = true;
        }

        isOnScreen = !(offRight || offLeft || offDown || offUp);
        if (keepOnScreen && !isOnScreen) {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
        }
        
    }

    void OnDrawGizmos () {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth*2, camHeight*2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
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
