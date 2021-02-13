using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{

    private Vector3 gameplayPosition = new Vector3 (0f,8.31f,64.42f);
    private Vector3 gameplayRotation = new Vector3 (0,180,0);
    private GameObject objectToFollow;
    private int gamePlaySpeed = 12;

    private Vector3 targetRotation;
    private Vector3 targetPosition;
    private int speed;

    // Start is called before the first frame update
    void Start()
    {
        targetRotation=gameplayRotation;
        targetPosition=gameplayPosition;
        speed=gamePlaySpeed;


    }

    // Update is called once per frame
    void Update()
    {
        

        if (objectToFollow!=null) {
            transform.LookAt(objectToFollow.transform);
            targetPosition = new Vector3 (objectToFollow.transform.position.x-5,objectToFollow.transform.position.y,objectToFollow.transform.position.z);
            //transform.position
        } else {
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, targetRotation, Time.deltaTime);
            targetPosition = gameplayPosition;
        }

        transform.position = Vector3.MoveTowards(transform.position,targetPosition,speed*Time.deltaTime);
    }

    public void followObject(GameObject obj) {
        objectToFollow=obj;
    }




}
