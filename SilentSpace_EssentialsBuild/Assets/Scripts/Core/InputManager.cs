using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SilentSpace.Core
{
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// Static object reference for the Input Manager. (Singleton-like)
        /// </summary>
        public static InputManager Instance; //This has to be referenced to get access to input manager do: inputManager = InputManager.Instance;

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
        private InputAction _openNote;

        //Declaration Syntax On Other Scripts: _inputManager.EventName += ReceiverMethodName;
        //Check PlayerStateMachine.cs for usage example.
        #region C# Events
        
        /// <summary>
        /// Gets invoke when player just pressed the run input.
        /// </summary>
        public event Action OnRunStarted;
        
        /// <summary>
        /// Gets invoke when player is holding the run input.
        /// </summary>
        public event Action OnRunHold;
        
        /// <summary>
        /// Gets invoke as soon as player lets go of the run input.
        /// </summary>
        public event Action OnRunCanceled;
        
        /// <summary>
        /// Gets invoke when player just pressed the crouch input.
        /// </summary>
        public event Action OnCrouchStarted;
        
        /// <summary>
        /// Gets invoke when player is holding the crouch input.
        /// </summary>
        public event Action OnCrouchHold;
        
        /// <summary>
        /// Gets invoke as soon as player lets go of the run input.
        /// </summary>
        public event Action OnCrouchCanceled;
        
        /// <summary>
        /// Gets invoke when player just pressed the interact input.
        /// </summary>
        public event Action OnInteractStarted;
        
        /// <summary>
        /// Gets invoke when player is holding the interact input.
        /// </summary>
        public event Action OnInteractHold;
        
        /// <summary>
        /// Gets invoke as soon as player lets go of the interact input.
        /// </summary>
        public event Action OnInteractCanceled;
        
        /// <summary>
        /// Gets invoke when player presses the pause input.
        /// </summary>
        public event Action OnPause;
        
        /// <summary>
        ///  Gets invoke when player presses the journal input.
        /// </summary>
        public event Action OnJournal;
        
        /// <summary>
        ///  Gets invoke when player presses the map input.
        /// </summary>
        public event Action OnMap;

       
        #endregion

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            
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
            _openPause.performed += OnPausePressed;
            _openPause.Enable();

            _openJournal = _playerInputActions.PlayerControls.OpenJournal;
            _openJournal.performed += OnJournalPressed;
            _openJournal.Enable();

            _openMap = _playerInputActions.PlayerControls.OpenMap;
            _openMap.started += OnMapPressed;
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
        * magnitude is bigger than 0. and checking for <= to 0 would be the 
        * opposite.
        */
        
        /// <summary>
        /// Returns Vector2 WASD movement.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetMovement()
        {
            return _movement.ReadValue<Vector2>();
        }

        /// <summary>
        /// Returns Vector2 mouse movement.
        /// </summary>
        /// <returns></returns>
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
        */
        
        private void OnInteraction(InputAction.CallbackContext context)
        {
            if (context.started) //Sends as soon as key is pressed.
            {
                OnInteractStarted?.Invoke();
            }

            if (context.performed) //Sends when key is being hold.
            {
                OnInteractHold?.Invoke();
            }

            if (context.canceled) //sends when key is let go.
            {
                OnInteractCanceled?.Invoke();
            }
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            if (context.started) //Sends as soon as key is pressed.
            {
                OnRunStarted?.Invoke();
            }

            if (context.performed) //Sends when key is being hold.
            {
                OnRunHold?.Invoke();
            }

            if (context.canceled) //Sends when key is let go.
            {
                OnRunCanceled?.Invoke();
            }
        }

        private void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.started) //Sends as soon as key is pressed.
            {
                OnCrouchStarted?.Invoke();
            }

            if (context.performed) //Sends when key is hold.
            {
                OnCrouchHold?.Invoke();
            }

            if (context.canceled) //Sends when key is let go.
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

        private void OnPausePressed(InputAction.CallbackContext context)
        {
            OnPause?.Invoke(); //Sends when press.
        }

        private void OnJournalPressed(InputAction.CallbackContext context)
        {
            OnJournal?.Invoke(); //Sends when press.
        }
        
        private void OnMapPressed(InputAction.CallbackContext context)
        {
            OnMap?.Invoke(); //Sends when press.
        }
        #endregion

        #region Enable/Disable Inputs

        /*
         * PlayerControls Enables/Disables this can be called by any script that references Input Manager. giving them the ability to disable and reenable the controls.
         */
        
        /// <summary>
        /// Enable mouse movement inputs.
        /// </summary>
        public void EnableLookInputs() => _mouseMovement.Enable();
        
        /// <summary>
        /// Disable mouse movement inputs.
        /// </summary>
        public void DisableLookInputs() => _mouseMovement.Disable();

        /// <summary>
        /// Enable WASD movement inputs.
        /// </summary>
        public void EnableMovementInputs() => _movement.Enable();
        
        /// <summary>
        /// Disable WASD movement inputs.
        /// </summary>
        public void DisableMovementInputs() => _movement.Disable();
        
        /// <summary>
        /// Enable run inputs.
        /// </summary>
        public void EnableRunInputs() => _run.Enable();
        
        /// <summary>
        /// Disable run inputs.
        /// </summary>
        public void DisableRunInputs() => _run.Disable();

        /// <summary>
        /// Enable interact inputs.
        /// </summary>
        public void EnableInteractionInputs() => _interact.Enable();
        
        /// <summary>
        /// Disable interact inputs.
        /// </summary>
        public void DisableInteractInputs() => _interact.Disable();

        /// <summary>
        /// Enable pause inputs.
        /// </summary>
        public void EnableOpenPauseInputs() => _openPause.Enable();
        
        /// <summary>
        /// Disable pause inputs.
        /// </summary>
        public void DisableOpenPauseInputs() => _openPause.Disable();

        /// <summary>
        /// Enable journal inputs.
        /// </summary>
        public void EnableOpenJournalInputs() => _openJournal.Enable();
        
        /// <summary>
        /// Disable journal inputs.
        /// </summary>
        public void DisableOpenJournalInputs() => _openJournal.Disable();

        #endregion
    }
}