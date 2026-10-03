using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{
    [Header("Mundos")]
    public GameObject presentWorld;
    public GameObject pastWorld;

    [Header("Estado actual")]
    public bool isPresent = true;

    void Start()
    {
        // El juego siempre comienza en el PRESENTE
        isPresent = true;
        UpdateWorld();
    }

    void Update()
    {
        // C cambia entre PRESENTE y PASADO
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            SwitchTime();
        }
    }

    public void SwitchTime()
    {
        isPresent = !isPresent;
        UpdateWorld();
    }

    void UpdateWorld()
    {
        if (presentWorld != null)
        {
            presentWorld.SetActive(isPresent);
        }

        if (pastWorld != null)
        {
            pastWorld.SetActive(!isPresent);
        }

        if (isPresent)
        {
            Debug.Log("PRESENTE - BOSQUE QUEMADO");
        }
        else
        {
            Debug.Log("PASADO - BOSQUE VIVO");
        }
    }

    // Nos permitirá consultar fácilmente en qué época estamos
    public bool IsPresent()
    {
        return isPresent;
    }

    public bool IsPast()
    {
        return !isPresent;
    }
}