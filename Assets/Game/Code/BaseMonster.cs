using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : MonoBehaviour
{   
    private static BaseMonster instance;

    // Get instance
    public static BaseMonster getInstance(){
        return instance;
    }

    [SerializeField] private HealthBar _healthBar;
    // Set the Hp value of monster
    public float hp = 10;
    public float currentHealth = 10;

    public GameObject particle;

    public GameObject[] droppedItem;
    private RobotPlayer player;

    private int itemDropProbability;

    // Check if the monster is alive
    public bool IsAlive(){
        return currentHealth > 0;
    }

    // The monster is attacked and loses hp
    public void GetDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        // If the monster dies
        if (!IsAlive())
        {
            Die();
        }
        else
        {
            _healthBar.UpdateHealthBar(hp, currentHealth);
        }
    }

    // If the monster dies, remove the monster
    public virtual void Die(){

        itemDropProbability = Random.Range(0,100);

        transform.rotation = transform.rotation * Quaternion.AngleAxis(180, Vector3.up);
        GameObject obj = Instantiate(particle, transform.position, transform.rotation);
        Destroy(this.gameObject);
        UI.getInstance().updateScoreUI();
        FindObjectOfType<AudioManager>().Play("MonsterExplode");

        if(itemDropProbability < 10){
            Instantiate(droppedItem[0],transform.position,Quaternion.identity);
        }else if (itemDropProbability >=10 && itemDropProbability <=40){
            Instantiate(droppedItem[1],transform.position,Quaternion.identity);
        }
    }

    public virtual void startAttack(){
        
    }

    public void HpUp(){
        hp = hp + 5;
    }
}
