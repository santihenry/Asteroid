using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandler<T> 
{

    IHandler<T> SetNext(IHandler<T> next);
    T handle(T b);
}
