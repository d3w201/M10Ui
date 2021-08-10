using System.Collections.Generic;
using Entity.Dialog;
using Script.Entity.Interface;
using UnityEngine;

namespace Script.Entity.Interactable
{
    public class InteractableChest : RootInteractable, IInteractable
    {
        
        public void DoInteract()
        {
            GetComponent<Animation>().Play();
        }
        public List<DialogData> GetDialogData()
        {
            var dialogList = new List<DialogData>();
            var text1 = new DialogData(T00);
            dialogList.Add(text1);
            return dialogList;
        }

        private const string T00 =
            "Un baule chiuso, mi servirebbe una chiave ...";
    }
}