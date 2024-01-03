using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    private float turnVelocity;
    private Transform camTransform;
    private Vector3 velocity;
    private Animator playerAnim;
    private float movementSpeed;

    public float walkSpeed = 6f;
    public float runSpeed = 15f;
    public float smoothTurn = 0.1f;

    public float gravity = -20f;
    public float jumpForce = 3f;

    void Start()
    {
        movementSpeed = walkSpeed;
        cc = this.GetComponent<CharacterController>();
        camTransform = Camera.main.transform;

        playerAnim = this.transform.GetComponentInChildren<Animator>();
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, smoothTurn);
            this.transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            cc.Move((moveDir + velocity) * movementSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnim.SetBool("Run", true);
                playerAnim.SetBool("Walk", false);
                movementSpeed = runSpeed;
            }
            else {
                playerAnim.SetBool("Run", false);
                playerAnim.SetBool("Walk", true);
                movementSpeed = walkSpeed;
            }

        }
        else {

            playerAnim.SetBool("Run", false);
            playerAnim.SetBool("Walk", false);
            movementSpeed = walkSpeed;
        }



        if (cc.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {

            velocity.y = Mathf.Sqrt(jumpForce);
            playerAnim.SetTrigger("Jump");

        }
        else {
           velocity.y += gravity * Time.deltaTime;
            cc.Move(velocity * movementSpeed * Time.deltaTime);
        }


    }

    public void teleport(Vector3 pos) {
        
        this.transform.position = pos;
        Debug.Log("yes");
    }

    public void setAnimator(int index) {

        playerAnim = this.transform.GetChild(index).GetComponent<Animator>();
    }
}