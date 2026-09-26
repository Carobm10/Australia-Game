using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{
    [Header("Mundos")]
    public GameObject presentWorld;
    public GameObject futureWorld;

    [Header("Estado")]
    public bool isPresent = true;

    void Start()
    {
        UpdateWorld();
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
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
        presentWorld.SetActive(isPresent);
        futureWorld.SetActive(!isPresent);

        if (isPresent)
        {
            Debug.Log("PRESENTE");
        }
        else
        {
            Debug.Log("FUTURO");
        }
    }
}