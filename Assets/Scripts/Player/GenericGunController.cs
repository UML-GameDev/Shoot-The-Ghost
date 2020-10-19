using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericGunController<T> : InputUser
 where T : ShooterData
{
    Shooter<T> shooter;


    void Awake()
    {
        shooter = GetComponent<Shooter<T>>();
    }

    void Start()
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
