using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunController : InputUser
{
    IShooter shooter;


    void Awake()
    {
        shooter = GetComponent<IShooter>();
    }

    void Start()
    {

    }

    void OnDisable()
    {

    }
    
    void OnEnable()
    {
        input.attackEvent += shooter.setHolding;

        shooter.OnFinished.AddListener(() =>
        {
            input.attackEvent -= shooter.setHolding;
        });
    }
}
