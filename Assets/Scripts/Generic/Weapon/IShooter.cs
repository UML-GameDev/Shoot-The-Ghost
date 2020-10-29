using UnityEngine.Events;

/*
 * IShooter
 *  Interface that use for all the weapon
 */
//public enum WeaponType { SHOOT, THROWABLE };//We can add more;
public interface IShooter
{
    UnityEvent OnFinished { get; set; }
    void SetHolding(bool isHolding);
    void Reload();
}
