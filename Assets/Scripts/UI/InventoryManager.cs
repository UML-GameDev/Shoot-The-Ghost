using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InventoryManager: MonoBehaviour, GameInput.IUIActions
{
    public UnityAction<int> switchWeaponUIEvent;
    public UnityAction<int> switchPlayerWeaponEvent;
    GameInput gameInput;
    [HideInInspector]public int currentIndex = 0;
    [HideInInspector]public int size = 3;

    void OnEnable(){
        //(only create one instance of this object)
        if(gameInput == null){
            gameInput = new GameInput();
            gameInput.UI.SetCallbacks(this);
        }
        gameInput.UI.Enable();
    }

    void OnDisable(){
        gameInput.UI.Disable();
    }

    public void OnSwitchInventory(InputAction.CallbackContext context){
        Vector2 offset = context.ReadValue<Vector2>();
        if(offset == new Vector2(0,0)) return;
        currentIndex -= (int)offset.y;

        if(currentIndex < 0) currentIndex = size-1;
        else if(currentIndex >= size) currentIndex = 0;

        //switch border in inventory UI
        if(switchWeaponUIEvent != null){
            //Debug.Log(context.ReadValue<Vector2>());
            switchWeaponUIEvent.Invoke(currentIndex);
        }
        //switch player weapons sprite and attackEvent;
        if(switchPlayerWeaponEvent != null){
            switchPlayerWeaponEvent.Invoke(currentIndex);
        }
    }
}
