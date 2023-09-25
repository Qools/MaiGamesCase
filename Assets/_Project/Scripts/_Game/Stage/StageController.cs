using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageController : MonoBehaviour
{

    [SerializeField] private Transform elevator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerPrefKeys.player))
        {
            EventSystem.CallStageEnter();

            //DOVirtual.DelayedCall(2f, RiseElevator);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(PlayerPrefKeys.player))
        {
            
        }
    }

    private void RiseElevator()
    {
        elevator.DOMoveY(0f, 2f);
    }
}
