using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private Collider triggerCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerPrefKeys.player))
        {
            triggerCollider.enabled = false;

            EventSystem.CallStageEnter();
            EventSystem.CallGameOver(GameResult.Win);
        }
    }
}
