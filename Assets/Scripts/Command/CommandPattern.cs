using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    public abstract class Command
    {
        protected float moveDistance = 1f;
        [Range(20, 400)]
        public float maxSpeed;
        protected float _speed = 60;
        protected float _currentV = 0;
        protected float _currentH = 0;
        protected float _turnLeftRigth = 0;
        protected float _interpolation = 50;
        protected float _turnSpeed = 200;
        protected float Aceleration = Input.GetAxisRaw("Vertical");


        protected List<Weapons> weaponsList = new List<Weapons>();
        protected Weapons weapon;
        protected static int currentWeapon;

        public virtual void Init(ShipModel player) { }

        public abstract void Execute(Transform playerTrans, Command command);

        public abstract void Execute(Command command);

        public virtual void Undo(Transform playerTrans) { }

        public virtual void Move(Transform playerTrans) { }

        public virtual void Shoot() { }
    }

    public class MoveFoward : Command
    {
        public override void Execute(Transform playerTrans, Command command)
        {
            Move(playerTrans);
            ShipModel.oldCommands.Add(command);
        }
        public override void Undo(Transform playerTrans)
        {
            playerTrans.Translate(-playerTrans.forward * moveDistance);
        }
        public override void Move(Transform playerTrans)
        {
            playerTrans.position += playerTrans.forward * _speed * Time.deltaTime;
        }

        public override void Execute(Command command)
        {
            throw new NotImplementedException();
        }
    }

    public class MoveLeft : Command
    {
        public override void Execute(Transform boxTrans, Command command)
        {
            Move(boxTrans);
            ShipModel.oldCommands.Add(command);
        }

        public override void Undo(Transform boxTrans)
        {
            boxTrans.Translate(boxTrans.right * moveDistance);
        }

        public override void Move(Transform boxTrans)
        {
            float dirHTwo = Input.GetAxisRaw("Horizontal");
            float dirV = 0;
            float dirH = 0;

            _currentV = Mathf.Lerp(_currentV, dirV, _interpolation * Time.deltaTime);
            _currentH = Mathf.Lerp(_currentH, dirH, _interpolation * Time.deltaTime);
            _turnLeftRigth = Mathf.Lerp(_turnLeftRigth, dirHTwo, _interpolation * Time.deltaTime);

            boxTrans.Rotate(_currentV * _turnSpeed * Time.deltaTime, _turnLeftRigth * _turnSpeed * Time.deltaTime, _currentH * _turnSpeed * Time.deltaTime);
        }

        public override void Execute(Command command)
        {
            throw new NotImplementedException();
        }
    }

    public class MoveRight : Command
    {
        public override void Execute(Transform playerTrans, Command command)
        {
            Move(playerTrans);
            ShipModel.oldCommands.Add(command);
        }

        public override void Undo(Transform playerTrans)
        {
            playerTrans.Translate(-playerTrans.right * moveDistance);
        }

        public override void Move(Transform playerTrans)
        {
            float dirHTwo = Input.GetAxisRaw("Horizontal");
            float dirV = 0;
            float dirH = 0;

            _currentV = Mathf.Lerp(_currentV, dirV, _interpolation * Time.deltaTime);
            _currentH = Mathf.Lerp(_currentH, dirH, _interpolation * Time.deltaTime);
            _turnLeftRigth = Mathf.Lerp(_turnLeftRigth, dirHTwo, _interpolation * Time.deltaTime);

            playerTrans.Rotate(_currentV * _turnSpeed * Time.deltaTime, _turnLeftRigth * _turnSpeed * Time.deltaTime, _currentH * _turnSpeed * Time.deltaTime);
        }

        public override void Execute(Command command)
        {
            throw new NotImplementedException();
        }
    }

    public class ShotCommand : Command
    {     
        public override void Init(ShipModel player)
        {
            weaponsList.Add(player.GetComponentInChildren<MachineGun>());
            weaponsList.Add(player.GetComponentInChildren<RocketGun>());
            weaponsList.Add(player.GetComponentInChildren<Granadas>());
            Debug.Log($"arma actual : [ {currentWeapon} ]  {weapon}   |   cant armas :  {weaponsList.Count}");
        }

        public override void Execute(Command command)
        {
            Debug.Log($"arma actual : [ {currentWeapon} ] ");
            weapon = weaponsList[currentWeapon];
            Shoot();
        }


        public override void Shoot()
        {
            weapon.Shoot();         
        }

        public override void Execute(Transform playerTrans, Command command)
        {
            throw new NotImplementedException();
        }
    }

    public class NextWeaponCommand : Command
    {
        public override void Init(ShipModel player)
        {
            weaponsList.Add(player.GetComponentInChildren<MachineGun>());
            weaponsList.Add(player.GetComponentInChildren<RocketGun>());
            weaponsList.Add(player.GetComponentInChildren<Granadas>());

        }

        public override void Execute(Command command)
        {

            currentWeapon++;
 
            if (currentWeapon > weaponsList.Count-1)
                currentWeapon = 0;

            weapon = weaponsList[currentWeapon];

            Debug.Log($"arma actual : [ {currentWeapon} ]  {weapon}   |   cant armas :  {weaponsList.Count}");

        }

        public override void Execute(Transform playerTrans, Command command)
        {
            throw new NotImplementedException();
        }
    }

    public class PrevWeaponCommand : Command
    {

        public override void Init(ShipModel player)
        {
            weaponsList.Add(player.GetComponentInChildren<MachineGun>());
            weaponsList.Add(player.GetComponentInChildren<RocketGun>());
            weaponsList.Add(player.GetComponentInChildren<Granadas>());
        }

        public override void Execute(Command command)
        {
            currentWeapon--;


            if (currentWeapon < 0)
                currentWeapon = weaponsList.Count-1;

            weapon = weaponsList[currentWeapon];

            Debug.Log($"arma actual : [ {currentWeapon} ]  {weapon}   |   cant armas :  {weaponsList.Count}");

        }

        public override void Execute(Transform playerTrans, Command command)
        {
            throw new NotImplementedException();
        }
    }



    public class UndoCommand : Command
    {
        public override void Execute(Transform playerTrans, Command command)
        {
            List<Command> oldCommands = ShipModel.oldCommands;

            if(oldCommands.Count > 0)
            {
                Command latestCommand = oldCommands[oldCommands.Count - 1];

                latestCommand.Undo(playerTrans);

                oldCommands.RemoveAt(oldCommands.Count - 1);
            }
        }

        public override void Execute(Command command)
        {
            throw new NotImplementedException();
        }
    }

    public class ReplayCommand : Command
    {
        public override void Execute(Transform boxTrans, Command command)
        {
            ShipModel.shouldStartReplay = true; 
        }

        public override void Execute(Command command)
        {
            throw new NotImplementedException();
        }
    }

}
