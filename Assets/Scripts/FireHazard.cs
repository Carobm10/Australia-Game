using UnityEngine;

public class FireHazard : MonoBehaviour
{
    [Header("Daño")]
    public float damage = 25f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() == null)
            return;

        TimeLifeManager lifeManager =
            FindFirstObjectByType<TimeLifeManager>();

        if (lifeManager != null)
        {
            lifeManager.RemoveLife(damage);
        }
    }
}