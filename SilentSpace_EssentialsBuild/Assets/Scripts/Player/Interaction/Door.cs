using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SilentSpace.Player.interaction
{
    public class Door : MonoBehaviour,IInteractable
    {
        public string InteractionPrompt { get; }

     
        private GameObject _doorTop;
        private GameObject _doorBottom;
        private BoxCollider _topCollider;
        private BoxCollider _bottomCollider;
        private Vector3 _goalPositionTop;
        private Vector3 _goalPositionBottom;
        private bool _openDoor = false;
        private bool _doorIsOpen = false;

        private void Start()
        {
            _doorBottom = transform.GetChild(0).gameObject;
            _doorTop = transform.GetChild(1).gameObject;
            _bottomCollider = _doorBottom.GetComponent<BoxCollider>();
            _topCollider = _doorTop.GetComponent<BoxCollider>();

            var topDoorPos = _doorTop.transform.position;
            var bottomDoorPos = _doorBottom.transform.position;
            //1.5 | -1.3
            _goalPositionTop = new Vector3(topDoorPos.x, topDoorPos.y + 1.5f, topDoorPos.z);
            _goalPositionBottom = new Vector3(bottomDoorPos.x, bottomDoorPos.y - 1.3f, bottomDoorPos.z);
        }

        public void Interact()
        {
            if (_doorIsOpen)
            {
                return;
            }
            
            _openDoor = true;
        }

        private void Update()
        {
            
            if (_openDoor)
            {
                _doorTop.transform.position = Vector3.Lerp(_doorTop.transform.position, _goalPositionTop, 3 * Time.deltaTime);
                _doorBottom.transform.position = Vector3.Lerp(_doorBottom.transform.position, _goalPositionBottom, 3 * Time.deltaTime);
                _topCollider.enabled = false;
                _bottomCollider.enabled = false;
                
                if (_doorBottom.transform.position == _goalPositionBottom && _doorTop.transform.position == _goalPositionTop)
                {
                    _openDoor = false;
                    _doorIsOpen = true;
                }
            }
        }
    }
}
