using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseRobot : MonoBehaviour
{   
    // Set the Hp value of Robot
    public int hp = 100;

    // Set 4 names for sound when getting damaged
    private string[] names = {"GetDamaged1", "GetDamaged2", "GetDamaged3", "GetDamaged4"};

    // Check if the robot is alive
    public bool IsAlive(){
        return hp > 0;
    }

    // The robot is attacked and loses hp
    public void GetAttack(int damage){
        hp = hp - damage;

        System.Random rd = new System.Random();
        FindObjectOfType<AudioManager>().Play(names[rd.Next(4)]);
        FindObjectOfType<AudioManager>().Play("GetDamagedIron");

        // If the robot dies
        if (!IsAlive()){
            Die();
        }
    }

    // If the player dies, remove the player
    public virtual void Die(){
        Destroy(this.gameObject);
        SceneManager.LoadScene(2);
    }

    public virtual void openFire(){
        
    }
}
