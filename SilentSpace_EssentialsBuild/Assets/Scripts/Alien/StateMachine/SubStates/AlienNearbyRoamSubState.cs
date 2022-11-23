using SilentSpace.Helpers;
using UnityEngine;
using UnityEngine.AI;
using AudioType = SilentSpace.Audio;

namespace SilentSpace.Alien.StateMachine.SubStates
{
    public class AlienNearbyRoamSubState : AlienBaseState
    {
        public AlienNearbyRoamSubState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) : base(currentContext, alienStateFactory)
        {
            StateName = "Nearby Roam SubState";
        }

        public override void EnterState()
        {
            Ctx.audioController.PlayAudio(AudioType.AudioType.SFX_Enemy_Creature_Speak_01,true);
            Ctx.Log("Entered Nearby Roam Substate");
            Ctx.Timer = new Timer(10f);
            Ctx.Timer.OnTimerEnd += CheckSwitchStates;
        }
        public override void FixedUpdateState() { }

        public override void UpdateState()
        {
            Ctx.Timer.Tick();
            if (!(Ctx.agent.remainingDistance <= Ctx.agent.stoppingDistance)) return;
            if (RandomPoint(Ctx.transform.position, Ctx.range, out var point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                Ctx.agent.SetDestination(point);
            }
        }

        public override void ExitState()
        {
            Ctx.audioController.StopAudio(AudioType.AudioType.SFX_Enemy_Creature_Speak_01, true);
            Ctx.Timer.OnTimerEnd -= CheckSwitchStates;
            Ctx.agent.SetDestination(Ctx.Position);
        }
        public override void InitializeSubState() { }

        public override void CheckSwitchStates()
        {
            SwitchState(Factory.AreaRoam());
        }

        private bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            var randomPoint = center + Random.insideUnitSphere * range;
            if (NavMesh.SamplePosition(randomPoint, out var hit, 25.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }
    }
}
