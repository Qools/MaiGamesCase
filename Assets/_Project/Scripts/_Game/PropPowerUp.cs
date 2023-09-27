using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropPowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerPrefKeys.player))
        {
            EventSystem.CallPowerUpPickeUp();

            gameObject.SetActive(false);
        }
    }
}
