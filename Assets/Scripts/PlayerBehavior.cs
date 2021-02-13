using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public GameObject cannon;
    public Rigidbody projectile1;
    public TMP_Text text;
    private float playerZDistanceFromCamera = 20;
    private float cameraYMax = 20f;
    private float cameraYMin = -15f;
    private float mouseXMin = -10f;
    private float mouseXMax = 26.5f;
    private int score = 0;
    private int shots = 0;
    private bool readyToFire = true;
    public int projectileType;
    private List<GameObject> activeTrailMarks = new List<GameObject>();
    public int level;
    private bool gameOver=false;
    private int dScore = 10;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCameraDistance();
        if(level==2) score = PlayerPrefs.GetInt("score", 0);
        text.text = "Score: " + score + "\n+10";  
        Debug.Log("Level: " + level);
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToFire) {
            UpdateCameraDistance();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition+new Vector3(0,0,playerZDistanceFromCamera));
            bool inBounds = mousePos.y<cameraYMax && mousePos.y>cameraYMin && mousePos.x < mouseXMax && mousePos.x > mouseXMin;
            if (inBounds) {
                Vector3 direction = mousePos + cannon.transform.position;
                float angle = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
                cannon.transform.rotation=Quaternion.AngleAxis(angle,Vector3.back);

            }
        }

        if (Input.GetMouseButtonDown(0)) {
            RemoveTrail();
            cannon.transform.GetComponent<CannonBehavior>().fire(projectileType);
            shots++;
        }
    }

    public void addTrail(List<GameObject> newTrail) {
        activeTrailMarks.AddRange(newTrail);
    }

    private void RemoveTrail() {
        foreach(GameObject mark in activeTrailMarks) Destroy(mark);
        activeTrailMarks.Clear();
    }

    public void addScore() {
        if (!gameOver) {
        score+=dScore;
        dScore=10-shots;
        text.text = "Score: " + score+"\n" + (dScore<0? "" + dScore : "+" + dScore);
        //Debug.Log(score);
        }
    }

    public void miss() {
        if (!gameOver) {
            dScore=10-shots;
            text.text = "Score: " + score+"\n" + (dScore<0? "" + dScore : "+" + dScore);
            //Debug.Log(score);
        }
    }

    public void targetGotGot() {
        Debug.Log("gotem");
        if (level==1) {
            PlayerPrefs.SetInt("score", score);
            SceneManager.LoadScene("Level2");
        }
        else {
            text.text = "Final Score: " + score+"\n";
            gameOver=true;
            PlayerPrefs.SetInt("score", 0);
        }
    }

    private void UpdateCameraDistance() {
        float distance = Vector3.Distance(Camera.main.transform.position,cannon.transform.position);
        playerZDistanceFromCamera = Mathf.Abs(distance);
    }

}
