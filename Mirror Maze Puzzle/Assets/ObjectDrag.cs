using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    [SerializeField] float blockMoveSpeed;
    [SerializeField] float maxForce = 10f; // Adjust the maximum force as needed

    private Rigidbody rb;
    private Camera mainCamera;
    private Vector3 mouseStartPosition;
    private bool isDragging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        // Freeze constraints you don't want to be affected during dragging
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void OnMouseDown()
    {
        if(GameController.Instance.GetGameState() == GameController.GameState.Ongoing)
        {
            isDragging = true;
            mouseStartPosition = GetMouseWorldPos();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 mouseDelta = GetMouseWorldPos() - mouseStartPosition;

            // Only allow movement along the X or Z axis, based on the larger delta
            Vector3 movement = new Vector3(
                Mathf.Abs(mouseDelta.x) > Mathf.Abs(mouseDelta.z) ? mouseDelta.x : 0f,
                0f,
                Mathf.Abs(mouseDelta.z) > Mathf.Abs(mouseDelta.x) ? mouseDelta.z : 0f
            );

            // Adjust force multiplier as needed and clamp the force magnitude
            Vector3 force = movement * blockMoveSpeed;
            force = Vector3.ClampMagnitude(force, maxForce);
            rb.AddForce(force, ForceMode.Force);
            if(GameController.Instance.GetGameState() == GameController.GameState.Ended)
            {
                isDragging = false;
                rb.velocity = Vector3.zero;
            }
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}
