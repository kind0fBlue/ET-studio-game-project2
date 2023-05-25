using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New : MonoBehaviour
{   
    private int count = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {   
            Debug.Log(count);
            count++;
            if (count == 1) {
                Destroy(GameObject.Find("Image2"));
            }
            if (count == 2) {
                Destroy(GameObject.Find("Image1"));
            }
        }
    }
}
