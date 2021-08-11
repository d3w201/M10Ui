using System;
using System.Linq;
using Controller;
using Enumeral;
using Script.Entity.Interface;
using Script.Enumeral;
using UnityEngine.InputSystem;

namespace Script.Controller.Items
{
    public class InteractableController : RootController
    {
        //Private-props
        private IInteractable _item;
        
        //Public-methods
        public void DoInteract(Action callback)
        {
            GameController.SetStatus(GameStatus.Interact);
            //interactable.DoInteract();
            var dialogData = _item.GetDialogData();
            dialogData.Last().Callback = () =>
            {
                _item.DoInteract();
                GameController.SetStatus(GameStatus.Play);
                callback();
            };
            DialogController.Show(dialogData);
        }
        
        public void HandleInteract(InputValue value)
        {
            DialogController.Click_Window();
        }

        public void SetInteractable(IInteractable interactable)
        {
            this._item = interactable;
        }
    }
}