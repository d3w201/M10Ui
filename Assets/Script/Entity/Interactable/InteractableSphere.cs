using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Dialog;
using Script.Entity.Interface;
using UnityEngine;

namespace Script.Entity.Interactable
{
    public class InteractableSphere : RootInteractable, IInteractable
    {
        public bool ready;
        public void DoInteract()
        {
            //StartCoroutine("wait_for_input");
            AsyncWaitForInput();
        }

        // . t e s t i n g . y i e l d . \\
        private IEnumerator wait_for_input()
        {
            Debug.Log("wait_for_input");
            IEnumerator enumerator = GetInput();
            yield return enumerator;
            Debug.Log(enumerator.Current);
        }

        private IEnumerator GetInput()
        {
            Debug.Log("GetInput");
            while (!ready)
            {
                yield return null;
            }

            yield return "r e t u r n e d y i e l d";
        }

        // . t e s t i n g . a w a i t . \\
        private async void AsyncWaitForInput()
        {
            Debug.Log("AsyncWaitForInput");
            var value = await AsyncGetInput();
            Debug.Log(value);
        }

        private async Task<String> AsyncGetInput()
        {
            Debug.Log("AsyncGetInput");
            while (!ready)
            {
                await Task.Yield();
            }
            return "r e t u r n e d a s y n c";
        }

        #region MyRegion

        public List<DialogData> GetDialogData()
        {
            var dialogList = new List<DialogData>();
            var text1 = new DialogData(T00);
            text1.SelectList.Add("Correct", "Lorem ipsum dolor");
            text1.SelectList.Add("Wrong", "sit amet");
            //text1.Callback = () => { Debug.Log("test"); };
            dialogList.Add(text1);
            return dialogList;
        }

        private const string T00 =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

        #endregion
    }
}