using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletData", order = 1)]
public class BulletData : ScriptableObject
{
    public float bulletSpeed = 1f;
    public float lifeDuration = 2f;
}