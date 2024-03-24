using Mirror;
using UnityEngine;

public class FirstPersonLook : NetworkBehaviour
{
    [SerializeField] Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;
    public Camera playerCamera;

    // Add a public flag to control when the player can look around
    public bool canLook = true;

    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;

        if (!isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (canLook && isLocalPlayer) // Only update the look if canLook is true
        {
            // Get smooth velocity.
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);

            // Rotate camera up-down and controller left-right from velocity.
            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
        }
    }

    // Public method to set the canLook variable, called by other scripts to control the look functionality.
    public void SetCanLook(bool canLook)
    {
        this.canLook = canLook;
        // If we're not allowed to look, the cursor should be unlocked and visible.
        Cursor.lockState = canLook ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !canLook;
    }
}
