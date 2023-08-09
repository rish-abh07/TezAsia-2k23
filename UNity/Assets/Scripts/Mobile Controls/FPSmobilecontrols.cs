using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSmobilecontrols : MonoBehaviour
{
    //Movement 
    public CharacterController  characterController;
    Vector2 moveTouchStartPosition;
    Vector2 moveInput;
    //Refrences 
    public Transform cameraTransform;

    //Player Settings
    public float cameraSenstivity;
    public float moveSpeed;
    public float moveInputDeadZone;
//Touch detection
    int leftFingerId, rightFingerId;
    float halfScreenWidth;

    //CameraContol
    Vector2 lookInput;
    float cameraPitch;



    // Start is called before the first frame update
    void Start()
    {
        leftFingerId = -1;
        rightFingerId = -1;

        halfScreenWidth = Screen.width / 2;

        //calculate the movement input dead zone 
        moveInputDeadZone = Mathf.Pow(Screen.height/moveInputDeadZone,2);
        
    }

    // Update is called once per frame
    private void Update()
    {
        {
            GetTouchInput();

            if(rightFingerId != -1)
            {
                LookAround();
            }
            if(leftFingerId != -1) 
            {
                Move();
            }
        }
    }
    void GetTouchInput()
    {
        for(int i =0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            //Check Each Touch Phase
            switch(touch.phase)
            {
                case TouchPhase.Began:
                    if(touch.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        //Statrt tracking the left finger if it was not tracked before
                        leftFingerId = touch.fingerId;
                        // set the start position 
                        moveTouchStartPosition = touch.position;

                    }
                    else if(touch.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        rightFingerId=touch.fingerId;
                       
                    }
                    break;
                    case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if(touch.fingerId == leftFingerId)
                    {
                        //stop tracking the left finger
                        leftFingerId=-1;
                        Debug.Log("Stopped tracking left finger");
                    }
                    else if(touch.fingerId == rightFingerId)
                    {
                        //stop tracking right finger 
                        rightFingerId=-1;
                        Debug.Log("Stopped tracking the right finger");
                    }
                    break; 
                    case TouchPhase.Moved:
                        if(touch.fingerId == rightFingerId)
                        {
                        lookInput = touch.deltaPosition * cameraSenstivity * Time.deltaTime;
                        }
                        else if(touch.fingerId == leftFingerId){
                        moveInput = touch.position - moveTouchStartPosition;
                        }
                    break;
                case TouchPhase.Stationary:
                    if(touch.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;

            }
        }
        
        
    }
    void LookAround()
    {
        // vertical pitch rotation 
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        //horizontal yaw rotation
        transform.Rotate(transform.up, lookInput.x);

    }
    void Move()
    {
        //Dont move if the touch device is shoter then the decided dead zone
        if (moveInput.sqrMagnitude <= moveInputDeadZone) return;

        Vector2 movementDir = moveInput.normalized* moveSpeed*Time.deltaTime;

        characterController.Move(transform.right * movementDir.x + transform.forward *movementDir
            .y);
        
    }
}
