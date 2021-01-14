using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelSwitcherScript : MonoBehaviour
{

    public GameObject wintext, alerttext;
    public int sceneIndex;
    public float waittime;
    private bool levelCleared;
    // Start is called before the first frame update

    private void Start() {
        levelCleared = false;
        wintext.SetActive(false);
        alerttext.SetActive(false);
    }

    private void Update() {
        if(levelCleared && Input.GetKeyDown(KeyCode.Return)){
            SceneManager.LoadScene(sceneIndex);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            Debug.Log("Level Finished");
            wintext.SetActive(true);
            StartCoroutine(switchLevelAfterSeconds(waittime));

        }
    }

    IEnumerator switchLevelAfterSeconds(float seconds){
        yield return new WaitForSecondsRealtime(seconds);
        alerttext.SetActive(true);
        levelCleared = true;
    }

}
