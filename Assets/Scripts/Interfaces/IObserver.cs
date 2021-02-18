using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver 
{
    void OnNotify(string eventName);


    /*void OnNotify(string eventName, int score);
    void OnNotify(string eventName, int score, int indexSoundClip);*/
}
