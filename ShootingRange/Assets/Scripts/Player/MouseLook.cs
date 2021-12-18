using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour 
{
    private Vector2 mouseLook;
    private Vector2 smoothV;

    private readonly float sensitivity = 2.0F;
    private readonly float smoothing = 2.0F;

    private readonly float minRot = -25.0F;
    private readonly float maxRot = +25.0F;

    private PlayerController character;
    private ControlPanelUI controlPanelUI;

    void Start ()
    {
        character = PlayerController.Instance;
        controlPanelUI = ControlPanelUI.Instance;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	void Update ()
    {
        if (controlPanelUI.CheckIfControlPanelIsActive())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            LookDirection();
        }
    }

    private void LookDirection()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        moveDirection = Vector2.Scale(moveDirection, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        
        smoothV.x = Mathf.Lerp(smoothV.x, moveDirection.x, 1F / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, moveDirection.y, 1F / smoothing);

        mouseLook += smoothV;
        mouseLook.y = Mathf.Clamp(mouseLook.y, minRot, maxRot);
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
