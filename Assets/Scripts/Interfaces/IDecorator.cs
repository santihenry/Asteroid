using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDecorator<T>  //MyA1-P2
{
    void Execute(T data);
    void Stop(T data);
}
