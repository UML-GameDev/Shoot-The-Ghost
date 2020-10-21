using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IShooter
{
    UnityEvent OnFinished { get; set; }

    void setHolding(bool isHolding);
}
