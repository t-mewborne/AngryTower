using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{

    public GameObject player;
    public GameObject trail;

    private List<GameObject> childs = new List<GameObject>();

    public float lifetime = 5f;

    public Rigidbody rb;
    private PlayerBehavior playerScript;
    private int ProjectileType = 1;
    private bool actionUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        //rb = gameObject.GetComponent<Ridigdbody>();
        playerScript=player.GetComponent<PlayerBehavior>();
        Invoke("Suicide",lifetime);
        InvokeRepeating("LeaveTrail",0f,0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Suicide() {
        playerScript.addTrail(childs);
        CancelInvoke();
        Destroy(gameObject);
    }

    private void LeaveTrail() {
        GameObject childOne = Instantiate(trail);
        childOne.transform.position=transform.position;
        childs.Add(childOne);
    }

    public void setProjectileType(int i) {
        if (i!=1 && i!=2) Debug.Log("Invalid projectile type: " + i);
        else ProjectileType = i;
    }

    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag=="Castle"){
            playerScript.addScore();
            //collision.gameObject.GetComponent<CastleObjectBehavior>().kill();
            Destroy(collision.gameObject);
            Suicide();
        } else if (collision.gameObject.tag=="Ground"){
            playerScript.miss();
            Suicide();
        } else if (collision.gameObject.tag=="Target"){
            Destroy(collision.gameObject);
            playerScript.targetGotGot();
            Suicide();
        }

    }

    public void Action() {
        if(!actionUsed) {
            if (ProjectileType==1) {
                GameObject childOne = Instantiate(transform.gameObject,transform.position,transform.rotation);
                GameObject childTwo = Instantiate(transform.gameObject,transform.position,transform.rotation);

                childOne.GetComponent<Rigidbody>().velocity = new Vector3(rb.velocity.x,rb.velocity.y,10);
                childTwo.GetComponent<Rigidbody>().velocity = new Vector3(rb.velocity.x,rb.velocity.y,-10);
            } else if (ProjectileType==2) {
                GameObject childOne = Instantiate(transform.gameObject,transform.position,transform.rotation);
                GameObject childTwo = Instantiate(transform.gameObject,transform.position,transform.rotation);

                childOne.GetComponent<Rigidbody>().velocity = new Vector3(rb.velocity.x,rb.velocity.y+10,rb.velocity.z);
                childTwo.GetComponent<Rigidbody>().velocity = new Vector3(rb.velocity.x,rb.velocity.y-10,rb.velocity.z);
            }
        actionUsed=true;
        }
    }
}
