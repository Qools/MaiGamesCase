using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectablePusher : MonoBehaviour
{
    [SerializeField] private Transform pusher;

    [SerializeField] private float pushForce;

    private float pusherStarPosition;

    private void Start()
    {
        pusherStarPosition = pusher.localPosition.y;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if (rigidbody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();
            rigidbody.AddForceAtPosition(forceDirection * pushForce, transform.position, ForceMode.Impulse);
        }
    }

    private void Push()
    {
        pusher.DOLocalMoveY(-1f, 1.5f);
    }

    private void ResetPusherPosition()
    {
        pusher.DOLocalMoveY(pusherStarPosition, 0.1f);
    }

    private void OnEnable()
    {
        EventSystem.OnStageEnter += Push;
        EventSystem.OnStageExit += ResetPusherPosition;
    }

    private void OnDisable()
    {
        EventSystem.OnStageEnter -= Push;
        EventSystem.OnStageExit -= ResetPusherPosition;
    }
}
