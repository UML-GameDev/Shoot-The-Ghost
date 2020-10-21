using UnityEngine;
public enum WeaponType { SHOOT, THROWABLE };//We can add more;
public abstract class WeaponBase : MonoBehaviour
{
    public WeaponBase(WeaponType type) { this.type = type; }
    protected WeaponType type;
    protected bool isHolding = false;
    public abstract void AttackState(bool hold);
}
