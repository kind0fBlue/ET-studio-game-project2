using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBS : MonoBehaviour
{
    private Transform camera_T;

    private Transform player_T;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {   
        camera_T = gameObject.GetComponent<Transform>();
        player_T = GameObject.Find("Player").GetComponent<Transform>();
        // The relative position of the camera relative to the player
        offset = new Vector3(0f,34.641f,-20f);
    }

    // Update is called once per frame
    void Update()
    {   
        if (player_T != null) {
            camera_T.position = Vector3.Lerp(camera_T.position, player_T.position + offset, Time.deltaTime * 5);
        }
        
    }
}
