using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 6f;
    public float jumpForce = 7f;

    [Header("Deteccion de suelo")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Empujar")]
    public LayerMask pushableLayer;

    private Rigidbody2D rb;
    private Animator animator;

    private float horizontal;
    private bool isGrounded;
    private bool isPushing;

    void Awake()
    {
        // Rigidbody2D está en Player
        rb = GetComponent<Rigidbody2D>();

        // Animator está en el hijo KoalaVisual
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // =========================
        // MOVIMIENTO HORIZONTAL
        // =========================

        horizontal = 0f;

        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            horizontal = -1f;
        }

        if (Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal = 1f;
        }


        // =========================
        // DETECCION DE SUELO
        // =========================

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );


        // =========================
        // SALTO
        // =========================

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );
        }


        // =========================
        // ANIMACIONES
        // =========================

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontal));

            animator.SetBool("IsGrounded", isGrounded);

            animator.SetBool("IsPushing", isPushing);
        }


        // =========================
        // GIRAR PERSONAJE
        // =========================

        if (horizontal != 0)
        {
            Vector3 scale = transform.localScale;

            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(horizontal);

            transform.localScale = scale;
        }
    }


    void FixedUpdate()
    {
        // Movimiento físico
        rb.linearVelocity = new Vector2(
            horizontal * moveSpeed,
            rb.linearVelocity.y
        );
    }


    // =========================
    // DETECTAR CAJA EMPUJABLE
    // =========================

    private void OnCollisionStay2D(Collision2D collision)
    {
        bool esEmpujable =
            (pushableLayer.value & (1 << collision.gameObject.layer)) != 0;

        if (esEmpujable)
        {
            isPushing = horizontal != 0 && isGrounded;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        bool esEmpujable =
            (pushableLayer.value & (1 << collision.gameObject.layer)) != 0;

        if (esEmpujable)
        {
            isPushing = false;
        }
    }


    // =========================
    // VISUALIZAR GROUNDCHECK
    // =========================

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.DrawWireSphere(
                groundCheck.position,
                groundCheckRadius
            );
        }
    }
}