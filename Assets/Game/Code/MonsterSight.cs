using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSight : MonoBehaviour
{   
    // Viewable range
    public float fov = 110f;

    // Whether the player is in view
    public bool playerIsInSight;

    // Last seen player's position
    public Vector3 playerLastSightPos;

    public static Vector3 resetPosition = Vector3.back;

    // Game Object of player
    private GameObject player;

    // Collider of view sphere
    private SphereCollider sphereCol;

    // Start is called before the first frame update
    void Start()
    {
        // Get collider of view sphere
        sphereCol = GetComponent<SphereCollider>();

        // Get game object of player
        player = GameObject.FindGameObjectWithTag("Player");

        // Get last seen player's position
        playerLastSightPos = resetPosition;
    }

    private void OnTriggerStay(Collider other){
        if (other.gameObject == player){
            playerIsInSight = false;
            
            // Direction between player and monster
            Vector3 direction = other.transform.position - transform.position;

            // The angle between the monster's direction and the direction vector
            float angle = Vector3.Angle(direction,transform.forward);

            // Check if the field of view is exceeded
            if (angle < fov * 0.5f){
                RaycastHit hit;
                
                // Check if there is any obstacle between the monster and the player
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sphereCol.radius)){
                    // If the collision object is the player, record the player's position
                    playerIsInSight = true;
                    playerLastSightPos = player.transform.position;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.gameObject == player){
            playerIsInSight = false;
        }
    }
}
