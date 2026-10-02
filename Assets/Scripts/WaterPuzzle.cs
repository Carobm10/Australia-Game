using UnityEngine;

public class WaterPuzzle : MonoBehaviour
{
    [Header("Objetos que desaparecen en el futuro")]
    public GameObject[] objectsToDisable;

    [Header("Objetos que aparecen en el futuro")]
    public GameObject[] objectsToEnable;

    private bool solved = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (solved)
            return;

        if (other.GetComponent<PlayerController>() != null)
        {
            SolvePuzzle();
        }
    }

    void SolvePuzzle()
    {
        solved = true;

        foreach (GameObject obj in objectsToDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        foreach (GameObject obj in objectsToEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        Debug.Log("PUZZLE RESUELTO - EL FUTURO CAMBIÓ");

        // El objeto se consume al recogerlo
        gameObject.SetActive(false);
    }
}