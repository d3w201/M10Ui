using Controller.Player;
using Script.Controller.Player;
using UnityEngine;

namespace Script.Entity.Interactable
{
    public class RootInteractable : MonoBehaviour
    {
        protected ChiuskyController ChiuskyCtrl;
        
        //triggers
        private void OnTriggerEnter(Collider other)
        {
            if (!"Chiusky".Equals(other.gameObject.tag)) return;
            ChiuskyCtrl ??= other.gameObject.GetComponent<ChiuskyController>();
            ChiuskyCtrl.SetFocusItem(this.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!"Chiusky".Equals(other.gameObject.tag)) return;
            ChiuskyCtrl ??= other.gameObject.GetComponent<ChiuskyController>();
            if (this.gameObject.Equals(ChiuskyCtrl.GetFocusItem()))
            {
                ChiuskyCtrl.SetFocusItem(null);
            }
        }
    }
}