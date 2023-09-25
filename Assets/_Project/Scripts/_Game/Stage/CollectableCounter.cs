using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableCounter : MonoBehaviour
{
    [SerializeField] private TextMeshPro collectableText;

    [SerializeField] private int requiredCollectable;
    public int collectedCollectable;

    private void Start()
    {
        collectedCollectable = 0;

        SetText(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerPrefKeys.collectable))
        {
            collectedCollectable++;
            other.gameObject.SetActive(false);

            SetText(collectedCollectable);
        }
    }

    private void SetText(int _value)
    {
        string tempText = _value.ToString() + " / " + requiredCollectable.ToString();
        collectableText.text = tempText;
    }
}
