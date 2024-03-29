﻿using Commands;
using Components;

namespace Behaviours
{
    public class CoinMoveBehaviour : MoveBehaviour<IBounceMove>
    {
        public CoinMoveBehaviour(IBounceMove move) : base(move)
        {
        }

        public override void CommandBehaviour(ICommand command)
        {
            if (command is IAddRandomDirectionCommand addRandomDirectionCommand)
                AddRandomMove();
        }

        private void AddRandomMove()
        {
            var randomDirect = UnityEngine.Random.insideUnitSphere * movable.BounceRadius;
            movable.Rigidbody.AddForce(randomDirect * movable.Force, UnityEngine.ForceMode.Impulse);
            movable.Transform.rotation = UnityEngine.Random.rotation;
        }

        public override void Pause()
        {
        }

        public override void UnPause()
        {
        }

        public override void Update()
        {
        }
    }
}
