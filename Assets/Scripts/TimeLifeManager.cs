using UnityEngine;
using UnityEngine.UI;

public class TimeLifeManager : MonoBehaviour
{
    [Header("Vida / Tiempo")]
    public float maxLife = 100f;
    public float currentLife = 100f;

    [Header("Velocidades")]
    public float futureDrainRate = 10f;

    [Header("Referencias")]
    public TimeManager timeManager;
    public Slider timeBar;

    private bool isDead = false;

    void Start()
    {
        currentLife = maxLife;

        if (timeBar != null)
        {
            timeBar.maxValue = maxLife;
            timeBar.value = currentLife;
        }
    }

    void Update()
    {
        if (isDead || timeManager == null)
            return;

        if (!timeManager.isPresent)
        {
            DrainLife();
        }

        UpdateTimeBar();

        if (currentLife <= 0)
        {
            Die();
        }
    }

    void DrainLife()
    {
        currentLife -= futureDrainRate * Time.deltaTime;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);
    }

    void UpdateTimeBar()
    {
        if (timeBar != null)
        {
            timeBar.value = currentLife;
        }
    }

    void Die()
    {
        if (isDead)
            return;

        isDead = true;
        currentLife = 0;

        PlayerRespawn playerRespawn = FindFirstObjectByType<PlayerRespawn>();

        if (playerRespawn != null)
        {
            playerRespawn.Die();
        }

        Invoke(nameof(ResetLife), 1.1f);
    }
    void ResetLife()
    {
        currentLife = maxLife;
        isDead = false;

        if (timeBar != null)
        {
            timeBar.value = currentLife;
        }
    }

    public void AddLife(float amount)
    {
        if (isDead)
            return;

        currentLife += amount;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);

        if (timeBar != null)
        {
            timeBar.value = currentLife;
        }
    }

    public void RemoveLife(float amount)
    {
        if (isDead)
            return;

        currentLife -= amount;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);

        if (timeBar != null)
        {
            timeBar.value = currentLife;
        }

        if (currentLife <= 0)
        {
            Die();
        }
    }
}