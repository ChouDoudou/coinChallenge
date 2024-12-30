using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]                // Assure la présence du component RigidBody
[RequireComponent(typeof(Animator))]                 // Assure la présence du component Animator
public class PlayerController : MonoBehaviour
{
    private GameController gameScript;
    [Header("Mouvement")]                            // Permet plus de lisibilité dans l'affichage de l'inspector
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float jumpForce = 5f;
    public string horizontalAxis = "Horizontal";     // Axe horizontal modifiable dans l'éditeur
    public string verticalAxis = "Vertical";         // Axe vertical modifiable dans l'éditeur
    public string jumpButton = "Jump";
    [Header("Paramètres de gravitation")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.5f;         // Distance maximale pour le Raycast
    private Rigidbody rb;
    private Animator animator;
    private bool isOnFloor;
    private int life = 120;
    private const int maxLife = 120;
    public float force = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Vérifie si le joueur est au sol
    bool IsGrounded()
    {
        // Lance un Raycast vers le bas pour détecter le sol
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    void Update()
    {
        // Gère les mouvements horizontaux
        float horizontal = Input.GetAxisRaw(horizontalAxis);
        float vertical = Input.GetAxisRaw(verticalAxis);
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized * moveSpeed;

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection.Normalize();

        if (movementDirection != Vector3.zero)
        {
            // Calcule la rotation cible en fonction de la direction du mouvement
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            // Tourne progressivement l'objet vers la rotation cible
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                toRotation, 
                rotationSpeed * Time.deltaTime
            );
        }

        // Déplace le joueur
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // Gère le saut
        if (Input.GetButtonDown(jumpButton) && IsGrounded())
        {
            rb.velocity = Vector3.up * jumpForce;
            Debug.Log("Le personnage saute.");
        }
    }

    public void UpdateLife(int value)
    {
        life += value;
        if(life > maxLife)
        {
            life = maxLife;
        }
        
        if(life < 0)
        {
            life = 0;
        }

        if(life <= 0)
        {
            Debug.Log("Game Over");        // Game Over
            gameScript.IsInGame = false; 
        }
    }

    public void PerdreVie()
    {

    }
}