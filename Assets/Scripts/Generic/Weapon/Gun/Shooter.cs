using UnityEngine;
using UnityEngine.Events;

public abstract class Shooter<T> : MonoBehaviour, IShooter
where T : ShooterData
{
    public T shooterData;
    float timeToFire;

    public UnityEvent OnFinished { get; set; } = new UnityEvent();

    protected bool isHolding;

    void OnDisable()
    {
        OnFinished.Invoke();
    }

    void  OnDestroy()
    {
        OnFinished.Invoke();
    }

    public void SetHolding(bool isHolding)
    {
        this.isHolding = isHolding;
    }
    protected void CheckShouldShoot()
    {
        if(isHolding && Time.time > timeToFire)
        {
            timeToFire = Time.time + 1/shooterData.fireRate;
            Shoot();
        }
    }

    public abstract void Shoot();
    public abstract void Reload();
}
