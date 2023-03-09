using Commands;
using Components;
using HECS.Controllers;
using UnityEngine;

namespace Behaviours
{
    public class PlayerStandartMoveBehaviour : MoveBehaviour<IMoveAndRotate>
    {
        private bool _isNeedRotateToTarget;
        private Vector3 _targetForRotation;

        public PlayerStandartMoveBehaviour(IMoveAndRotate movable) : base(movable)
        {
        }

        public override void Pause()
        {
            state = MoveStates.PAUSE;
        }

        public override void UnPause()
        {
            state = MoveStates.UNPAUSE;
        }

        public override void Update()
        {
            switch (state)
            {
                case MoveStates.DEFAULT:
                    Move();
                    Rotate();
                    break;
                case MoveStates.PAUSE:
                    break;
                case MoveStates.UNPAUSE:
                    state = MoveStates.DEFAULT;
                    break;
            }
        }

        public override void CommandBehaviour(ICommand command)
        {
            if (command is ICommandRotateToTarget rotateToTarget)
            {
                _isNeedRotateToTarget = true;
                _targetForRotation = rotateToTarget.Target;
            }
        }

        private void Rotate()
        {
            if (!_isNeedRotateToTarget)
                return;

            var directionToRotate = _targetForRotation - movable.Transform.position;
            movable.Transform.rotation = Quaternion.Lerp(movable.Transform.rotation,
                Quaternion.LookRotation(directionToRotate), movable.RotationSpeed * Time.deltaTime);

            if (Vector3.Angle(directionToRotate, movable.Transform.forward) < 0.3f)
                _isNeedRotateToTarget = false;
        }

        private void Move()
        {
            movable.Transform.position += new Vector3(InputController.Instance.HorizontalMove * movable.MoveSpeed * Time.deltaTime, 0,
                InputController.Instance.VerticalMove * movable.MoveSpeed * Time.deltaTime);

        }

    }
}
