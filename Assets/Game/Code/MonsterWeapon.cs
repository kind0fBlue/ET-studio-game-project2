using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    // Damage value
    public int power = 10;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            other.GetComponent<BaseRobot>().GetAttack(power);
        }
    }

    // private void OnCollisionEnter(Collision collision){
    //     if (collision.gameObject.tag == "Player"){
    //         collision.gameObject.GetComponent<BaseRobot>().GetAttack(power);
    //     }
    // }

}
