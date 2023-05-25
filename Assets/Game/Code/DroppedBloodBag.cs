using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedBloodBag : MonoBehaviour
{   

    public int hpUp = 10;
    private void OnCollisionEnter(Collision collision){
        // Check if the player collided with the weapon
        if(collision.gameObject.tag == "Player"){
            // Add hp to players
            RobotPlayer.GetInstance().addHp(hpUp);

            // Remove dropped weapon models
            Destroy(this.gameObject);
        }
    }
}
