using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Damage per bullet
    public int power = 5;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Monster"){
            other.GetComponent<BaseMonster>().GetDamage(power);
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag != "Sight"){
            Destroy(this.gameObject);
        }

    }
}
