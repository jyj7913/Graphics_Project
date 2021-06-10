using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempPlayer_Select : MonoBehaviour
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
    private Vector3 forward_;
    private Vector3 right_;
    private bool allowMove;

    private List<Collider> m_collisions = new List<Collider>();

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        forward_ = new Vector3(0f, 0f, 1f);
        right_ = new Vector3(1f, 0f, 0f);
        allowMove = true;
        playerRigidbody.MovePosition(new Vector3(PlayerPrefs.GetFloat("LastPosition") * 18f, 5.2f, 0f));
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
        if(other.tag == "SampleStage")
        {
            SceneManager.LoadScene("SampleScene");
            PlayerPrefs.SetFloat("LastPosition", 0f);
        }
        if (other.tag == "Stage1")
        {
            SceneManager.LoadScene("Stage1");
            PlayerPrefs.SetFloat("LastPosition", 1f);
        }
        if (other.tag == "Stage2") {
            SceneManager.LoadScene("Stage2");
            PlayerPrefs.SetFloat("LastPosition", 2f);
        }
        if (other.tag == "Stage3")
        {
            SceneManager.LoadScene("Stage3");
            PlayerPrefs.SetFloat("LastPosition", 3f);
        }
    }

    private void Update()
    {
        if (!m_jumpInput && Input.GetKey(KeyCode.Space))
        {
            m_jumpInput = true;
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
        if (Input.GetKey(KeyCode.LeftControl)) {
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
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && m_jumpInput)
        {
            m_jumpTimeStamp = Time.time;
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
}
