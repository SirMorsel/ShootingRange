using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;
    public static PlayerController Instance { get { return _instance; } }

    [SerializeField ]private float walkSpeed = 5.0F;
    [SerializeField] private float runSpeed = 10.0F;
    [SerializeField] private float jumpForce = 8.0F;
    [SerializeField] private float gravity = 9.81F;

    private Vector3 moveDirection = Vector3.zero;

    private bool isShooting;
    private bool canShoot = true;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Animator animatorController;

    // Start is called before the first frame update
    void Start()
    {
        //animatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
       // ReloadWeapon();
    }

    private void Move()
    {
        CharacterController controller = gameObject.GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection *= runSpeed;
                //animatorController.SetFloat("movementY", Input.GetAxis("Vertical") * 2);
            }
            else
            {
                moveDirection *= walkSpeed;
                //animatorController.SetFloat("movementX", Input.GetAxis("Horizontal"));
                //animatorController.SetFloat("movementY", Input.GetAxis("Vertical"));
            }

            if (Input.GetKey(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);


    }
    /*
    public void ReloadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponentInChildren<WeaponStats>().manualReaload();
        }
    }

    public void fire()
    {
        GetComponentInChildren<WeaponStats>().getMag();

        if (GetComponentInChildren<WeaponStats>().currrentBulletsInMag > 0 && canShoot == true)
        {
            isShooting = true;
            GetComponentInChildren<WeaponStats>().getAudio();
            float projectile_speed = GetComponentInChildren<WeaponStats>().bulletSpeed;
            // Debug.Log("Nach Schuss " + GetComponentInChildren<WeaponStats>().currrentBulletsInMag);

            GameObject bullet = Instantiate(GetComponentInChildren<WeaponStats>().bulletPrefab,
                                            GetComponentInChildren<WeaponStats>().bulletSpawn.position,
                                            GetComponentInChildren<WeaponStats>().bulletSpawn.rotation) as GameObject;
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * projectile_speed * Time.deltaTime;
            bullet.GetComponent<Rigidbody>().AddRelativeForce((Vector3.forward * 12) * projectile_speed);
            bullet.GetComponent<BulletInfos>().damage = GetComponentInChildren<WeaponStats>().damage;
        }
    }
    */
}
