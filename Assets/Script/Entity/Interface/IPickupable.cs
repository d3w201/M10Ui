using System.Collections.Generic;
using Entity.Dialog;

namespace Script.Entity.Interface
{
    public interface IPickupable
    {
        public void DoPickup();
    
        public List<DialogData> GetDialogData();
    }
}
