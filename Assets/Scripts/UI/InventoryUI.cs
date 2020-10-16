using System.Collections.Generic;
using UnityEngine;

public class InventoryUI: MonoBehaviour
{
    public InventoryManager inventoryM;
    public GameObject[] items;
    int previousIndex = 0;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        inventoryM.switchWeaponUIEvent += UpdateSelected;
        items[0].SetActive(true);
        for(int i = 1; i < items.Length; ++i){
            items[i].SetActive(false);
        }
        previousIndex = 0;
    }

    void OnDisable(){
        inventoryM.switchWeaponUIEvent -= UpdateSelected; 
        for(int i = 0; i < items.Length; ++i){
            items[i].SetActive(false);
        }
    }

    void UpdateSelected(int index){
        items[previousIndex].SetActive(false);
        items[index].SetActive(true);
        previousIndex = index;
    }
}
