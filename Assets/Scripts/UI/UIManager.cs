using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/*
 * UIManager:
 *      This class handles input for any interaction between user and UI   
 *      
 *      This class inherit GameInput.IUIAction, which includes any callbacks necessary to implement
 *      and inherit MonoBehaviour so it can attach to GameObject
 *
 *      This class should attach to Empty gameObject
 */

public class UIManager: MonoBehaviour, GameInput.IUIActions
{
    //Events to invokes the when the specific player callbacks are call
    public UnityAction<int> switchWeaponUIEvent;
    public UnityAction<int> switchPlayerWeaponEvent;

    GameInput gameInput;

    //Global variable: size of inventory and currentIndex
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
        //We reading vector2 because mouse scroll can be either vertical or "Horiztonal", and we want vertical input
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
