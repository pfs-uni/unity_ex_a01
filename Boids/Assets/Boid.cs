using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [Header("Set Dynamically")]
    public Rigidbody rigid;

    private Neighborhood neighborhood;

    // User this for Initialisation
    void Awake() {
        neighborhood = GetComponent<Neighborhood>();
        rigid = GetComponent<Rigidbody>();

        // Set a random initial position
        pos = Random.insideUnitSphere * Spawner.S.spawnRadius;

        // Set a random initial velocity
        Vector3 vel = Random.onUnitSphere * Spawner.S.velocity;
        rigid.velocity = vel;

        LookAhead();

        // Give the boid a random colour, but make sure its not too dark
        Color randColor = Color.black;
        while( randColor.r + randColor.g + randColor.b < 1.0f) {
            randColor = new Color(Random.value, Random.value, Random.value);
        }

        Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends) {
            r.material.color = randColor;
        }
        TrailRenderer tRend = GetComponent<TrailRenderer>();
        tRend.material.SetColor("_TintColor", randColor);
    }

    void LookAhead() {
        transform.LookAt(pos+rigid.velocity);
    }

    public Vector3 pos {
        get {return transform.position;}
        set {transform.position = value;}
    }

    // FixedUpdate is called once per physics update
    void FixedUpdate() {
        Vector3 vel = rigid.velocity;
        Spawner spn = Spawner.S;

        // Collision Avoidance - Avoid neighbors who are two close
        Vector3 velAvoid = Vector3.zero;
        Vector3 tooClosePos = neighborhood.avgClosePos;

        // If the response is vector.zero then no need to react
        if (tooClosePos != Vector3.zero) {
            velAvoid = pos - tooClosePos;
            velAvoid.Normalize();
            velAvoid *= spn.velocity;
        }

        // Velocity Matching - try to match velocity with neighbors
        Vector3 velAlign = neighborhood.avgVel;

        // Only do more if the velAlign is not Vector3.zero
        if (velAlign != Vector3.zero) {
            // Really interested in direction, so normalise velocity
            velAlign.Normalize();

            // And then set ot the speed we chose
            velAlign *= spn.velocity;
        }

        // Flock Centering - move towards the centre of of the local neighbors
        Vector3 velCenter = neighborhood.avgPos;
        if (velCenter != Vector3.zero) {
            velCenter -= transform.position;
            velCenter.Normalize();
            velCenter *= spn.velocity;
        }

        // Attraction - Move towards the Attractor
        Vector3 delta = Attractor.POS - pos;

        // Check if we're attacted or avoiding the attractor
        bool attracted = (delta.magnitude > spn.attractPushDist);
        Vector3 velAttract = delta.normalized * spn.velocity;

        // Apply all the velocities
        float fdt = Time.fixedDeltaTime;
        
        if (velAvoid != Vector3.zero) {
            vel = Vector3.Lerp(vel, velAvoid, spn.collAvoid*fdt);
        } else {
            if (velAlign != Vector3.zero) {
                vel = Vector3.Lerp(vel, velAlign, spn.velMatching*fdt);
            }
            if (velCenter != Vector3.zero) {
                vel = Vector3.Lerp(vel, velAlign, spn.flockCentering*fdt);
            }
            if (velAttract != Vector3.zero) {
                if (attracted) {
                    vel = Vector3.Lerp(vel, velAttract, spn.attractPull*fdt);
                } else {
                    vel = Vector3.Lerp(vel, -velAttract, spn.attractPush*fdt);
                }
            }
        }
        
        // Set vel to the velocity set on the Spawner Singleton
        vel = vel.normalized * spn.velocity;

        // Assign this to the rigidbody
        rigid.velocity = vel;

        // Look in the direction of the new velocity
        LookAhead();
    }

    // Collision Avoidance - avoid neighbors who are two close
    

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
