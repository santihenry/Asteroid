using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;


public class ShipModel : MonoBehaviour
{
    public float speed = 60;
    public float currentV = 0;
    public float currentH = 0;
    public float turnLeftRigth = 0;
    public float interpolation = 50;
    public float turnSpeed = 200;

    public float aceleration;
    public float dirHTwo;
    public float dirV;
    public float dirH;


    public bool death = false;
    public float respawnTime = 2f;
    public float currentTime;

    public Weapons weapon;
    public List<Weapons> weapons = new List<Weapons>();
    public int currentWeapon = 0;

    public List<IObserver> allObservers = new List<IObserver>();


    public Transform playerTrans;
    public Command buttonW, buttonS, buttonA, buttonD, buttonB, buttonZ, buttonR;
    public static List<Command> oldCommands = new List<Command>();
    private Vector3 startPos;
    public Coroutine replayCoroutine;
    public static bool shouldStartReplay;
    public bool isReplaying;

    public Vector3 StartPos
    {
        get
        {
            return startPos;
        }
        set
        {
            startPos = value;
        }
    }

}
