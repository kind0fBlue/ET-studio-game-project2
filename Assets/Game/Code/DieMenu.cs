using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieMenu: MonoBehaviour {


    public void backMenu() {
        SceneManager.LoadScene(0);
    }

    public void Update() {
        FindObjectOfType<AudioManager>().Play("BGM1");
    }
}