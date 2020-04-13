using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot S;

    // Fields set in the Unity Inspector pane
    [Header("Set in Inspector:")]
    
    public GameObject prefabProjectile;
    public float velocityMult = 8f;
    

    // Fields set dynamically (at runtime)
    [Header("Set Dynamically")]

    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    private Rigidbody projectileRigidBody;

    static public Vector3 LAUNCH_POS {
        get {
            if (S ==null) return Vector3.zero;
            return S.launchPos;
        }
    }

    void Awake() {
        S = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);

        launchPos = launchPointTrans.position;
    }

    void OnMouseEnter() {
        //print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit() {
        //print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown() {
        // Player presses mouse button while over the slingshot
        aimingMode = true;

        // Instantiate a Projectile
        projectile = Instantiate(prefabProjectile) as GameObject;

        // Start it at the launchpoint
        projectile.transform.position = launchPos;

        // Set it to isKinematic for now
        //projectile.GetComponent<Rigidbody>().isKinematic = true; //old placeholder code...

        projectileRigidBody = projectile.GetComponent<Rigidbody>();
        //projectileRigidBody.isKinematic = true;

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If Slingshot is not in aimingMode, don't run this code
        if (!aimingMode) return;
        
        // Get the current mouse position in 2D screen coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Find the delta from the lauchPos to the mousePos3D
        Vector3 mouseDelta = mousePos3D-launchPos;

        // Limit the mouseDelta to the radius of the Slingshot SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude){
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        // Move the projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0)) {
            // The mouse has been released
            aimingMode = false;
            projectileRigidBody.isKinematic = false;
            projectileRigidBody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
        }
    }


}
