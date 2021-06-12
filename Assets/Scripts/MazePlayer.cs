using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MazePlayer : MonoBehaviour                                         // Similar with TempPlayer.cs except FPS
{
    private float m_moveSpeed = 5;
    private float m_jumpForce = 7;

    private readonly float m_walkScale = 0.33f;
    private readonly float m_sprintScale = 2f;

    private bool m_wasGrounded;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.05f;
    private bool m_jumpInput = false;

    private bool m_isGrounded;

    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    public Camera main_cam;
    public CinemachineVirtualCamera player2d_cam;
    public CinemachineVirtualCamera player3d_cam;
    private bool is2D;
    private Vector3 forward_;
    private Vector3 right_;
    private bool allowMove;
    private Vector3 temp = new Vector3(0f, 0f, 1f);

    private float rotateSpeedX = 15F;
    private float minRotateX = -360F;
    private float maxRotateX = 360F;
    private Quaternion xQuaternion;

    private float rotationX = 0F;

    private float rotAverageX = 0F;

    private List<Collider> m_collisions = new List<Collider>();

    void Start()
    {
        is2D = true;
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        forward_ = new Vector3(0f, 0f, 1f);
        right_ = new Vector3(1f, 0f, 0f);
        allowMove = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Kill")
        {
            Dead();
        }
        if (other.tag == "Maze")
        {
            SceneManager.LoadScene("Stage2Final");
        }
        if (other.tag == "Home")
        {
            SceneManager.LoadScene("StageSelection");
        }
    }

    private void Update()
    {
        if (!m_jumpInput && Input.GetKey(KeyCode.Space))
        {
            m_jumpInput = true;
        }

        if (allowMove && m_isGrounded)
        {
            CamControl();
        }
    }

    private void FixedUpdate()
    {
        playerAnimator.SetBool("Grounded", m_isGrounded);
        if (allowMove)
        {
            DirectUpdate();
        }
        m_wasGrounded = m_isGrounded;
        m_jumpInput = false;
    }

    private void DirectUpdate()
    {
        Vector3 dist = playerInput.move_hori * right_ * Time.deltaTime * m_moveSpeed;
        Vector3 dist2 = playerInput.move_vert * forward_ * Time.deltaTime * m_moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dist *= m_walkScale;
            dist2 *= m_walkScale;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            dist *= m_sprintScale;
            dist2 *= m_sprintScale;
        }
        Vector3 relativePos = dist + dist2;
        playerRigidbody.MovePosition(playerRigidbody.position + relativePos);

        float directionLength = relativePos.magnitude;
        relativePos.y = 0;
        relativePos = relativePos.normalized * directionLength;

        if (is2D)
        {
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
        else
        {
            rotAverageX = 0f;                                                                   // Detect mouse movement for FPS moving

            rotationX += Input.GetAxis("Mouse X") * rotateSpeedX;
            rotAverageX = ClampAngle(rotationX, minRotateX, maxRotateX);

            xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

            transform.rotation = xQuaternion;
            forward_ = xQuaternion * temp;
            right_ = new Vector3(forward_.z, 0f, -forward_.x);
        }

        playerAnimator.SetFloat("MoveSpeed", relativePos.magnitude);
        JumpingAndLanding();
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && m_jumpInput)
        {
            m_jumpTimeStamp = Time.time;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            playerAnimator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            playerAnimator.SetTrigger("jump");
        }
    }

    private void CamControl()
    {
        if (Input.GetButtonDown("ChangeView"))
        {
            is2D = !is2D;
            if (is2D)
            {
                player2d_cam.Priority = 10;             // switch camera
                player3d_cam.Priority = 5;   // x to zero
                forward_ = new Vector3(0f, 0f, 1f);
                right_ = new Vector3(1f, 0f, 0f);
            }
            else
            {
                player2d_cam.Priority = 5;
                player3d_cam.Priority = 10;
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

    void Dead()
    {
        Debug.Log("Die!");
        if (SceneManager.GetActiveScene().name == "Stage1")
            SceneManager.LoadScene("Stage1");
        else if (SceneManager.GetActiveScene().name == "Stage3" || SceneManager.GetActiveScene().name == "Stage2Final")
            SceneManager.LoadScene("Stage3");
        else if (SceneManager.GetActiveScene().name == "Stage2")
            SceneManager.LoadScene("Stage2");

    }
    public static float ClampAngle(float angle, float min, float max)           // Return proper angles between minimum, maximum angle
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}