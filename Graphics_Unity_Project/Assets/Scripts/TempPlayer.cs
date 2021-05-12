using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TempPlayer : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    public Camera main_cam;
    public CinemachineVirtualCamera player2d_cam;
    public CinemachineVirtualCamera player3d_cam;
    private bool is2D;
    private Vector3 forward_;
    private Vector3 right_;
    private bool allowMove;
    // Start is called before the first frame update
    void Start()
    {
        is2D = true;
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        Debug.Log(transform.forward);
        Debug.Log(transform.right);
        forward_ = Vector3.zero;
        right_ = new Vector3(0f, 0f, 1f);
        allowMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowMove)
        {
            CamControl();
            Move();
        }
    }
    private void Move()
    {
        Vector3 dist = playerInput.move_hori * right_ * Time.deltaTime * 5f;
        Vector3 dist2 = playerInput.move_vert * forward_ * Time.deltaTime * 5f;
        Vector3 relativePos = dist + dist2;
        playerRigidbody.MovePosition(playerRigidbody.position + relativePos);
        if (relativePos != Vector3.zero)                                                    // Rotate Facing
        {
            Vector3 targetDirection = relativePos.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime);
            }
        }
    }
    private void CamControl()
    {
        if (playerInput.changeView)
        {
            is2D = !is2D;
            if (is2D)
            {
                player2d_cam.Priority = 10;             // switch camera
                player3d_cam.Priority = 5;
                playerRigidbody.MovePosition(new Vector3(0, playerRigidbody.position.y, playerRigidbody.position.z));   // x to zero
                forward_ = Vector3.zero;                // change key
                right_ = new Vector3(0f, 0f, 1f);
            }
            else
            {
                player2d_cam.Priority = 5;
                player3d_cam.Priority = 10;
                playerRigidbody.MovePosition(new Vector3(0, playerRigidbody.position.y, playerRigidbody.position.z));
                forward_ = new Vector3(0f, 0f, 1f);
                right_ = new Vector3(1f, 0f, 0f);
            }
            StartCoroutine(waitChange());               // Delay        
        }
    }
    IEnumerator waitChange()
    {
        transform.localScale = new Vector3(0, 0, 0);        // Make Invisible
        playerRigidbody.useGravity = false;                 // Fix in sky
        playerRigidbody.MovePosition(playerRigidbody.position + new Vector3(0f, 0.1f, 0f)); // For if
        allowMove = false;                                  // Fix object

        yield return new WaitForSeconds(0.3f);
        main_cam.orthographic = !main_cam.orthographic;

        yield return new WaitForSeconds(0.4f);              // Wait for 0.7 sec total

        transform.localScale = new Vector3(1, 1, 1);        // No more fix...
        playerRigidbody.useGravity = true;
        allowMove = true;
    }
}
