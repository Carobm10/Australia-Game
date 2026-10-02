using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn")]
    public Transform respawnPoint;
    public float respawnDelay = 1f;

    private Rigidbody2D rb;
    private bool isRespawning = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

        // Detenemos al jugador
        rb.linearVelocity = Vector2.zero;

        // Esperamos un momento
        yield return new WaitForSeconds(respawnDelay);

        // Lo devolvemos al checkpoint
        transform.position = respawnPoint.position;

        rb.linearVelocity = Vector2.zero;

        isRespawning = false;
    }
}