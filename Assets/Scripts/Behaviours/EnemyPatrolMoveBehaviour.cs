﻿using Components;
using Configs;
using UnityEngine;
using UnityEngine.Assertions;

namespace Behaviours
{
    public class EnemyPatrolMoveBehaviour : MoveBehaviour<IMoveAndRotate>
    {
        private readonly EnemyPatrolConfig enemyPatrolConfig;
        private LevelController levelController;
        private Vector3 target;
        private Vector3 startPosition;
        private float waitPeriod;

        public EnemyPatrolMoveBehaviour(IMoveAndRotate movable, EnemyPatrolConfig enemyPatrolConfig) : base(movable)
        {
            Assert.IsNotNull(enemyPatrolConfig, "No config for patrol behaviour");
            this.enemyPatrolConfig = enemyPatrolConfig;
            GlobalCommander.Commander.Inject((LevelController ctrl) => levelController = ctrl);
        }

        public override void Pause()
        {
        }

        public override void UnPause()
        {
        }

        public override void Update()
        {
            switch (state)
            {
                case MoveStates.DEFAULT:
                    DefaultLogic();
                    break;
                case MoveStates.WAIT:
                    Wait();
                    break;
                case MoveStates.MOVE:
                    if (Move())
                        return;
                    state = MoveStates.WAIT;
                    waitPeriod = Time.time + enemyPatrolConfig.WaitBeforeNextPatrol;
                    break;
                case MoveStates.PAUSE:
                    break;
                case MoveStates.UNPAUSE:
                    break;
            }
        }

        private void DefaultLogic()
        {
            startPosition = movable.Transform.position;
            var tileTarget = levelController.GetRandomAvailablePosition(movable.Transform.position, enemyPatrolConfig.PatrolRange);
            target = new Vector3(tileTarget.x, movable.Transform.position.y, tileTarget.z);
            state = MoveStates.MOVE;
        }

        private void Wait()
        {
            if (waitPeriod > Time.time)
                return;

            if (enemyPatrolConfig.PatrolRandom)
            {
                state = MoveStates.DEFAULT;
                return;
            }
            var temp = target;
            target = startPosition;
            startPosition = temp;
            state = MoveStates.MOVE;
        }

        private bool Move()
        {
            movable.Transform.position = Vector3.MoveTowards(movable.Transform.position, target, movable.MoveSpeed * Time.deltaTime);
            movable.Transform.rotation = Quaternion.Lerp(movable.Transform.rotation, Quaternion.LookRotation(target - movable.Transform.position), movable.RotationSpeed * Time.deltaTime);

            if (Vector3.Distance(movable.Transform.position, target) <= movable.StoppingDistance)
                return false;

            return true;
        }
    }
}
