using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttributes", menuName = "PlayerAttributes")]
public class PlayerAttributes : ScriptableObject
{
    public float xClamp;
    public float mouseSensitivity;
    public float movementSpeed;
}
