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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
            }
            else
            {
                moveDirection *= walkSpeed;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
