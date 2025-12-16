using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameManager gameManager; // TODO maybe: search in scene or singleton?
    public float baseSpeed;
    public float mouseSens;
    public Camera playerCamera;
    public Transform groundCheck;
    public float groundCheckExtends;
    public LayerMask groundLayer;
    public LayerMask targetLayer;
    public Transform bombSpawn;
    public Bomb bomb;
    public float linearDamping;
    public float linearDampingAirborne;
    public float AirbourneSpeedMultiplier;

    private bool jumpRequest = false;
    private bool HorizontalMoveRequest = false;
    private bool VerticalMoveRequest = false;
    private float airborneSpeed;
    private float currentSpeed;

    private float verticalAxis;
    private float horizontalAxis;
    [SerializeField] private Transform camPivot;
    float camRotation;
    Rigidbody rb;
    
    bool canJump;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;

        currentSpeed = baseSpeed;
        airborneSpeed = baseSpeed * AirbourneSpeedMultiplier;
    }

    

    // Update is called once per frame
    void Update()
    {
        //movement 
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        if (horizontalAxis != 0)
        {
            HorizontalMoveRequest = true; 
        }
        if (verticalAxis != 0)
        {
            VerticalMoveRequest = true;
        }
        
        //transform.Translate(0, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime); // TODO: proper physics
        //transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
        //Horizontal Rotation
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        //vertical cam rotation
        camRotation -= Input.GetAxis("Mouse Y");
        camRotation = Mathf.Clamp(camRotation, -8f, 45f);
        camPivot.transform.localRotation = Quaternion.Euler(camRotation, 0, 0);
        
        //jumping
        if (Input.GetKeyDown("space") && IsGrounded())
        {
            jumpRequest = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Bomb newBomb = Instantiate(bomb, bombSpawn.position, Quaternion.Euler(0, 0, 0));
            newBomb.player = transform;

        }

        //adjust damping to avoid slow fall
        if (!IsGrounded())
        {
            rb.linearDamping = linearDampingAirborne;
            currentSpeed = airborneSpeed;
        }
        if (IsGrounded())
        {
            rb.linearDamping = linearDamping;
            currentSpeed = baseSpeed;
            
        }
    }

    private void FixedUpdate()
    {
        //execute jump
        if (jumpRequest)
        {
            canJump = false;
            jumpRequest = false;
            rb.AddForce(transform.up, ForceMode.Impulse);
            Debug.Log(canJump);
        }
        
        //execute movement
        
        if (VerticalMoveRequest)
        {
            rb.AddForce(transform.forward * verticalAxis * currentSpeed, ForceMode.VelocityChange);
        }
        if (HorizontalMoveRequest)
        {
            rb.AddForce(transform.right * horizontalAxis * currentSpeed, ForceMode.VelocityChange);
        }

    }

    private bool IsGrounded()
    {
        //Grounded check
        return Physics.Raycast(transform.GetComponent<Collider>().bounds.center, -transform.up, transform.GetComponent<Collider>().bounds.extents.y + groundCheckExtends, groundLayer);


    }

    public void Die()
    {
        gameManager.resetScene();
        
    }

    

}
