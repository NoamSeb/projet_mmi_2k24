using UnityEngine;
using UnityEngine.InputSystem;
using DialogueEditor;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_Speed = 3.0f;
    [SerializeField] private float m_TurnSmoothTime = 0.1f;
    private Animator m_Animator;
    private GameObject collidedObject;
    private string collidedObjectName;

    private CharacterController m_Character;
    private float m_TurnSmoothVelocity;

    private Vector2 m_MoveVector;
    private bool m_Interact;

    private bool m_Touched = false;

    [SerializeField] ObjectDisplay m_InteractivObjects;
    [SerializeField] GameObject m_interactionKey;

    #region Initialization
    private void OnEnable()
    {
        m_Character = gameObject.AddComponent<CharacterController>();
        m_Character.radius = 0.4f;
        m_Character.detectCollisions = true;

        // Find the character object and get its Animator component
        Transform childObject = transform.Find("character");
        if (childObject != null)
        {
            m_Animator = childObject.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Child object not found!");
        }
    }

    private void OnDisable()
    {
        Destroy(m_Character);
    }
    #endregion

    private void FixedUpdate()
    {
        Move();
    }

    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        m_MoveVector = context.ReadValue<Vector2>();
    }

    public void ReadInteractInput(InputAction.CallbackContext context)
    {
        // Check if the interaction input has started (button pressed)
        if (context.started)
        {
            m_Interact = true;

            // Check if the player is near an interactive object and if so, trigger interaction
            if (m_Touched)
            {
                m_interactionKey.SetActive(false);
                m_InteractivObjects.TriggerDialog(collidedObjectName);
                ScoreManager.Instance.AddScore();
                collidedObject.tag = "Untagged";
            }
        }
        // Check if the interaction input has been canceled (button released)
        else if (context.canceled)
        {
            m_Interact = false;
        }
    }

    private void Move()
    {
        // Find the direction
        Vector3 direction = new Vector3(m_MoveVector.x, 0f, m_MoveVector.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Get direction angle from direction vector
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Add π/2 (90 degrés) to rotation angle
            targetAngle += 90f;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, m_TurnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Use normalized vector to move the character
            Vector3 moveDirection = direction;

            // Apply the movement
            m_Character.Move(moveDirection.normalized * m_Speed * Time.deltaTime);
            m_Animator.SetBool("isWalkin", true);
        }
        else
        {
            // If the character don't move, set the isWalkin parameter to false
            m_Animator.SetBool("isWalkin", false);
        }
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "interactiv")
        {
            m_interactionKey.SetActive(true);
            collidedObject = collider.gameObject;
            collidedObjectName = collider.gameObject.name;
            m_Touched = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "interactiv")
        {
            m_Touched = false;
            m_interactionKey.SetActive(false);
        }
    }
}