using UnityEngine.Events;

/*
 * IShooter
 * 
 * 
 */ 
public interface IShooter
{
    UnityEvent OnFinished { get; set; }

    void setHolding(bool isHolding);
}
