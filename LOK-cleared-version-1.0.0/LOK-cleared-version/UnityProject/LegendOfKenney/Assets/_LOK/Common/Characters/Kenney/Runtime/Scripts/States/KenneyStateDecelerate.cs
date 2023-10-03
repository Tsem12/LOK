using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateDecelerate : AKenneyState
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        private float _timer = 0f;

        #pragma warning restore 0414
        #endregion
        private IMove2DLockedReader _lockedReader;
        private IMove2DDirReader _movReader;
        private IMove2DSpeedMaxReader _speedMaxReader;
        private IMove2DOrientWriter _orientWriter;
        private IMove2DSpeedWriter _speedWriter;

        private Vector2 _orient;
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
            // - Read Move Orient
            // - Write Move Speed
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            _orient = _orientWriter.OrientDir;
            Debug.Log("Enter dece");
            float speed = _speedWriter.MoveSpeed / _speedMaxReader.MoveSpeedMax;
            _timer = MovementsData.StopDecelerationDuration * (1 - speed);
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.StopDecelerationDuration
        }

        protected override void OnStateUpdate()
        {
            _orientWriter.OrientDir = _orient;
            if (_lockedReader.AreMovementsLocked)
            {
                StateMachine.ChangeState(StateMachine.StateIdle);
            }

            if (_movReader.MoveDir != Vector2.zero)
            {
                if (Vector2.Angle(_movReader.MoveDir, _orientWriter.OrientDir) > MovementsData.TurnBackAngleThreshold)
                {
                    if (MovementsData.TurnBackDecelerationDuration > 0)
                    {
                        StateMachine.ChangeState(StateMachine.StateTurnBackDecelerate);
                    }
                    else if (MovementsData.TurnBackAccelerationDuration > 0)
                    {
                        StateMachine.ChangeState(StateMachine.StateAccelerate);
                    }
                    else
                    {
                        StateMachine.ChangeState(MovementsData.StartAccelerationDuration > 0f ? StateMachine.StateAccelerate : StateMachine.StateWalk);
                    }
                }
            }

            _timer += Time.deltaTime;

            if (_timer > MovementsData.StopDecelerationDuration)
            {
                StateMachine.ChangeState(StateMachine.StateIdle);
            }

            float percentage = _timer / MovementsData.StopDecelerationDuration;
            //Debug.Log(            _orientWriter.OrientDir);
            _speedWriter.MoveSpeed = _speedMaxReader.MoveSpeedMax * ( 1 - percentage);
            //Go to State Idle if Movements are locked

            //If there is MoveDir
            //If the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
            //If MovementsData.TurnBackAccelerationDuration > 0 => Go to StateTurnBackDecelerate
            //Else If MovementsData.TurnBackAccelerationDuration > 0 => Go to StateTurnBackAccelerate
            //Else Go to StateAccelerate if MovementsData.StartAccelerationDuration > 0f
            //Else Go to StateWalk

            //Increment _timer with deltaTime

            //If _timer > MovementsData.StopDecelerationDuration
            //Go to StateIdle (deceleration is finished)

            //Calculate percent using timer and MovementsData.StopDecelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
        }
    }
}