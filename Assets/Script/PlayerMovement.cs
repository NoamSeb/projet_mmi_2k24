using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_Speed = 3.0f;
    [SerializeField] private float m_TurnSmoothTime = 0.1f;
    private Animator m_Animator;

    private CharacterController m_Character;
    private float m_TurnSmoothVelocity;

    private Vector2 m_MoveVector;

    private ObjectDisplay m_InteractivObject;

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

   private void Move()
{
    // Find the direction
    Vector3 direction = new Vector3(m_MoveVector.x, 0f, m_MoveVector.y).normalized;

    if (direction.magnitude >= 0.1f)
    {
        // Get direction angle from direction vector
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // Ajouter π/2 (90 degrés) à l'angle de rotation
        targetAngle += 90f;

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, m_TurnSmoothTime);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        // Utiliser directement le vecteur de direction normalisé pour le mouvement
        Vector3 moveDirection = direction;

        // Appliquer le mouvement
        m_Character.Move(moveDirection.normalized * m_Speed * Time.deltaTime);
        m_Animator.SetBool("isWalkin", true);
    }
    else
    {
        // Si le personnage ne bouge pas, définir le paramètre isWalking sur false
        m_Animator.SetBool("isWalkin", false);
    }
}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "interactiv")
        {
            Debug.Log("Collision with: " + hit.gameObject.name);
            //alertDisplay.DisplayAlert("Press F to interact");
        }
    }
}