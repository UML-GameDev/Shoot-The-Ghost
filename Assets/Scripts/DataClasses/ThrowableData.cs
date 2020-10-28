using UnityEngine;

[CreateAssetMenu(fileName = "ThrowableData",menuName = "ScriptableObjects/ThrowableData", order=1)]
public class ThrowableData: ScriptableObject
{
    public float lifeDuration = 3f;
    public float damage = 20f;
    public float effectRadius = 5f;
}