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
        private int aggro;
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
            aggro = 1;
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
                        if (aggro == 1)
                        {
                            _animator.SetBool("intimidateOnly", true);
                            states = States.intimidate;
                        }
                        else if (aggro >= 2)
                        {
                            _animator.SetBool("intimidate", true);
                            states = States.intimidate;
                        }
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
            _animator.SetBool("intimidateOnly", false);

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Roaming & Hunt"))
            {
                nav.isStopped = false;
            }

            if (stopAndGo == false)
            {
                randomPos = Random.insideUnitSphere * alertness;
                NavMesh.SamplePosition(transform.position + randomPos, out navHit, 5000f, NavMesh.AllAreas);

                if (highAlert == true && aggro == 3)
                {
                    randomPos = Random.insideUnitSphere * alertness;
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
            }

            nav.speed = 7.5f;
            _animator.speed = 1.5f;
            nav.destination = player.transform.position;
            _animator.SetBool("attack", false);
            _animator.SetBool("intimidate", false);
            _animator.SetBool("intimidateOnly", false);
            _animator.SetBool("roaming", false);

            //Lose sight of player
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance > 30f && aggro == 3)
            {
                states = States.hunt;
            }
            else if (distance > 12.5f && aggro == 1)
            {
                states = States.roaming;
                nav.speed = 5f;
                _animator.SetBool("roaming", true);
            }
            else if (distance > 30f && aggro == 2)
            {
                states = States.roaming;
                nav.speed = 5f;
                _animator.SetBool("roaming", true);
            }
            else if (distance <= 5f)
            {
                states = States.attack;
            }
        }

        public void Intimidate()
        {
            nav.isStopped = true;
            roar.pitch = 1.2f;
            roar.Play();
            states = States.chase;
        }
        
        //Search for player in nearby area of the player until it lose all alertness
        public void Hunt()
        {
            _animator.SetBool("roaming", true);

            if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
            {
                highAlert = true;
                states = States.roaming;
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
}