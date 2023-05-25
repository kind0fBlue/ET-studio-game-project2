using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : BaseMonster
{


    private NavMeshAgent nav;
    private Animator animator;
    
    // The sight of monster
    private MonsterSight monsterSight;

    // The sight of attack
    private MonsterSight attackSight;

    // Current weapon
    //public MonsterWeaponBase currentWeapon;

    // Game object of player
    private GameObject player;

    // Patrolling speed
    //public float patrolSpeed = 10;

    // The index of the current path
    //public int pathPointIndex;
    
    // Waiting time after patrolling to path point
    //public float waitTime = 1f;

    // Transform of patrolling path points
    //public Transform pathPoints;
   // private Transform pathPoints;
    
    // Timer after patrolling to path point
    //private float timer;

    // Chase speed
    public float chaseSpeed = 10;

    // Chase waiting time
    public float chaseWaitTime = 1f;

    // Chase timer
    private float chaseTimer;

    private void Awake(){
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        monsterSight = transform.Find("MonsterSight").GetComponent<MonsterSight>();
        attackSight = transform.Find("AttackSight").GetComponent<MonsterSight>();
    }


    public void Attack(){
        animator.SetBool("Shoot", true);
        FindObjectOfType<AudioManager>().Play("MonsterAttack");
    }

    public override void startAttack()
    {
        base.startAttack();
    }

    public void Chasing(){
        nav.isStopped = false;
        nav.speed = chaseSpeed;
        Vector3 sightDeltaPosition = monsterSight.playerLastSightPos - transform.position;
        if (sightDeltaPosition.sqrMagnitude > 4){
            nav.destination = monsterSight.playerLastSightPos;
            if (nav.remainingDistance <= nav.stoppingDistance){
                chaseTimer += Time.deltaTime;
                if (chaseTimer >= chaseWaitTime){
                    chaseTimer = 0;
                    nav.speed = 0;
                }
            } else{
                chaseTimer = 0;
            }
        }
    }

//     public void Patrol(){
//         nav.isStopped = false;
//         nav.speed = patrolSpeed;

//         // Check if the current patrol path point is reached
//         if (nav.remainingDistance <= nav.stoppingDistance){
//             timer += Time.deltaTime;
//             if (timer >= waitTime){
//                 if (pathPointIndex == pathPoints.childCount - 1){
//                     pathPointIndex = 0;
//                 } else{
//                     pathPointIndex++;
//                 }
//                 timer = 0;
//             }
//         } else{
//             timer = 0;
//         }
        
// //        nav.destination = pathPoints.GetChild(pathPointIndex).position;
//     }

    // Update is called once per frame
    void Update()
    {   
        // Check if the player is dead, if dead, return directly
        if(!RobotPlayer.GetInstance().IsAlive()){
            return;
        }

        // If the player is within the attack range of the monster, enter the attack state
        if (attackSight.playerIsInSight){
            // attack
            Attack();
        } else if (monsterSight.playerIsInSight){
            // If the player is within sight of the monster, chase the player
            Chasing();
        } else {
            // Normal patrol
            //Patrol();
        }

        // Toggle animation state
        animator.SetFloat("Speed", nav.speed / chaseSpeed);

        Init_Rotate();
        Rotate_Func();
    }


    private Quaternion raw_rotation;
  
    private Quaternion lookat_rotation;
 
    private float per_second_rotate = 1080.0f;
 
    float lerp_speed = 0.0f;
    // lerp
    float lerp_tm = 0.0f;



    void Init_Rotate()
    {
        raw_rotation = transform.rotation;
        transform.LookAt(player.transform.localPosition);
        lookat_rotation = transform.localRotation;
        transform.rotation = raw_rotation;
        float rotate_angle = Quaternion.Angle(raw_rotation, lookat_rotation);
        lerp_speed = per_second_rotate / rotate_angle;
        // Debug.Log("Angle:" + rotate_angle.ToString() + " speed:" + lerp_speed.ToString());
        lerp_tm = 0.0f;
    }

    void Rotate_Func()
    {
        lerp_tm += Time.deltaTime * lerp_speed;
        transform.rotation = Quaternion.Lerp(raw_rotation, lookat_rotation, lerp_tm);
        if (lerp_tm >= 1)
        {
            transform.rotation = lookat_rotation;
        }
    }
}
