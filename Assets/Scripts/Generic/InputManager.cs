using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputManager : MonoBehaviour,  GameInput.IPlayerActions
{
    public UnityAction<Vector2> moveEvent;
    public UnityAction<Vector2> lookEvent; //aim
    public UnityAction jumpEvent;
    public UnityAction<bool> attackEvent;
    // public UnityAction interactEvent;
    // public UnityAction pauseEvent;

    GameInput gameInput;

    void OnEnable(){
        //(only create one instance of this object)
        if(gameInput == null){
            gameInput = new GameInput();
            gameInput.Player.SetCallbacks(this);
        }
        gameInput.Player.Enable();
    }
    void OnDisable(){
        gameInput.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context){
        if(moveEvent != null){
             moveEvent.Invoke(context.ReadValue<Vector2>());
        }
    }
    public void OnLook(InputAction.CallbackContext context){
        if(lookEvent != null){
            lookEvent.Invoke(context.ReadValue<Vector2>());
        }
    }
    public void OnFire(InputAction.CallbackContext context){
        if(attackEvent != null){ 
            switch(context.phase){
                case InputActionPhase.Started:
                    attackEvent.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    attackEvent.Invoke(false);
                    break;
            }
            
        }
    }
    public void OnJump(InputAction.CallbackContext context){
        if(jumpEvent != null && context.phase == InputActionPhase.Started){
            jumpEvent.Invoke();
        }
    }
}
