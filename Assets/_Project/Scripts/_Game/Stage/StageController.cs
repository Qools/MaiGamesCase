using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageController : MonoBehaviour
{
    [SerializeField] private CollectableCounter collectableCounter;

    [SerializeField] private Transform elevator;
    [SerializeField] private Collider stageTriggerCollider;
    [SerializeField] private Collider elvetorCollider;

    [SerializeField] private Transform rightGate;
    [SerializeField] private Transform leftGate;

    [SerializeField] private float timeToCollect;
    private bool isCompleted = true;
    private bool waitForCollectables = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerPrefKeys.player))
        {
            EventSystem.CallStageEnter();

            DOVirtual.DelayedCall(timeToCollect, () => WaitForCollectables());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(PlayerPrefKeys.player))
        {
            CheckCollectables();
        }
    }

    private void CheckCollectables()
    {
        if (collectableCounter.collectedCollectable >= collectableCounter.requiredCollectable)
        {
            if (isCompleted)
            {
                RiseElevator();
                OpenGates();

                isCompleted = false;
                stageTriggerCollider.enabled = false;
            }
        }
        else if (waitForCollectables && collectableCounter.collectedCollectable < collectableCounter.requiredCollectable)
        {
            EventSystem.CallGameOver(GameResult.Lose);
        }
    }

    private void RiseElevator()
    {
        elvetorCollider.enabled = false;

        elevator.DOMoveY(0f, 2f).OnComplete(()=> EventSystem.CallStageExit());
    }

    private void OpenGates()
    {
        rightGate.DORotate(Vector3.zero, 2f);
        leftGate.DORotate(Vector3.zero, 2f);
    }

    private void WaitForCollectables()
    {
        waitForCollectables = true;
    }
}
