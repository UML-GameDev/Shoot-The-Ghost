using UnityEngine;

[CreateAssetMenu(fileName = "ThrowData", menuName = "ScriptableObjects/ThrowData", order = 1)]
public class ThrowData : GunData 
{
    public Vector2 throwForce;
    //maybe multiplier?
}
