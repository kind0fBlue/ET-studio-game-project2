using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedWeapon : MonoBehaviour
{   
    // Identify which weapon the dropped weapon is
    public GameObject dropedWeapon;
    
    private void OnCollisionEnter(Collision collision){
        // Check if the player collided with the weapon
        if(collision.gameObject.tag == "Player"){
            // Add collision weapons to players
            RobotPlayer.GetInstance().addWeapon(dropedWeapon);

            // Remove dropped weapon models
            Destroy(this.gameObject);
        }
    }
}
