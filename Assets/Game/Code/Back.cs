using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Monster").Length == 0){
            SceneManager.LoadScene(2);
        }
    }
}
