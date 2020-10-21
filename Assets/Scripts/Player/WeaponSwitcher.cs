using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSwitcher : MonoBehaviour
{

    public UnityEvent<IEquippableItem> OnEquipSwitched = new UnityEvent<IEquippableItem>();
    public GameObject currentWeapon;
    public InputManager input;


    void Awake()
    {
        var inputUser = currentWeapon.GetComponent<InputUser>();
        
        inputUser.input = this.input;

        currentWeapon.SetActive(true);
        inputUser.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
