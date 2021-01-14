using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bodyUpturnCheck : MonoBehaviour
{
    public float distanceToUpturnCheck;
    public float multiplier;
    public LayerMask groundLayer;
    public GameObject head;
    public GameObject canvas;
    public Camera mainCamera;

    void Start() {
        canvas.SetActive(false);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)){
            SceneManager.LoadScene(0);
        }
        if(upturned()) {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            Debug.Log("You Ded Son");
            StartCoroutine(reloadSceneAfterSeconds(3));
            StartCoroutine(showGameOverText(1));
        }
    }
    // Start is called before the first frame update
    public bool upturned() {
        return Physics2D.OverlapCircle(head.transform.position, distanceToUpturnCheck, groundLayer);
    }

    public bool checkIfOnGround() {
        return Physics2D.OverlapCircle(transform.position, distanceToUpturnCheck * multiplier, groundLayer);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(head.transform.position, distanceToUpturnCheck);
        Gizmos.DrawWireSphere(transform.position, distanceToUpturnCheck * multiplier);
    }

    IEnumerator reloadSceneAfterSeconds(float time) {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1f; 
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator showGameOverText(float time) {
        yield return new WaitForSecondsRealtime(time);
        canvas.SetActive(true);
    }
}
