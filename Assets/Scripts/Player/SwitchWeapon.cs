using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchWeapon : MonoBehaviour
{
    public InventoryManager inventoryM;
    public InputManager inputM;
    public GameObject [] weapons;
    public UnityEvent<bool> [] actions;

    private int previousIndex = 0;
    
    void OnEnable() {
        inventoryM.switchPlayerWeaponEvent += Switch;
        inputM.attackEvent = actions[0];
        //Not Necessary?
        weapons[0].SetActive(true);
        for(int i = 1; i < weapons.Length; ++i){
              weapons[i].SetActive(false);
        }
    }

    void OnDisable() {
        inventoryM.switchPlayerWeaponEvent -= Switch;
        inputM.attackEvent = null;
    }

    void Switch(int index){
       inputM.attackEvent = actions[index];

       weapons[previousIndex].SetActive(false);
       weapons[index].SetActive(true);

       previousIndex = index;
    }
}
