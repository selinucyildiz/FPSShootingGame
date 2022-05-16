using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity; 
    private bool isGrounded; // when object moves, it is moving up, 
    public float speed = 5f; 
    public float gravity = -9.8f; 
    public float jumpHeight = 1.5f;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        
    }

    //receive the inputs for inputmanager, apply them ch controller
    public void ProcessMove(Vector2 input) 
    {
        Vector3 directionMove = Vector3.zero;
        directionMove.x = input.x;
        directionMove.z = input.y;
        controller.Move(transform.TransformDirection(directionMove) * Time.deltaTime * speed);
        // for checking grounded 
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y); // to see how much force being applied to out player
        animator.SetFloat("speed", Mathf.Abs(directionMove.x) + Mathf.Abs(directionMove.z));
    }

    public void Jump() 
    {
        if(isGrounded) {
            playerVelocity.y = Mathf.Sqrt(jumpHeight* -3.0f * gravity);

        }
    }
}
