using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SilentSpace.Core
{
    public class InputManager : MonoBehaviour
    {
        //Singleton
        public static InputManager Instance;

        //Inputs
        private PlayerInputActions _playerInputActions;
        private InputAction _movement;
        private InputAction _mouseMovement;
        private InputAction _run;
        private InputAction _crouch;
        private InputAction _interact;
        private InputAction _openPause;
        private InputAction _openJournal;
        private InputAction _openMap;

        #region C# Events

        public event Action OnRunStarted;
        public event Action OnRunHold;
        public event Action OnRunCanceled;
        public event Action OnCrouchStarted;
        public event Action OnCrouchHold;
        public event Action OnCrouchCanceled;
        public event Action OnInteractStarted;
        public event Action OnInteractHold;
        public event Action OnInteractCanceled;
        public event Action OnOpenPause;
        public event Action OnOpenJournal;
        public event Action OnOpenMap;

        #endregion

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();

            //singleton
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #region Input Definitions

        private void OnEnable()
        {
            /*
         * On Enable we reference the Input control schemes we have
         * and then enabled them. In the case of Inputs that have interactions
         * or return a bool value like buttons, we can also subscribe methods
         * to receive the input events (started, performed, canceled).
         * 
         */

            _movement = _playerInputActions.PlayerControls.Movement;
            _movement.Enable();

            _mouseMovement = _playerInputActions.PlayerControls.MouseMovement;
            _mouseMovement.Enable();

            _run = _playerInputActions.PlayerControls.Run;
            _run.started += OnRun;
            _run.performed += OnRun;
            _run.canceled += OnRun;
            _run.Enable();

            _crouch = _playerInputActions.PlayerControls.Crouch;
            _crouch.started += OnCrouch;
            _crouch.performed += OnCrouch;
            _crouch.canceled += OnCrouch;
            _crouch.Enable();

            _interact = _playerInputActions.PlayerControls.Interact;
            _interact.started += OnInteraction;
            _interact.performed += OnInteraction;
            _interact.canceled += OnInteraction;
            _interact.Enable();

            _openPause = _playerInputActions.PlayerControls.OpenPause;
            _openPause.performed += OnPause;
            _openPause.Enable();

            _openJournal = _playerInputActions.PlayerControls.OpenJournal;
            _openJournal.performed += OnJournal;
            _openJournal.Enable();

            _openMap = _playerInputActions.PlayerControls.OpenMap;
            _openMap.started += OnMap;
            _openMap.Enable();
        }

        private void OnDisable()
        {
            /*
         * On Disable we disable all the control scheme in the case the input manager
         * were to be disabled or destroy. This helps with the loading through scenes
         * and make sure double instances do not occur.
         * 
         */
            _movement.Disable();
            _mouseMovement.Disable();
            _crouch.Disable();
            _interact.Disable();
            _openPause.Disable();
            _openJournal.Disable();
            _openMap.Disable();
        }

        #endregion

        #region Vector2 Inputs Handling

        /*
        * Get Movement and Mouse movement returns the vector2 value from using 
        * the WASD keys and the Mouse Delta.
        * If you want to use this as a check for a if statement you can
        * use it's magnitude to see if the inputs are being used.
        * i.e if(inputManager.GetMovement().magnitude >= 0.1) true if Getmovement's
        * magnitude is bigger than 0. and checking for lower or equal to 0 would be the 
        * opposite.
        * 
        */
        public Vector2 GetMovement()
        {
            return _movement.ReadValue<Vector2>();
        }

        public Vector2 GetMouseMovement()
        {
            return _mouseMovement.ReadValue<Vector2>();
        }

        #endregion

        #region Button Inputs

        /*
        * This type of methods are used to invoke an event depending of
        * the type of interaction the input sent. This is used to create
        * different interactions if the player tapped the button or hold
        * the button.
        * 
        */
        private void OnInteraction(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnInteractStarted?.Invoke();
            }

            if (context.performed)
            {
                OnInteractHold?.Invoke();
            }

            if (context.canceled)
            {
                OnInteractCanceled?.Invoke();
            }
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnRunStarted?.Invoke();
            }

            if (context.performed)
            {
                OnRunHold?.Invoke();
            }

            if (context.canceled)
            {
                OnRunCanceled?.Invoke();
            }
        }

        private void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnCrouchStarted?.Invoke();
            }

            if (context.performed)
            {
                OnCrouchHold?.Invoke();
            }

            if (context.canceled)
            {
                OnCrouchCanceled?.Invoke();
            }
        }

        /*
        *
        *This method type is for single press buttons, we only care
        *for the performed context so as soon as its press we invoke
        *the event. If we wanted something to happen when we canceled
        *we can do like the methods before and check the if statement
        *for a type of context interaction.
        *
        */

        private void OnPause(InputAction.CallbackContext context)
        {
            OnOpenPause?.Invoke();
        }

        private void OnJournal(InputAction.CallbackContext context)
        {
            OnOpenJournal?.Invoke();
        }

        private void OnMap(InputAction.CallbackContext context)
        {
            Debug.Log(context);
            OnOpenMap?.Invoke();
        }
        #endregion

        public bool IsRunEnabled()
        {
            return _run.enabled;
        }

        #region Enable/Disable Inputs

        /*
         * PlayerControls Enables/Disables this can be called by any script that references Input Manager. giving them the ability to disable and reenable the controls.
         */
        public void EnableLookInputs() => _mouseMovement.Enable();

        public void DisableLookInputs() => _mouseMovement.Disable();

        //Enable/Disable Ability to move.
        public void EnableMovementInputs() => _movement.Enable();
        public void DisableMovementInputs() => _movement.Disable();
        
        //Enable/Disable Ability to Run.
        public void EnableRunInputs() => _run.Enable();
        public void DisableRunInputs() => _run.Disable();

        //Enable/Disable Ability to interact.
        public void EnableInteractionInputs() => _interact.Enable();
        public void DisableInteractInputs() => _interact.Disable();

        //Enable/Disable Ability to open pause menu.
        public void EnableOpenPauseInputs() => _openPause.Enable();
        public void DisableOpenPauseInputs() => _openPause.Disable();

        //Enable/Disable Ability to open journal menu.
        public void EnableOpenJournalInputs() => _openJournal.Enable();
        public void DisableOpenJournalInputs() => _openJournal.Disable();

        #endregion
    }
}