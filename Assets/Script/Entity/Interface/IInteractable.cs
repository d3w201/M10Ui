using System.Collections.Generic;
using Entity.Dialog;

namespace Script.Entity.Interface
{
    public interface IInteractable
    {
        void DoInteract();
        
        public List<DialogData> GetDialogData();

    }
}