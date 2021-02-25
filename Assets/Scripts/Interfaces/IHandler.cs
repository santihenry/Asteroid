using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandler
{
    IHandler SetNext(IHandler nextHandler);
    object Handle();
}
