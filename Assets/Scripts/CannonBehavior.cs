using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehavior : MonoBehaviour
{

    public GameObject projectileSource1;
    public GameObject camera;
    private GameObject newProjectile;
    ProjectileBehavior behavior;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject fire(int ptype) {
        if (newProjectile==null){
            float scle = transform.localScale.x;
            newProjectile = Instantiate(projectileSource1,transform.position,transform.rotation);
            newProjectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.left*40);
            behavior = newProjectile.GetComponent<ProjectileBehavior>();
            behavior.player = transform.parent.gameObject;
            behavior.setProjectileType(ptype);

            camera.GetComponent<CameraBehavior>().followObject(newProjectile);
        } else {
            behavior.Action();
        }
        return newProjectile;
    }
}
