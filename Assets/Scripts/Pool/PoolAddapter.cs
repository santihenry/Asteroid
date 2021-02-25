using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PoolAddapter<T>        //MyA1-P1
{

    Pool pool;

    public PoolAddapter(Func<IPoolObject> createFunction, Action<IPoolObject> disableFunction, int initialQuantity)
    {
        pool = new Pool(createFunction, disableFunction, initialQuantity);
    }

    public T GetObject()
    {
        var obj = pool.AcquireObject();
        return (T)obj;
    }

    public void ReturnObject(T obj)
    {
        pool.ReleaseObject((IPoolObject)obj);

    }
}
