using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public interface HealthUpdatable
{
    UnityEvent<float> OnHealthUpdated {get; }
    float currHealth { get; set; }

    void TakeDamage(float damage);
}
