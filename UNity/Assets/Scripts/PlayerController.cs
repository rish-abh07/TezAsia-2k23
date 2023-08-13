using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public CharacterController controller;

    float speed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 3.0f;
    public GameObject Character;
    private Animator _anim;
    // Start is called before the first frame update
    Vector3 velocity;
    bool isGrounded;
    bool isSprinting = true;
    public float mass = 100f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    void Start()
    {
        _anim = Character.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(!isGrounded && velocity.y < 0)
        {
            velocity.y = -mass;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            controller.Move(move * speed * Time.deltaTime);
            _anim.Play("MoveFWD_Normal_InPlace_SwordAndShield");
            if(Input.GetKeyDown(KeyCode.LeftShift) && isSprinting){
                speed = 15.0f;
            }
            
            
            
        }else if(Input.GetKey(KeyCode.Space) && !isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
           // Debug.Log("I am walking");
            _anim.Play("JumpFull_Normal_RM_SwordAndShield");
           
        }
        else
        {
            _anim.Play("Idle_Battle_SwordAndShield");
            speed = 12.0f;
            
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        Debug.Log(isGrounded);
        Debug.Log(speed);

    }
}
