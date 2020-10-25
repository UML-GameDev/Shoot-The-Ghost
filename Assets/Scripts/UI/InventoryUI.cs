using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

/*
 * InventoryUI
 *      This class handles the UI of the Inventory
 *      
 *      This class inherits MonoBeahviour so it can attach to gameObject
 *      
 *      This class should attach to a gameObject where it hold Slot Prefab as a child
 */
public class InventoryUI: MonoBehaviour
{
    public UIManager inventoryM;

    public GameObject[] items;
    private GameObject[] borders;
    private GameObject[] ammoTexts;
    int previousIndex = 0;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        inventoryM.switchWeaponUIEvent += UpdateSelected;

        //If the borders is not declared yet, declared it with size of items
        if (borders == null) borders = new GameObject[items.Length];
        if (ammoTexts == null) ammoTexts = new GameObject[items.Length];

        for(int i = 0; i < items.Length; ++i){
            //If it's not declared, find the gameObject with name Border and assign to gameObject
            if (borders[i] == null) borders[i] = items[i].transform.Find("Border").gameObject;
            if (ammoTexts[i] == null) ammoTexts[i] = items[i].transform.Find("Count").gameObject;

            //Set Active the first items
            if (i == 0)
            {
                borders[i].SetActive(true);
                ammoTexts[i].SetActive(true);
            }
            else
            {
                borders[i].SetActive(false);
                ammoTexts[i].SetActive(false);
            }
        }
        previousIndex = 0;
    }

    void OnDisable(){
        inventoryM.switchWeaponUIEvent -= UpdateSelected; 
        for(int i = 0; i < borders.Length; ++i){
            borders[i].SetActive(false);
            ammoTexts[i].SetActive(false);
        }
    }

    public void UpdateSelected(int index){
        borders[previousIndex].SetActive(false);
        ammoTexts[previousIndex].SetActive(false);
        borders[index].SetActive(true);
        ammoTexts[index].SetActive(true);

        previousIndex = index;
    }
}
