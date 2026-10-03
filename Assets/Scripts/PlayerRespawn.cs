using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn")]
    public Transform respawnPoint;
    public float respawnDelay = 1f;

    private Rigidbody2D rb;
    private Animator animator;
    private PlayerController playerController;

    private bool isRespawning = false;
    private RigidbodyConstraints2D originalConstraints;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();

        // Guardamos las restricciones normales del koala
        originalConstraints = rb.constraints;
    }

    public void Die()
    {
        if (!isRespawning)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    IEnumerator RespawnRoutine()
    {
        isRespawning = true;

        // 1. Quitamos el control al jugador
        if (playerController != null)
            playerController.enabled = false;

        // 2. Detenemos completamente al koala
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        // 3. Reproducimos la animación de muerte
        if (animator != null)
            animator.SetTrigger("Dead");

        // 4. Esperamos mientras se reproduce la muerte
        yield return new WaitForSeconds(respawnDelay);

        // 5. Lo llevamos al RespawnPoint
        transform.position = respawnPoint.position;

        // 6. Restauramos su física
        rb.constraints = originalConstraints;
        rb.linearVelocity = Vector2.zero;

        // 7. Reactivamos el control
        if (playerController != null)
            playerController.enabled = true;

        isRespawning = false;
    }
}