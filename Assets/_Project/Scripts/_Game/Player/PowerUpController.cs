using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    [SerializeField] private GameObject leftProp;
    [SerializeField] private GameObject rightProp;

    private bool isProsActive;

    private float timer;

    private void Start()
    {
        leftProp.SetActive(false);
        rightProp.SetActive(false);

        timer = 0f;

        isProsActive = false;
    }

    private void Update()
    {
        if (!isProsActive) 
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= playerAttributes.powerUpTime)
        {
            DisableProps();
        }
    }

    private void OnEnable()
    {
        EventSystem.OnPowerUpPickUp += EnableProps;
    }

    private void OnDisable()
    {
        EventSystem.OnPowerUpPickUp -= EnableProps;
    }

    private void EnableProps()
    {
        timer = 0f;

        isProsActive = true;

        rightProp.SetActive(true);
        leftProp.SetActive(true);
    }

    private void DisableProps()
    {
        isProsActive = false;

        timer = 0f;

        leftProp.SetActive(false);
        rightProp.SetActive(false);
    }
}
