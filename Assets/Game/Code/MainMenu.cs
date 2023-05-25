using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Start() {
        mouseCursor.instance.ActivateNormalCursor();
    }

    public void Update() {
        FindObjectOfType<AudioManager>().Play("BGM1");
    }

    public void RandomMap() {
        SceneManager.LoadScene(3);
    }
}