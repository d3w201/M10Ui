using UnityEngine;

namespace Script.Entity.Interactable
{
    public class InteractablePickup :InteractableGeneric
    {
        public string ItemName;
        public string Details ;
        public override void DoInteract()
        {
            Debug.Log(this.gameObject.name);
            ChiuskyCtrl.inventory.Add(this.gameObject);
            base.DoInteract();
            this.gameObject.SetActive(false);
        }
    }
}