using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;

public class ShipController : MonoBehaviour, IObservable
{
    ShipModel _model;

    private void Awake()
    {      
        _model = GetComponent<ShipModel>();
        _model.Source = GetComponent<AudioSource>();
        _model.weapons.Add(GetComponentInChildren<MachineGun>());
        _model.weapons.Add(GetComponentInChildren<RocketGun>());
    }

    private void Start()
    {
        _model.buttonW = new MoveFoward();
        _model.buttonA = new MoveLeft();
        _model.buttonD = new MoveRight();
        _model.buttonZ = new UndoCommand();
        _model.buttonR = new ReplayCommand();

        _model.weapon = _model.weapons[0];


        _model.renderer = gameObject.GetComponentsInChildren<Renderer>();
        _model.col = gameObject.GetComponent<Collider>();

    }

    void Update()
    {

        _model.currentTime += Time.deltaTime;
        LoseLife();
        if (Time.deltaTime == 0 || _model.death) return;

        if (!_model.isReplaying)
            HandleInput();

        StartReplay();

        if (Input.GetKey(KeyCode.Space))
        {
            _model.weapon.Shoot();
        }

        ChangeWeapon();
    }



    public void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {           
            _model.currentWeapon--;
            if (_model.currentWeapon < 0)
                _model.currentWeapon = _model.weapons.Count-1;           
        }
        if (Input.GetKeyDown(KeyCode.E))
        {          
             _model.currentWeapon++;
            if (_model.currentWeapon > _model.weapons.Count-1)
                _model.currentWeapon = 0;
        }
        _model.weapon = _model.weapons[_model.currentWeapon];
    }
    


    /*
    public void Movement()
    {

        model.aceleration = Input.GetAxisRaw("Vertical");
        model.dirHTwo = Input.GetAxisRaw("Horizontal");
        model.dirV = 0;
        model.dirH = 0;

        model.currentV = Mathf.Lerp(model.currentV, model.dirV, model.interpolation * Time.deltaTime);
        model.currentH = Mathf.Lerp(model.currentH, model.dirH, model.interpolation * Time.deltaTime);
        model.turnLeftRigth = Mathf.Lerp(model.turnLeftRigth, model.dirHTwo, model.interpolation * Time.deltaTime);

        transform.Rotate(model.currentV * model.turnSpeed * Time.deltaTime, model.turnLeftRigth * model.turnSpeed * Time.deltaTime, model.currentH * model.turnSpeed * Time.deltaTime);

        if (model.aceleration > 0)
        {
            transform.position += transform.forward * model.speed * Time.deltaTime;
        }
    }
    */


    private void HandleInput()
    {
        float Aceleration = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.A))
        {
            _model.buttonA.Execute(_model.playerTrans, _model.buttonA);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _model.buttonD.Execute(_model.playerTrans, _model.buttonD);
        }
        if (Aceleration > 0)
        {
            _model.buttonW.Execute(_model.playerTrans, _model.buttonW);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            _model.buttonZ.Execute(_model.playerTrans, _model.buttonZ);
        }

        if (Aceleration > 0)
        {
            if (!_model.Source.isPlaying || _model.Source.clip != _model.allSounds[Sounds.movementTurbo])
            {
                _model.Source.clip = _model.allSounds[Sounds.movementTurbo];
                _model.Source.Play();
            }
        }
    }

    private void StartReplay()
    {

        if (ShipModel.shouldStartReplay && ShipModel.oldCommands.Count > 0)
        {
            ShipModel.shouldStartReplay = false;

            if (_model.replayCoroutine != null)
            {
                StopCoroutine(_model.replayCoroutine);
            }
            _model.replayCoroutine = StartCoroutine(ReplayCommands(_model.playerTrans));
        }
    }

    IEnumerator ReplayCommands(Transform boxTrans)
    {
        _model.isReplaying = true;
        boxTrans.position = _model.StartPos;

        for (int i = 0; i < ShipModel.oldCommands.Count; i++)
        {
            ShipModel.oldCommands[i].Move(boxTrans);
            yield return new WaitForSeconds(0.3f);
        }
        _model.isReplaying = false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Asteroids>())
        {
            _model.currentTime = 0f;
            Notify("LoseLife");
            _model.death = true;
        }
    }


    private void LoseLife()
    {
        if (_model.death)
        {
            if (!_model.Source.isPlaying || _model.Source.clip != _model.allSounds[Sounds.loseLife])
            {
                _model.Source.clip = _model.allSounds[Sounds.loseLife];
                _model.Source.Play();
            }

            _model.speed = 0;
            for (int i = 0; i < _model.renderer.Length; i++)
            {
                _model.renderer[i].enabled = false;
                _model.col.enabled = false;
            }

            if (_model.respawnTime - _model.currentTime < 0)
            {
                for (int i = 0; i < _model.renderer.Length; i++)
                {
                    _model.renderer[i].enabled = true;
                    _model.col.enabled = true;
                }
                _model.speed = 60;
                _model.respawnTime = 2;
                _model.death = false;             
            }
        }
    }

    public void Notify(string eventName)
    {
        for (int i = 0; i < _model.allObservers.Count; i++)
        {
            _model.allObservers[i].OnNotify(eventName);
        }
    }

    public void SubEvent(IObserver obs)
    {
        if (!_model.allObservers.Contains(obs)) _model.allObservers.Add(obs);
    }

    public void UnSubEvent(IObserver obs)
    {
        if (_model.allObservers.Contains(obs))
            _model.allObservers.Remove(obs);
    }
}
