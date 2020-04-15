using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S;       // Singleton Pattern

    [Header("Set In Inspector")]
    // These fields control the movement of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;
    
    // This variable holds a reference to the last triggering GameObject
    private GameObject lastTriggerGO = null;        // fast moving gameobject can cause multiple trigger events

    void Awake () {
        if (S == null) {
            S=this;             // Set the singleton
        } else {
            Debug.LogError("Hero.Awake() - Attempted to assign a second Hero.S!");
        }
    
    }
  
    // Update is called once per frame
    void Update()
    {
        // Pull in info from the Input Class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // Change the transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        // Rotate the ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler(yAxis*pitchMult, xAxis * rollMult, 0);

        // Allow the ship to fire
        if (Input.GetKeyDown(KeyCode.Space)) {
            TempFire();
        }
    }

    void TempFire() {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.up * projectileSpeed;
    }

    void OnTriggerEnter(Collider other) {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        //print ("Triggered: " + go.name);

        // Make sure its not the same triggering gameobject as last time
        if (go == lastTriggerGO) {
            return;
        }
        lastTriggerGO = go;

        if (go.tag == "Enemy") {    // If the shield was triggered by an enemy
            shieldLevel--;
            Destroy(go);
        } else {
            print ("Triggered by a non-enemy: " + go.name);
        }
    }

    public float shieldLevel {
        get {
            return(_shieldLevel);
        }
        set {
            _shieldLevel = Mathf.Min(value, 4);
            // if the shield is going to be set to less than zero
            if (value<0) {
                Destroy(this.gameObject);
                // Tell Main.S to restart the game after a delay
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }
}
