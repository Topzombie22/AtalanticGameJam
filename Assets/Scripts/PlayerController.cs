using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Objects")]
    public GameObject player;
    public GameObject cam;
    private Rigidbody rb;

    [Header("Variables")]
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float xSensativity;
    [SerializeField]
    private float ySensativity;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float dashPower;
    [SerializeField]
    private float sphereCastDist;

    private float yRotation;

    public bool canMove;
    public bool canLook;
    public bool canJump;
    public bool isGrounded;


    [Header("Vectors")]
    [SerializeField]
    Vector3 playerMovement;
    [SerializeField]
    Vector3 input;
    public Vector3 speed;
    float camYClamp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Jump();
            MovePlayer();
        }
        if (canLook)
        {
            MoveCamera();
        }
    }

    void MovePlayer()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal") * playerSpeed, rb.velocity.y, Input.GetAxisRaw("Vertical") * playerSpeed);
        playerMovement = transform.TransformDirection(input);
        rb.velocity = new Vector3(playerMovement.x, rb.velocity.y, playerMovement.z);
        speed = rb.velocity;
    }


    void MoveCamera()
    {
        float x = xSensativity * Input.GetAxisRaw("Mouse X");
        float y =+ ySensativity * Input.GetAxisRaw("Mouse Y");

        yRotation -= y;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        player.transform.Rotate(0, x, 0);
    }

    void Jump()
    {

        if (canJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            canJump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.y);
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

        RaycastHit hit;
        if (Physics.SphereCast(player.transform.position, 0.1f, Vector3.down, out hit, sphereCastDist))
        {
            if (hit.transform.tag == "Ground")
            {
                isGrounded = true;
                canJump = true;
            }
        }
        else
        {
            isGrounded = false;
            canJump = false;
        }
    }
}
