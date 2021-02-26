using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour, IObservable
{
    ShipModel _model;

    bool act;

    private void Awake()
    {
        _model = GetComponent<ShipModel>();
        _model.Source = GetComponent<AudioSource>();

        _model.weapons.Add(GetComponentInChildren<MachineGun>());
        _model.weapons.Add(GetComponentInChildren<RocketGun>());
        _model.weapons.Add(GetComponentInChildren<SuperRocketGun>());
        _model.weapons.Add(GetComponentInChildren<Granadas>());

        _model.keysCommands.Add(KeyCode.W, new FowardCommand());
        _model.keysCommands.Add(KeyCode.A, new RightCommand());
        _model.keysCommands.Add(KeyCode.D, new LeftCommand());
        _model.keysCommands.Add(KeyCode.Space, new ShootCommand());
        _model.keysCommands.Add(KeyCode.Q, new PrevWeaponCommand());
        _model.keysCommands.Add(KeyCode.E, new NextWeaponCommand());
        _model.keysCommands.Add(KeyCode.G, new ActivateGranades());
        _model.keysCommands[KeyCode.Space].Init(_model.gameObject);
        _model.keysCommands[KeyCode.Q].Init(_model.gameObject);
        _model.keysCommands[KeyCode.E].Init(_model.gameObject);
        _model.keysCommands[KeyCode.G].Init(_model.gameObject);

    }

    private void Start()
    {
        _model.weapon = _model.weapons[0];
        _model.col = gameObject.GetComponent<Collider>();
        _model.speedDecorator = new SpeedPowerUpDecorator();
        _model.forceFieldDecorator = new ForceFieldPowerUpDecorator();
        _model.doubleBullet = new DoubleBulletDecoretor();
        _model.tripleBullet = new TripleBulletDecorator();
        _model.debufBullet = new DebuffBulletDecorator();
    }

    void Update()
    {
       
        _model.currentTime += Time.deltaTime;

        LoseLife();

        if (Time.deltaTime == 0 || _model.death) return;
             
        if (_model.powerUpSpeed)
        {
            if(_model.TimeWithSpeedMax - _model.currentTime <= 0)
            {
                _model.speedDecorator.Stop(_model);
            }            
        }

        if (_model.powerUp)
        {
            if(_model.TimeWithPowerUp - _model.currentTime <= 0)
            {
                _model.forceFieldDecorator.Stop(_model);
            }
        }

        foreach (var comand in _model.keysCommands)
        {
            if (Input.GetKey(comand.Key) && comand.Key != KeyCode.Q && comand.Key != KeyCode.E && comand.Key != KeyCode.G)
            {
                comand.Value.Execute(_model.gameObject);
                _model.allCommands.Add(comand.Value);
            }

            if (Input.GetKeyDown(comand.Key) && (comand.Key == KeyCode.Q || comand.Key == KeyCode.E || comand.Key == KeyCode.G))
            {
                comand.Value.Execute(_model.gameObject);
                _model.allCommands.Add(comand.Value);
            }
        }


        if (Input.GetKey(KeyCode.R) && _model.allCommands.Count > 0)
        {
            var act = _model.allCommands[_model.allCommands.Count - 1];
            act.Undo(_model.gameObject);
            _model.allCommands.RemoveAt(_model.allCommands.Count - 1);
        }


        if (_model.aceleration > 0)
        {
            if (!_model.Source.isPlaying || _model.Source.clip != _model.allSounds[Sounds.movementTurbo])
            {
                _model.Source.clip = _model.allSounds[Sounds.movementTurbo];
                _model.Source.Play();
            }
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Asteroids>() && !_model.inmortal)
        {
            _model.currentTime = 0f;
            Notify("LoseLife");
            _model.death = true;
        }
    }


    float curr;
    private void LoseLife()
    {
        curr += Time.deltaTime;

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
                _model.speed = 60;
                _model.currentTime = 0;
                _model.death = false;
                _model.inmortal = true;
                curr = 0;
            }
        }

        if (_model.inmortal)
        {
            if (.1f - curr <= 0)
            {
                curr = 0;
                act = !act;
                _model.fbx.SetActive(!_model.fbx.activeSelf);
            }

            if (_model.inmuneDuration - _model.currentTime < 0)
            {
                _model.inmortal = false;
                _model.fbx.SetActive(true);
                _model.col.enabled = true;
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

    public ShipModel GetModel
    {
        get
        {
            return _model;
        }
    }

}
