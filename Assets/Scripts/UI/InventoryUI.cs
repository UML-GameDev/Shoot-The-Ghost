using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InventoryUI: MonoBehaviour
{
    public InventoryManager inventoryM;
    //TODO(MAYBE?)-Change this to accept arr of gameObject(itemsBorder)and iterate and get list of border object seraching child of the gameobject
    public GameObject[] itemsBorder;
    int previousIndex = 0;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        inventoryM.switchWeaponUIEvent += UpdateSelected;

        for(int i = 1; i < itemsBorder.Length; ++i){
            if (i == 0) itemsBorder[i].SetActive(true);
            else        itemsBorder[i].SetActive(false);
        }
        previousIndex = 0;
    }

    void OnDisable(){
        inventoryM.switchWeaponUIEvent -= UpdateSelected; 
        for(int i = 0; i < itemsBorder.Length; ++i){
            itemsBorder[i].SetActive(false);
        }
    }

    void UpdateSelected(int index){
        itemsBorder[previousIndex].SetActive(false);
        itemsBorder[index].SetActive(true);
        
        previousIndex = index;
    }
}
