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
        _model.weapons.Add(GetComponentInChildren<Granadas>());


        _model.keysCommands.Add(KeyCode.W, new FowardCommand());
        _model.keysCommands.Add(KeyCode.A, new RightCommand());
        _model.keysCommands.Add(KeyCode.D, new LeftCommand());
        _model.keysCommands.Add(KeyCode.Space, new ShootCommand());

        _model.keysCommands[KeyCode.Space].Init(_model.gameObject);
       
    }

    private void Start()
    {
        /*
        _model.buttonW = new MoveFoward();
        _model.buttonA = new MoveLeft();
        _model.buttonD = new MoveRight();
        _model.buttonZ = new UndoCommand();
        _model.buttonR = new ReplayCommand();

        _model.shootButton = new ShotCommand();
        _model.prevtWeaponButton = new PrevWeaponCommand();
        _model.nextWeaponButton = new NextWeaponCommand();

        _model.shootButton.Init(_model);
        _model.prevtWeaponButton.Init(_model);
        _model.nextWeaponButton.Init(_model);
        */

        _model.weapon = _model.weapons[0];
        _model.col = gameObject.GetComponent<Collider>();
        _model.speedDecorator = new SpeedPowerUpDecorator();

    }

    void Update()
    {
        _model.currentTime += Time.deltaTime;
        LoseLife();
        if (Time.deltaTime == 0 || _model.death) return;
        

        PowerUpFieldForce();


        if (_model.powerUpSpeed)
        {
            if(_model.TimeWithSpeedMax - _model.currentTime < 0)
            {
                _model.speedDecorator.Stop(_model);
            }
            
        }


        foreach (var comand in _model.keysCommands)
        {
            if (Input.GetKey(comand.Key))
            {
                comand.Value.Execute(_model.gameObject);
                _model.allCommands.Add(comand.Value);
            }
        }


        if (Input.GetKey(KeyCode.R) && _model.allCommands.Count>0)
        {
            var act = _model.allCommands[_model.allCommands.Count - 1];
            act.Undo(_model.gameObject);
            _model.allCommands.RemoveAt(_model.allCommands.Count - 1);
        }

    }

    public void PowerUpFieldForce()
    {
        if (_model.time > _model.TimeWithPowerUp)
        {
            _model.powerUp = false;
            _model.time = 0;
        }
        if (_model.powerUp)
        {
            _model.forceField.SetActive(true);
            _model.time += Time.deltaTime;
        }
        else if (!_model.powerUp)
        {
            _model.forceField.SetActive(false);
            _model.time = 0f;
        }
    }

    public void Save()
    {
        SaveManager.SaveShipStats(_model);
    }

    public void Load()
    {
        ShipData data = SaveManager.LoadShipStats();
        _model.currentWeapon = data.currentIndexWeapon;
        transform.position = data.position.ToVector3();
        transform.rotation = data.rotation.ToQuaternion();
        _model.powerUp = data.poewerUpField;
        _model.powerUpSpeed = data.powerUpSeed;
        _model.weapon.currentLevel = data.weaponLvl;
    }



    private void HandleInput()
    {
        float Aceleration = Input.GetAxisRaw("Vertical");
        /*
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




        if (Input.GetKey(KeyCode.Space))
        {
            _model.shootButton.Execute(_model.shootButton);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _model.prevtWeaponButton.Execute(_model.prevtWeaponButton);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _model.nextWeaponButton.Execute( _model.nextWeaponButton);
        }*/


        if (Aceleration > 0)
        {
            if (!_model.Source.isPlaying || _model.Source.clip != _model.allSounds[Sounds.movementTurbo])
            {
                _model.Source.clip = _model.allSounds[Sounds.movementTurbo];
                _model.Source.Play();
            }
        }
    }


    /*
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
    */

    /*
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
    */


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
            _model.weapon.currentLevel = 0;

            if (!_model.Source.isPlaying || _model.Source.clip != _model.allSounds[Sounds.loseLife])
            {
                _model.Source.clip = _model.allSounds[Sounds.loseLife];
                _model.Source.Play();
            }

            _model.speed = 0;

            _model.fbx.SetActive(false);
            _model.col.enabled = false;
            _model.speedDecorator.Stop(_model);

            if (_model.respawnTime - _model.currentTime < 0)
            {

                _model.fbx.SetActive(true);
                _model.col.enabled = true;

       
                _model.speed = 60;
                _model.currentTime = 0;
                _model.death = false;
            }
        }
    }




    IEnumerator Inmune()
    {
        yield return new WaitForSeconds(20f);
        _model.fbx.SetActive(!_model.fbx.activeSelf);
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

    public ShipModel GetModel
    {
        get
        {
            return _model;
        }
    }

}
