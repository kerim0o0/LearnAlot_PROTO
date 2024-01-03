using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCmanager : MonoBehaviour
{
    public float speed = 2f; // speed of movement
    public float minMoveTime = 1f; // minimum time to move in one direction
    public float maxMoveTime = 3f; // maximum time to move in one direction
    public float smoothTurn = 0.1f;

    public float minStoppageTime = 1f;
    public float maxStoppageTime = 3f;
    public bool wavingNPC;

    public float gravity;
    private float stoppageTime;
    private float moveTime; // time remaining in current direction
    private Vector3 moveDirection; // current movement direction
    private Vector3 velocity;
    private CharacterController controller; // character controller component
    private float turnVelocity;
    private Animator npcAnim;

    // Start is called before the first frame update
    void Start()
    {
        // get character controller component
        controller = GetComponent<CharacterController>();

        if (!wavingNPC)
        {
            // set initial move direction
            moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

            // set initial move time
            moveTime = Random.Range(minMoveTime, maxMoveTime);

            stoppageTime = Random.Range(minStoppageTime, maxStoppageTime);

            npcAnim = this.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (moveTime <= 0)
        {
            moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
            moveTime = Random.Range(minMoveTime, maxMoveTime);
            stoppageTime = Random.Range(minStoppageTime, maxStoppageTime);
        }

        if (stoppageTime > 0) {

            stoppageTime -= Time.deltaTime;
            npcAnim.SetBool("Walk", false);
        }

        if (moveDirection.magnitude > 0.1f && stoppageTime <= 0)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, smoothTurn);
            this.transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move((moveDir + velocity) * speed * Time.deltaTime);
            moveTime -= Time.deltaTime;
            npcAnim.SetBool("Walk", true);
        }

        velocity.y += gravity * Time.deltaTime;
    }
}