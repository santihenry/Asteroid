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
    public float inmuneDuration = 3;
    public bool test = false;

    public Weapons weapon;
    public List<Weapons> weapons = new List<Weapons>();
    public int currentWeapon = 0;


    public bool powerUp, powerUpSpeed;

    public List<IObserver> allObservers = new List<IObserver>();

    public GameObject deathFX,turbo,forceField;

    public GameObject fbx;

    public float time;
    public float timeSpeed;
    float timeWithPowerUp = 6f;
    float timeWithSpeedMax = 3f;


    public Dictionary<KeyCode, ICommand> keysCommands = new Dictionary<KeyCode, ICommand>();
    public List<ICommand> allCommands = new List<ICommand>();

    public SpeedPowerUpDecorator speedDecorator;

    /*
    public Transform playerTrans;
    public Command buttonW, buttonS, buttonA, buttonD, buttonB, buttonZ, buttonR,shootButton,nextWeaponButton, prevtWeaponButton;
    public static List<Command> oldCommands = new List<Command>();
    public Coroutine replayCoroutine;
    public static bool shouldStartReplay;
    public bool isReplaying;*/

    private Vector3 startPos;


    public Collider col;

    public List<AudioClip> allSounds;
    private AudioSource source;

    public AudioSource Source
    {
        get
        {
            return source;
        }
        set 
        {
            source = value;
        } 
    }

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

    public float TimeWithPowerUp
    {
        get
        {
            return timeWithPowerUp;
        }
        set
        {
            timeWithPowerUp = value;
        }
    }
    public float TimeWithSpeedMax
    {
        get
        {
            return timeWithSpeedMax;
        }
        set
        {
            timeWithSpeedMax = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

    }

}
