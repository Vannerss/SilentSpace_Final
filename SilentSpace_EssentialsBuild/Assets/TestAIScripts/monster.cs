using System.Collections;
using System.Collections.Generic;
using SilentSpace.Audio;
using UnityEngine;
using UnityEngine.AI;
using SilentSpace.Helpers;

namespace SilentSpace
{
    public class monster : MonoBehaviour
    {
        public GameObject player;
        public Transform eyes;

        private NavMeshAgent nav;
        //private string state = "idle";
        private bool alive = true;
        private float wait = 0f;
        private bool highAlert = false;
        private float alertness = 40f;

        private AudioController _audioController;
        private Animator _animator;
        //private Timer timer;

        public enum States
        {
            walk,
            search,
            idle,
            chase,
            hunt,
            kill
        }

        private States state;

        void Start()
        {
            state = States.idle;
            nav = GetComponent<NavMeshAgent>();
            nav.speed = 4f;
            //timer = new Timer();
            //timer.RemainingSeconds;
            //timer.Tick();
        }

        //Check if player is visible
        public void checkSight()
        {
            RaycastHit rayHit;

            if (Physics.Linecast(eyes.position, player.transform.position, out rayHit))
            {
                if (rayHit.collider.gameObject.name == "Body")
                {
                    if (state != States.kill)
                    {
                        state = States.chase;
                        nav.speed = 5f;
                    }
                }
            }
        }

        void Update()
        {
            
            switch (state)
            {
                case States.idle:
                    {
                        Idle();
                        break;
                    }
                case States.walk:
                    {
                        Walk();
                        break;
                    }
                case States.chase:
                    {
                        //Chase();
                        break;
                    }
                case States.search:
                    {
                        //Search();
                        break;
                    }
                case States.hunt:
                    {
                        //Hunt();
                        break;
                    }
            }

#region hide me
/*if (alive)
{
    //IDLE
    if (state == "idle")
    {
        //Pick random place to walk
        Vector3 randomPos = Random.insideUnitSphere * alertness;
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position + randomPos, out navHit, 100f, NavMesh.AllAreas);

        //Go near player cuz high alert
        if (highAlert)
        {
            NavMesh.SamplePosition(player.transform.position + randomPos, out navHit, 100f, NavMesh.AllAreas);

            //It will lose awereness of the player general position
            alertness += 5f;

            if (alertness > 20f)
            {
                highAlert = false;
                nav.speed = 4f;
            }
        }

        nav.SetDestination(navHit.position);
        state = "walk";
    }

    //WALK
    if (state == "walk")
    {
        if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
        {
            state = "search";
            wait = 5f;
        }
    }

    //SEARCH
    if (state == "search")
    {
        if (wait > 0f)
        {
            wait -= Time.deltaTime;
            //transform.Rotate(0f, 90f * Time.deltaTime, 0f);
        }
        else
        {
            state = "idle";
        }
    }

    //CHASE
    if (state == "chase")
    {
        nav.speed = 6f;
        nav.destination = player.transform.position;
        //nav.SetDestination(player.transform.position); 

        //Lose sight of player
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > 30f)
        {
            state = "hunt";
        }
    }

    //HUNT
    if (state == "hunt")
    {
        if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
        {
            state = "search";
            wait = 2f;
            highAlert = true;
            alertness = 5f;
            checkSight();
        }
    }
}*/
            //nav.SetDestination(player.transform.position);
            #endregion
        }

        //TODO: Make a roaming state that does Idle and Walk.
        private void Idle()
        {
            //Pick random place to walk
            Vector3 randomPos = Random.insideUnitSphere * alertness;
            NavMesh.SamplePosition(transform.position + randomPos, out NavMeshHit navHit, 1000f, NavMesh.AllAreas);

            //Go near player cuz high alert
            if (highAlert)
            {
                NavMesh.SamplePosition(player.transform.position + randomPos, out navHit, 1000f, NavMesh.AllAreas);

                //It will lose awereness of the player general position
                alertness += 10f;

                if (alertness > 40f)
                {
                    highAlert = false;
                    nav.speed = 4f;
                }
            }

            nav.SetDestination(navHit.position);
            state = States.walk;
            //Debug.Log("Spam");
        }

        private void Walk()
        {
            if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
            {
                state = States.idle;
                wait = 5f;
            }
        }

        public void Search()
        {
            if (state == States.search)
            {
                if (wait > 0f)
                {
                    wait -= Time.deltaTime;
                    //transform.Rotate(0f, 90f * Time.deltaTime, 0f);
                }
                else
                {
                    state = States.idle;
                }
            }
        }

        public void Chase()
        {
            nav.speed = 6f;
            nav.destination = player.transform.position;

            //Lose sight of player
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance > 30f)
            {
                state = States.hunt;
            }
        }

        public void Hunt()
        {
            if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
            {
                state = States.search;
                wait = 2f;
                highAlert = true;
                alertness = 5f;
                checkSight();
            }
        }
    }
}