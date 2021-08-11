using Controller.Player;
using Script.Controller.Camera;
using Script.Controller.Dialog;
using Script.Controller.Game;
using Script.Controller.Input;
using Script.Controller.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controller
{
    public class RootController : MonoBehaviour
    {
        //Game-Objects
        protected GameObject Chiusky;
        protected GameObject PauseUI;
        protected GameObject Dialog;
        protected GameObject MainCamera;
        protected GameObject GameManager;
        protected GameObject InputManager;
        protected GameObject EventSystemGameObject;
        protected GameObject InventoryUI;
        protected GameObject InventoryManager;

        //Components
        protected InputController InputController;
        protected DialogController DialogController;
        protected GameController GameController;
        protected Animator Animator;
        protected CharacterController CharacterController;
        protected ChiuskyController ChiuskyController;
        protected CameraController CameraController;
        protected EventSystem EventSystem;
        protected InteractableController InteractableController;
        protected InventoryController InventoryController;
        protected SelectItemController SelectItemController;

        //AnimationID
        protected int AnimIDSpeed;
        protected int AnimIDAim;
        protected int AnimIDAttack;

        //Awake
        protected void Awake()
        {
            if (!PauseUI)
            {
                PauseUI = GameObject.FindGameObjectWithTag("PauseUI");
            }
            
            if (!InventoryUI)
            {
                InventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
            }

            if (!Chiusky)
            {
                Chiusky = GameObject.FindGameObjectWithTag("Chiusky");
            }

            if (!Dialog)
            {
                Dialog = GameObject.FindWithTag("DialogAsset");
            }

            if (!MainCamera)
            {
                MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }

            if (!GameManager)
            {
                GameManager = GameObject.FindGameObjectWithTag("GameManager");
            }

            if (!InputManager)
            {
                InputManager = GameObject.FindGameObjectWithTag("InputManager");
            }
            if (!EventSystemGameObject)
            {
                EventSystemGameObject = GameObject.FindGameObjectWithTag("EventSystem");
            }
            if (!InventoryManager)
            {
                InventoryManager = GameObject.FindGameObjectWithTag("InventoryManager");
            }
            
            //Components
            InputController = InputManager.GetComponent<InputController>();
            ChiuskyController = Chiusky.GetComponent<ChiuskyController>();
            Animator = Chiusky.GetComponent<Animator>();
            CharacterController = Chiusky.GetComponent<CharacterController>();
            DialogController = Dialog.GetComponent<DialogController>();
            GameController = GameManager.GetComponent<GameController>();
            CameraController = MainCamera.GetComponent<CameraController>();
            EventSystem = EventSystemGameObject.GetComponent<EventSystem>();
            InteractableController = GameManager.GetComponent<InteractableController>();
            InventoryController = InventoryManager.GetComponent<InventoryController>();
            SelectItemController = InventoryUI.GetComponent<SelectItemController>();
            
            AnimIDSpeed = Animator.StringToHash("speed");
            AnimIDAim = Animator.StringToHash("aim");
            AnimIDAttack = Animator.StringToHash("attack");
        }
    }
}