using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Building"){
            //other.GetComponent<MapManger>().SetIsTrigger();
            Destroy(this.gameObject);
            Debug.Log("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");
        }
    }
}
