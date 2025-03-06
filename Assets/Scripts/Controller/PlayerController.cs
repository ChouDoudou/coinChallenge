using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using Unity.VisualScripting.ReorderableList;
using UnityEditor.Experimental.GraphView;
#endif
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]                // Assure la présence du component RigidBody
public class PlayerController : MonoBehaviour
{
    private GameController gameScript;
    [Header("Mouvement")]                            // Permet plus de lisibilité dans l'affichage de l'inspector
    public float moveSpeed = 5f;
    public float superSpeed = 6.5f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float rotationSpeed = 5f;
    public float jumpForce = 5f;
    public string horizontalAxis = "Horizontal";     // Axe horizontal modifiable dans l'éditeur
    public string verticalAxis = "Vertical";         // Axe vertical modifiable dans l'éditeur
    public string jumpButton = "Jump";
    [Header("Paramètres de gravitation")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.5f;         // Distance maximale pour le Raycast
    private Rigidbody rb;
    [SerializeField] private Animator animator;
    private int life = 120;
    private const int maxLife = 120;
    public float force = 1f;
    public Transform cam;
    bool jump = false;
    private float speed;
    public int enemieDestroyed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameScript = FindObjectOfType<GameController>();
        speed = moveSpeed;
    }

    // Vérifie si le joueur est au sol
    bool IsGrounded()
    {
        // Lance un Raycast vers le bas pour détecter le sol
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    void Move(Vector3 direction)
    {
        // Déplace le joueur
        transform.position += direction * speed * Time.deltaTime;
    }

    void Update()
    {
        // Gère les mouvements horizontaux
        float horizontal = Input.GetAxisRaw(horizontalAxis);
        float vertical = Input.GetAxisRaw(verticalAxis);
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Move(moveDir);
        }

        // Gère le saut
        if (Input.GetButtonDown(jumpButton) && IsGrounded())
        {
            rb.velocity = Vector3.up * jumpForce;
            jump = true;
        }

        else if (jump && IsGrounded())
        {
            jump = false;
        }

        animator.SetBool("Moving", movement.magnitude >= 0.1f);
        animator.SetBool("isGrounded", IsGrounded());

        // Vérifie si le joueur est tombé sous Y = -10
        if (transform.position.y <= -10)
        {
            GameOver();
        }
    }

    public void UpdateLife(int value)
    {
        life += value;
        if (life > maxLife) life = maxLife;
        if (life < 0) life = 0;
        if (life <= 0) GameOver();
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        gameScript.GameOver(); // Appelle GameOver() du GameController
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float deltaY = collision.transform.position.y - transform.position.y;

            if (deltaY < 0)
            {
                // Si le joueur saute, l'ennemi est détruit
                collision.transform.parent.gameObject.SetActive(false);
                Debug.Log("L'ennemi a été détruit.");
                enemieDestroyed += 1;
            }

            else
            {
                // Si le joueur n'est pas en train de sauter, il perd de la vie
                LostLife(120);
            }
        }
    }

    public void LostLife(int damage)
    {
        UpdateLife(-damage);
    }

    public void SetSuperSpeed()
    {
        speed = superSpeed;
    }

        public void SetMoveSpeed()
    {
        speed = moveSpeed;
    }
}