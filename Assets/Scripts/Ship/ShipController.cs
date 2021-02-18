using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;

public class ShipController : MonoBehaviour
{
    ShipModel _model;

    private void Awake()
    {      
        _model = GetComponent<ShipModel>();
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

    }

    void Update()
    {
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


}
