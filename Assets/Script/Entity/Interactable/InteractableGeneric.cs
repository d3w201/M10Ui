using System;
using System.Collections.Generic;
using Entity.Dialog;
using Script.Entity.Interface;
using Script.Entity.Item;
using UnityEngine;

namespace Script.Entity.Interactable
{
    public abstract class InteractableGeneric :RootInteractable, IInteractable
    {
        private GenericItem _item; 
        private void Start()
        {
            _item = gameObject.GetComponent<GenericItem>();
        }

        public abstract void DoInteract();

        public List<DialogData> GetDialogData()
        {
            return new List<DialogData>
            {
                new DialogData(_item.interactText)
            };
        }
    }
}