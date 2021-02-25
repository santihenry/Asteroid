using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolObject
{
    void OnAcquire();
    void OnDispose();
}
