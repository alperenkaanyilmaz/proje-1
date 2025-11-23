using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3.5f;
    public float runMultiplier = 1.9f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.1f;

    [Header("Look")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2.2f;
    float pitch = 0f;

    CharacterController cc;
    Vector3 velocity;

    [Header("Interaction")]
    public float interactRange = 2.5f;
    public LayerMask interactLayer;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        if (cameraTransform == null && Camera.main != null) cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleLook();
        HandleMove();
        HandleInteraction();
    }

    void HandleLook()
    {
        float mx = Input.GetAxis("Mouse X") * mouseSensitivity;
        float my = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mx);
        pitch -= my;
        pitch = Mathf.Clamp(pitch, -85f, 85f);
        if (cameraTransform) cameraTransform.localEulerAngles = Vector3.right * pitch;
    }

    void HandleMove()
    {
        if (cc.isGrounded && velocity.y < 0) velocity.y = -2f;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float targetSpeed = walkSpeed * (Input.GetKey(KeyCode.LeftShift) ? runMultiplier : 1f);

        Vector3 move = transform.TransformDirection(input.normalized) * targetSpeed;
        cc.Move(move * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactRange, interactLayer))
            {
                var interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null) interactable.Interact(transform);
            }
        }
    }
}
