using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickeffect : MonoBehaviour
{
    
    public GameObject clickEffect;

    // Update is called once per frame
    void Update()
    {   
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            Instantiate(clickEffect, mouseWorldPosition, Quaternion.identity);
        }
    }
}
