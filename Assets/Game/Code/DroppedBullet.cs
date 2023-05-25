using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedBullet : MonoBehaviour
{   
    public int bulletNumUp = 10;
    private void OnCollisionEnter(Collision collision){
        // Check if the player collided with the weapon
        if(collision.gameObject.tag == "Player"){
            // Add bullet to players
            RobotPlayer.GetInstance().addBulletNum(bulletNumUp);

            // Remove dropped weapon models
            Destroy(this.gameObject);
        }
    }
}
