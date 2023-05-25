using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPlayer : BaseRobot
{

    // Define a player
    static RobotPlayer instance;

    // Get info of the player
    public static RobotPlayer GetInstance(){
        return instance;
    }

    // Initialize a player
    private void Awake(){
        instance = this;
    }

    // Weapon list
    public List<WeaponBase> Weapons;

    //
    public Transform Hand;

    Vector3 verticalVector;

    public float walkSpeed = 10f;
    public float runSpeed = 15f;
    public float speed;
    public bool isRun;

    public float gravity = 9.8f;

    private Vector3 hitPoint;

    [Header("Key code set up")]
    [SerializeField] [Tooltip("Run")] private KeyCode runInputKey; // run key 
    [SerializeField] [Tooltip("Run")] private KeyCode runInputKeyAlter; // run alter

    private WeaponBase CurrentWeapon;
    private int CurrentWeaponIndex = 0;

    private Animator animator;
    private CharacterController cc;

    // Movement Status
    public bool isMove = false;
    private Vector3 prev = Vector3.zero;
    private Vector3 curr = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        CurrentWeapon = Weapons[CurrentWeaponIndex];

        runInputKey = KeyCode.LeftShift;
        runInputKeyAlter = KeyCode.RightShift;

        curr = instance.transform.position;
        prev = curr;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        curr = instance.transform.position;
        // Check if moved
        if (Vector3.Distance(curr, prev) > 0.005f) {
            isMove = true;
            FindObjectOfType<AudioManager>().Play("Walk");
        }
        prev = curr;

        // Follow the mouse direction
        var rotate = GetMousePoint();
        PointToTarget(rotate);
        
        // Open fire
        // if(Input.GetButton("Fire1")){
        //     Shoot();
        // }

        if(Input.GetMouseButton(0)){
            if(CurrentWeapon.getBulletNum() > 0){
                Shoot();
            }
            //Shoot();
        }

        // Switch weapon
        float f = Input.GetAxis("Mouse ScrollWheel");
        if(f > 0) {
            nextWeapon(1);
        } else if (f < 0){
            nextWeapon(-1);
        }

        // UI update
        UI.getInstance().updateHpUI(instance.hp);
        UI.getInstance().updateWeaponUI(CurrentWeapon.weaponIcon, CurrentWeapon.bullet_num);
    }

    public void Movement()
    {   
        var trans = Camera.main.transform;
        var forward = Vector3.ProjectOnPlane(trans.forward, Vector3.up);
        var right = Vector3.ProjectOnPlane(trans.right, Vector3.up);
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        var moveDirection = v * forward + h * right;


        // Speed adjustment: run or walk?
        isRun = (Input.GetKey(runInputKey) || Input.GetKey(runInputKeyAlter)) ? true : false;
        speed = isRun ? runSpeed : walkSpeed; //walk or run speed set up    

        verticalVector.y -= gravity * Time.deltaTime;
        cc.Move((moveDirection.normalized * speed + verticalVector) * Time.deltaTime);
        animator.SetFloat("Speed", cc.velocity.magnitude / speed);
    }

    // Next weapon
    public void nextWeapon(int s){
        var index = (CurrentWeaponIndex + s + Weapons.Count) % Weapons.Count;
        CurrentWeapon.gameObject.SetActive(false);
        CurrentWeapon = Weapons[index];
        CurrentWeapon.gameObject.SetActive(true);
        CurrentWeaponIndex = index;
    }

    // Get new weapon
    public void addWeapon(GameObject weapon){
        for(int i = 0; i < Weapons.Count; i++){
            // Check if the current weapon is already in the backpack
            if(Weapons[i].gameObject.name == weapon.name){
                // If it exists, increase the number of bullets of the corresponding weapon
                Weapons[i].bullet_num = Weapons[i].bullet_num + 15;
                return;
            }
        }

        var new_weapon = GameObject.Instantiate(weapon, Hand);
        new_weapon.name = weapon.name;
        new_weapon.transform.localRotation = CurrentWeapon.transform.localRotation;
        Weapons.Add(new_weapon.GetComponent<WeaponBase>());
        nextWeapon(Weapons.Count - 1 -CurrentWeaponIndex);
    }

    public void addHp(int hpUp){
        instance.hp += hpUp;
    }

    public void addBulletNum(int bulletNumUp){
        CurrentWeapon.bullet_num += bulletNumUp;
    }

    public void Shoot(){
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("idle")){
            animator.SetBool("Shoot", true);
            FindObjectOfType<AudioManager>().Play("LaserShot");
        }
    }

    public override void openFire()
    {
        base.openFire();

        // debug
        Debug.Log("Open fire");

        // CurrentWeapon.OpenFire(transform.forward);
        
        CurrentWeapon.OpenFire(hitPoint);
    }

    public Vector3 GetMousePoint(){
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(cameraRay, out hit, 100.0f, LayerMask.GetMask("Floor"))){
            Vector3 playerToMouse = hit.point - transform.position;
            hitPoint = hit.point;
            playerToMouse.y = 0;
            return playerToMouse;
        }

        return Vector3.zero;
    }

    public void PointToTarget(Vector3 rotate){
        transform.LookAt(rotate + transform.position);
    }

    public bool getMoveStatus() {
        return isMove;
    }

}
