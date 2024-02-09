using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerBehavior : MonoBehaviour
{
    public GameBehavior gameManager;

    public float moveSpeed = 10f;

    public float jumpVelocity = 5f;

    public float distanceToGround = 0.1f;

    public LayerMask groundLayer;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    private float vInput;
    private float hInput;

    private Rigidbody _rb;

    private bool canShoot = false;
    private bool canJump = false;

    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;

    public Camera Main_Camera;


    public bool lockCursor = true;

    private CapsuleCollider _col;
    

    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Image crosshairObject;

    public bool playerCanMove = true;
   

    // Start is called before the first frame update
    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        _col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * moveSpeed;
        
        if(Input.GetMouseButtonDown(0))
        {
            canShoot = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            canJump = true;
        }

        if (cameraCanMove)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            if (!invertCamera)
            {
                pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
            }
            else
            {
                // Inverted Y
                pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
            }

            // Clamp pitch between lookAngle
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            Main_Camera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }
    }
    
    void FixedUpdate()
    {

        if (canJump)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            canJump = false;
        }

        if (canShoot)
        {
            GameObject newBullet = Instantiate(bullet, Main_Camera.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;
            newBullet.transform.eulerAngles = new Vector3(Main_Camera.transform.rotation.x, this.transform.rotation.y, 90);
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = Main_Camera.transform.forward * bulletSpeed;
            canShoot = false;
        }
        
        _rb = GetComponent<Rigidbody>();

        //Vector3 rotation = Vector3.up * hInput;

        //Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(transform.position + (transform.forward * vInput + this.transform.right * hInput) * Time.fixedDeltaTime);
    }
    private bool isGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }
}
