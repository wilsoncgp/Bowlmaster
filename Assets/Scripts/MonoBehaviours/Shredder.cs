using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour
{
    void OnTriggerExit(Collider collider)
    {
        // Destroy pins exiting the Pin Setter's collider
        Pin pin = collider.GetComponentInParent<Pin>();
        if (pin)
        {
            Destroy(pin.gameObject);
        }
    }
}
