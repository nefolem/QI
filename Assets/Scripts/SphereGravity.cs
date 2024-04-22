using System;
using Unity.VisualScripting;
using UnityEngine;

public class SphereGravity : MonoBehaviour
{
    [SerializeField] private Transform _gravityTarget;
    [SerializeField] private float _power = 10f; // Speed of player movement
    //[SerializeField] private float _torque = 2f; // The magnitude of the Physics.gravity vector
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _autoOrientSpeed = 3f;
    [SerializeField] private bool _isAutoOrienting = true;
    [SerializeField] private float _jumpForce = 5f; // The force applied when jumping
    [SerializeField] private LayerMask _groundMask; // Layer mask to detect ground
    [SerializeField] private Transform _groundCheckPosition; // Layer mask to detect ground

    private Rigidbody _rb;
    private bool _isGrounded = true;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _isGrounded = Physics.Raycast(_groundCheckPosition.position, -_groundCheckPosition.up, 2f, _groundMask);

    }

    void FixedUpdate()
    {
        if (!GameManager.Instanсe.IsCardShown)
        {
            ProcessInput();

        }
        ProcessGravity();
    }

    private void ProcessInput()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        
        Vector3 gravityDirection = (_gravityTarget.position + transform.position).normalized;
        Vector3 moveDirectionInGravityPlane = Vector3.Cross(gravityDirection, -transform.right) * verticalInput + Vector3.Cross(gravityDirection, transform.forward) * horizontalInput;
        
        transform.position += moveDirectionInGravityPlane * _power * Time.fixedDeltaTime;
        
        // Vector3 vForce = new Vector3(0f, 0f, verticalInput * _power);
        // Vector3 hForce = new Vector3(0f, horizontalInput * _torque, 0f);
        //
        // _rb.AddRelativeForce(hForce);
        // _rb.AddRelativeForce(vForce);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
    }

    private void ProcessGravity()
    {
        Vector3 diff = transform.position - _gravityTarget.position;
        _rb.AddForce(-diff.normalized * _gravityScale * _rb.mass);

        if (_isAutoOrienting)
        {
            AutoOrient(-diff);
        }

        // Check if the player is grounded
        
    }

    private void AutoOrient(Vector3 down)
    {
        Quaternion orientDirection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientDirection, _autoOrientSpeed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        _rb.AddForce(transform.up * _jumpForce);
    }
}
