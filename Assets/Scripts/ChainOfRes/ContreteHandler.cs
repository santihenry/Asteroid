using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContreteHandler : IHandler<Bullet>
{

    IHandler<Bullet> _next;


    public IHandler<Bullet> SetNext(IHandler<Bullet> next)
    {
        _next = next;
        return _next;
    }
    public Bullet handle(Bullet b)
    {
        throw new System.NotImplementedException();
    }

}
