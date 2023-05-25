using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    // Weapon icon
    public Sprite weaponIcon;

    // The muzzle position of the weapon
    public Transform muzzle_pos;

    // Bullet
    public GameObject bullet_obj;

    // Number of bullets
    public int bullet_num = 100;

    // The initial velocity of the bullet
    public int bullet_speed = 12;


    public void OpenFire(Vector3 hitPoint){
        if(bullet_num > 0){
            // If the number of bullets is greater than 0, generate bullets and set the initialization position to the muzzle
            // var bullet = GameObject.Instantiate(bullet_obj, muzzle_pos.position, Quaternion.identity);
            var bullet = GameObject.Instantiate(bullet_obj);
            bullet.transform.position = muzzle_pos.position;
            bullet.transform.localRotation = Quaternion.identity;
            
            var bullet_dir = (hitPoint - muzzle_pos.position).normalized;
            // bullet_dir.y = 0.1F;
            // bullet_dir.z -= 0.1F;
            Debug.Log(muzzle_pos.position);
            Debug.Log(transform.position);
            bullet.GetComponent<Rigidbody>().velocity = bullet_dir * bullet_speed;
            bullet_num--;
        }
    }

    public int getBulletNum(){
        return bullet_num;
    }
}
