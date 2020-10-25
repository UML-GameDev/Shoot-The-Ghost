using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * HealthUpdatable
 *      A interface that provides UnityEvent, currentHealth, and TakeDamage function
 *      Attached gameObject can be use to update the health bar of attached object
 */ 
public interface HealthUpdatable
{
    UnityEvent<float> OnHealthUpdated {get; }
    float currHealth { get; set; }

    void TakeDamage(float damage);
}
