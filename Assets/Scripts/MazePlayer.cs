using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MazePlayer : MonoBehaviour
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

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationX = 0F;
    float rotationY = 0F;

    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0F;

    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0F;

    public float frameCounter = 20;

    Quaternion originalRotation;

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

        originalRotation = transform.localRotation;
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
        if (other.tag == "ToMaze")
        {
            PlayerPrefs.SetInt("keys", 2);
            SceneManager.LoadScene("StageMaze");
        }
        if (other.tag == "Maze")
        {

            SceneManager.LoadScene("Stage2Final");
        }
        if (other.tag == "Home")
        {
            SceneManager.LoadScene("StageSelection");
        }
        if (other.tag == "Stage1")
        {
            GameManager.instance.Stage1Clear();
            SceneManager.LoadScene("StageSelection");
        }
        if (other.tag == "Stage2")
        {
            GameManager.instance.Stage2Clear();
            SceneManager.LoadScene("StageSelection");
        }
        if (other.tag == "Stage3")
        {
            GameManager.instance.Stage3Clear();
            SceneManager.LoadScene("StageSelection");
        }
        if (other.tag == "JumpUp")
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector3.up * m_jumpForce * 4, ForceMode.Impulse);
            playerRigidbody.AddForce(Vector3.forward * m_jumpForce, ForceMode.Impulse);
        }
        if (other.tag == "JumpUp2")
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            playerRigidbody.AddForce(Vector3.left * m_jumpForce, ForceMode.Impulse);
        }
        if (other.tag == "JumpUp3")
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector3.up * m_jumpForce * 5, ForceMode.Impulse);
            playerRigidbody.AddForce(Vector3.back * m_jumpForce * 2.7f, ForceMode.Impulse);
        }
        if (other.tag == "JumpUp4")
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector3.up * m_jumpForce * 5, ForceMode.Impulse);
            playerRigidbody.AddForce(Vector3.forward * m_jumpForce * 2.7f, ForceMode.Impulse);
        }
        if (other.tag == "JumpUp5")
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector3.up * m_jumpForce * 3.4f, ForceMode.Impulse);
        }
        if (other.tag == "JumpUp6")
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector3.up * m_jumpForce * 5, ForceMode.Impulse);
            playerRigidbody.AddForce(Vector3.back * m_jumpForce * 8, ForceMode.Impulse);
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

        if (relativePos != Vector3.zero)                                                    // Rotate Facing
        {
            Vector3 targetDirection = relativePos.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime);
            }
        }
        playerAnimator.SetFloat("MoveSpeed", relativePos.magnitude);

        JumpingAndLanding();

        if (!is2D)
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                rotAverageY = 0f;
                rotAverageX = 0f;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationX += Input.GetAxis("Mouse X") * sensitivityX;

                rotArrayY.Add(rotationY);
                rotArrayX.Add(rotationX);

                if (rotArrayY.Count >= frameCounter)
                {
                    rotArrayY.RemoveAt(0);
                }
                if (rotArrayX.Count >= frameCounter)
                {
                    rotArrayX.RemoveAt(0);
                }

                for (int j = 0; j < rotArrayY.Count; j++)
                {
                    rotAverageY += rotArrayY[j];
                }
                for (int i = 0; i < rotArrayX.Count; i++)
                {
                    rotAverageX += rotArrayX[i];
                }

                rotAverageY /= rotArrayY.Count;
                rotAverageX /= rotArrayX.Count;

                rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
                rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

                Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
                player3d_cam.LookAt.localRotation = originalRotation * xQuaternion * yQuaternion;
            }
            else if (axes == RotationAxes.MouseX)
            {
                rotAverageX = 0f;

                rotationX += Input.GetAxis("Mouse X") * sensitivityX;

                rotArrayX.Add(rotationX);

                if (rotArrayX.Count >= frameCounter)
                {
                    rotArrayX.RemoveAt(0);
                }
                for (int i = 0; i < rotArrayX.Count; i++)
                {
                    rotAverageX += rotArrayX[i];
                }
                rotAverageX /= rotArrayX.Count;

                rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

                Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
                player3d_cam.LookAt.localRotation = originalRotation * xQuaternion;
            }
            else
            {
                rotAverageY = 0f;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

                rotArrayY.Add(rotationY);

                if (rotArrayY.Count >= frameCounter)
                {
                    rotArrayY.RemoveAt(0);
                }
                for (int j = 0; j < rotArrayY.Count; j++)
                {
                    rotAverageY += rotArrayY[j];
                }
                rotAverageY /= rotArrayY.Count;

                rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);

                Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
                player3d_cam.LookAt.localRotation = originalRotation * yQuaternion;
            }
            Debug.Log(player3d_cam.LookAt.rotation.eulerAngles.normalized);
        }
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
            TransParent[] transParents = FindObjectsOfType<TransParent>();
            TransParent2[] transParents2 = FindObjectsOfType<TransParent2>();
            Blocking[] blockings = FindObjectsOfType<Blocking>();
            TransparentObj[] TransparentObjs = FindObjectsOfType<TransparentObj>();
            is2D = !is2D;
            if (is2D)
            {
                player2d_cam.Priority = 10;             // switch camera
                player3d_cam.Priority = 5;
                playerRigidbody.MovePosition(new Vector3(7, playerRigidbody.position.y, playerRigidbody.position.z));   // x to zero
                for (int i = 0; i < transParents.Length; i++)
                {
                    transParents[i].SetTrue();
                }
                for (int i = 0; i < transParents2.Length; i++)
                {
                    transParents2[i].SetFalse();
                }
                for (int j = 0; j < blockings.Length; j++)
                {
                    blockings[j].LongCollider();
                }
                for (int j = 0; j < TransparentObjs.Length; j++)
                {
                    TransparentObjs[j].SetTrue();
                }
            }
            else
            {
                player2d_cam.Priority = 5;
                player3d_cam.Priority = 10;
                playerRigidbody.MovePosition(new Vector3(0, playerRigidbody.position.y, playerRigidbody.position.z));
                for (int i = 0; i < transParents.Length; i++)
                {
                    transParents[i].SetFalse();
                }
                for (int i = 0; i < transParents2.Length; i++)
                {
                    transParents2[i].SetTrue();
                }
                for (int j = 0; j < blockings.Length; j++)
                {
                    blockings[j].ShortCollider();
                }
                for (int j = 0; j < TransparentObjs.Length; j++)
                {
                    TransparentObjs[j].SetFalse();
                }
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
        else if (SceneManager.GetActiveScene().name == "Stage2" || SceneManager.GetActiveScene().name == "Stage2Final")
            SceneManager.LoadScene("Stage2");
        else if (SceneManager.GetActiveScene().name == "Stage3")
            SceneManager.LoadScene("Stage3");

    }

    public static float ClampAngle(float angle, float min, float max)
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
