using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand  //MyA1-P3
{
    void Execute(GameObject obj);    
    void Undo(GameObject obj);
    void Init(GameObject obj);

}
