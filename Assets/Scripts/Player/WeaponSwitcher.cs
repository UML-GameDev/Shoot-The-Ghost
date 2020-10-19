using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSwitcher : MonoBehaviour
{

    public UnityEvent<EquippableItem> OnEquipSwitched = new UnityEvent<EquippableItem>();
    public GameObject currentWeapon;
    public InputManager input;


    void Awake()
    {
        var inputUser = currentWeapon.GetComponent<InputUser>();
        
        inputUser.input = this.input;
        inputUser.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
