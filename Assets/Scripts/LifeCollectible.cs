using UnityEngine;

public class LifeCollectible : MonoBehaviour
{
    [Header("Vida que recupera")]
    public float lifeAmount = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() == null)
            return;

        TimeLifeManager lifeManager =
            FindFirstObjectByType<TimeLifeManager>();

        if (lifeManager != null)
        {
            lifeManager.AddLife(lifeAmount);
            Destroy(gameObject);
        }
    }
}