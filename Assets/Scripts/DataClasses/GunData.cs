using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GunData", menuName = "ScriptableObjects/GunData", order = 1)]

public class GunData : ShooterData
{
    public int maxAmmo;
    public float reloadTime;
}
