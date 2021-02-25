using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsHandler : MonoBehaviour,IHandler
{
    IHandler _nextHandler;

    public IHandler SetNext(IHandler handler)
    {
        this._nextHandler = handler;
        return handler;
    }

    public virtual object Handle()
    {
        if (this._nextHandler != null)
        {
            return this._nextHandler.Handle();
        }
        else
        {
            return null;
        }
    }
}
