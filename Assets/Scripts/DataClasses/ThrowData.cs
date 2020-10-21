using UnityEngine;

[CreateAssetMenu(fileName = "ThrowData", menuName = "ScriptableObjects/ThrowData", order = 1)]
public class ThrowData : ScriptableObject
{
    public int maxAmmo = 6;
    public float chargeDuration = 3.0f;
    public float chargeInterval = 0.1f;
    public Vector2 minForce = new Vector2(10,10);
    public Vector2 maxForce = new Vector2(100, 100);
    //maybe multiplier?

}
