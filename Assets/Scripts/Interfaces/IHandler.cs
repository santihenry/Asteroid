using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandler   //MyA1-P5
{
    IHandler SetNext(IHandler nextHandler);
    object Handle();
}
