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
        public AudioClip[] footsounds;
        public Transform eyes;

        private bool highAlert = false;
        private bool stopAndGo = false;
        private float alertness = 40f;
        private NavMeshAgent nav;
        //private AudioController _audioController;
        private AudioSource sound;
        private Animator _animator;
        private States states;
        private Vector3 randomPos;
        private NavMeshHit navHit;

        //private Timer timer;

        public enum States
        {
            roaming,
            chase,
            hunt,
            attack
        }    

        void Start()
        {
            states = States.roaming;
            nav = GetComponent<NavMeshAgent>();
            sound = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();

            nav.speed = 5f;
            _animator.speed = 1f;

            //timer = new Timer();
            //timer.RemainingSeconds;
            //timer.Tick();
        }

        void Update()
        {
            switch (states)
            {
                case States.roaming:
                    {
                        Roaming();
                        break;
                    }
                case States.chase:
                    {
                        Chase();
                        break;
                    }
                case States.hunt:
                    {
                        Hunt();
                        break;
                    }
                case States.attack:
                    {
                        break;
                    }
            }

            _animator.SetFloat("velocity", nav.velocity.magnitude);
        }

        //Check if player is visible
        public void checkSight()
        {
            RaycastHit rayHit;

            if (Physics.Linecast(eyes.position, player.transform.position, out rayHit))
            {
                if (rayHit.collider.gameObject.name == "Body")
                {
                    if (states != States.attack)
                    {
                        states = States.chase;
                    }
                }
            }
        }

        public void Footsteps(int _num)
        {
            sound.clip = footsounds[_num];
            sound.Play();
        }

        private void Roaming()
        {
            if (stopAndGo == false)
            {
                randomPos = Random.insideUnitSphere * alertness;
                NavMesh.SamplePosition(transform.position + randomPos, out navHit, 5000f, NavMesh.AllAreas);

                if (highAlert == true)
                {
                    NavMesh.SamplePosition(player.transform.position + randomPos, out navHit, 5000f, NavMesh.AllAreas);

                    //It will lose awereness of the player general position
                    alertness += 10f;

                    if (alertness > 40f)
                    {
                        highAlert = false;
                        nav.speed = 5f;
                        _animator.speed = 1f;
                    }
                }
                nav.SetDestination(navHit.position);
                stopAndGo = true;
            }
            else if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending && stopAndGo == true)
            {
                stopAndGo = false;
            }
        }

        public void Chase()
        {
            nav.speed = 7.5f;
            _animator.speed = 1.5f;
            nav.destination = player.transform.position;

            //Lose sight of player
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance > 30f)
            {
                states = States.hunt;
            }
        }

        public void Hunt()
        {
            if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
            {
                states = States.roaming;
                highAlert = true;
                alertness = 5f;
                checkSight();
            }
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