using System;
using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateWalk : AKenneyState
    {
        private IMove2DLockedReader _lockedReader;
        private IMove2DDirReader _movReader;
        private IMove2DSpeedMaxReader _speedMaxReader;
        private IMove2DOrientWriter _orientWriter;
        private IMove2DSpeedWriter _speedWriter;
        protected override void OnStateInit()
        {
            _lockedReader = StateMachine.GetComponent<IMove2DLockedReader>();
            _movReader = StateMachine.GetComponent<IMove2DDirReader>();
            _speedMaxReader = StateMachine.GetComponent<IMove2DSpeedMaxReader>();
            _orientWriter = StateMachine.GetComponent<IMove2DOrientWriter>();
            _speedWriter = StateMachine.GetComponent<IMove2DSpeedWriter>();

            //Find Movable Interfaces inside StateMachine
            //You will need to :
            // - Check if movements are locked
            // - Read Move Dir
            // - Read Move SpeedMax
            // - Write Move Orient
            // - Write Move Speed
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            _speedWriter.MoveSpeed = _speedMaxReader.MoveSpeedMax;
            _orientWriter.OrientDir = _movReader.MoveDir;
            Debug.Log("Enter Walk");
            //Force MoveSpeed to MoveSpeedMax
            //Force OrientDir to MoveDir
        }

        protected override void OnStateUpdate()
        {
            if (_lockedReader.AreMovementsLocked)
            {
                StateMachine.ChangeState(StateMachine.StateIdle);
            }
            if (_movReader.MoveDir == Vector2.zero)
            {
                StateMachine.ChangeState(MovementsData.StopDecelerationDuration > 0 ? StateMachine.StateDecelerate : StateMachine.StateIdle);
            }

            if (MovementsData.TurnBackDecelerationDuration > 0 && Vector2.Angle(_movReader.MoveDir, _orientWriter.OrientDir) > MovementsData.TurnBackAngleThreshold)
            {
                StateMachine.ChangeState(StateMachine.StateTurnBackDecelerate);
            }

            _orientWriter.OrientDir = _movReader.MoveDir;
            //Go to State Idle if Movements are locked

            //If there is no MoveDir
            //Go to StateDecelerate if MovementsData.StopDecelerationDuration > 0
            //Go to StateIdle otherwise

            //If MovementsData.TurnBackDecelerationDuration > 0 and the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
            //Go to StateTurnBackDecelerate

            //Force OrientDir to MoveDir
        }
    }
}