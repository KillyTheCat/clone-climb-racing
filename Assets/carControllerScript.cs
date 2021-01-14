using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class carControllerScript : MonoBehaviour
{
    public Rigidbody2D frontTire, backTire, carRigidBody;
    public GameObject carBody;
    public GameObject pauseCanvas;
    public float speed = 20f;
    public float carTorque = 10f, distanceToUpturnCheck = 3f;
    public bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(paused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            restartLevel();            
        }
    }

    void movement() {
        // float horAxis = Input.GetAxisRaw("Horizontal");
        float horAxis = 0;

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            horAxis = 1;
        }
        else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            horAxis = -1;
        }
        else {
            horAxis = 0;
        }

        frontTire.AddTorque(-horAxis * speed * Time.deltaTime);
        backTire.AddTorque(-horAxis * speed * Time.deltaTime);


        if(carBody.GetComponent<bodyUpturnCheck>().checkIfOnGround() == false){
            carRigidBody.AddTorque(-horAxis * carTorque * Time.deltaTime);
            Debug.Log("Can Roll");
        }
    }

    void restartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PauseGame() {
        Time.timeScale = 0.0f;
        pauseCanvas.SetActive(true);
        paused = true;
        
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
        paused = false;
    }
}
