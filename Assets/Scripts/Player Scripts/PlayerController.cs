using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Assume you have references to your movement and look scripts
    public FirstPersonMovement movementScript;
    public FirstPersonLook lookScript;

    public void EnablePlayerControls(bool enable)
    {
        if (movementScript != null)
        {
            movementScript.enabled = enable;
        }

        if (lookScript != null)
        {
            lookScript.enabled = enable;
        }

        // If disabling controls, unlock the cursor. If enabling, lock it back.
        Cursor.lockState = enable ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !enable;
    }
}
