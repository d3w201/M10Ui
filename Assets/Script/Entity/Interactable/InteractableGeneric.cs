using System.Collections.Generic;
using Entity.Dialog;
using Script.Entity.Interface;
using UnityEngine;

namespace Script.Entity.Interactable
{
    public class InteractableGeneric :RootInteractable, IInteractable
    {
        public virtual void DoInteract()
        {
            Debug.Log("end interaction");
        }

        public List<DialogData> GetDialogData()
        {
            var dialogList = new List<DialogData>();
            var text1 = new DialogData(this.tag);
            dialogList.Add(text1);
            return dialogList;
        }
    }
}