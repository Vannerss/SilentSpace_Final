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
        public AudioSource roar;

        private bool highAlert = false;
        private bool stopAndGo = false;
        private float alertness = 40f;
        private NavMeshAgent nav;
        private AudioSource sound;
        private Animator _animator;
        private States states;
        private Vector3 randomPos;
        private NavMeshHit navHit;

        public enum States
        {
            roaming,
            intimidate,
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
                case States.intimidate:
                    {
                        Intimidate();
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
                        Attack();
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
                    if (states != States.chase)
                    {
                        _animator.SetBool("intimidate", true);
                        states = States.intimidate;
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
            _animator.SetBool("intimidate", false);

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
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Chasing"))
            {
                nav.isStopped = false;
                nav.speed = 7.5f;
                _animator.speed = 1.5f;
                nav.destination = player.transform.position;
                _animator.SetBool("attack", false);
                _animator.SetBool("intimidate", false);
                _animator.SetBool("roaming", false);

                //Lose sight of player
                float distance = Vector3.Distance(transform.position, player.transform.position);

                if (distance > 30f)
                {
                    states = States.hunt;
                }
                else if (distance <= 5f)
                {
                    states = States.attack;
                }
            }

            /*nav.speed = 7.5f;
            _animator.speed = 1.5f;
            nav.destination = player.transform.position;
            _animator.SetBool("attack", false);
            _animator.SetBool("intimidate", false);
            _animator.SetBool("roaming", false);

            //Lose sight of player
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance > 30f)
            {
                states = States.hunt;
            }
            else if (distance <= 5f)
            {
                states = States.attack;
            }*/
        }

        public void Intimidate()
        {
            nav.isStopped = true;
            roar.pitch = 1.2f;
            roar.Play();

            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                states = States.chase;
            }
        }
        
        //Search for player in nearby area of the player until it lose all alertness
        public void Hunt()
        {
            _animator.SetBool("roaming", true);

            if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
            {
                states = States.roaming;
                highAlert = true;
                alertness = 5f;
                checkSight();
            }
        }

        public void Attack()
        {
            _animator.SetBool("attack", true);
            states = States.chase;
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