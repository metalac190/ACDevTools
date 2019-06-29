using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ACDev.UI
{
    public static class UIHelper
    {
        /// <summary>
        /// Turns off all panels except for 1. Technically it turns them all off, then reenables the 1.
        /// </summary>
        /// <param name="panelToIsolate"></param>
        /// <param name="panelsToDisable"></param>
        public static void IsolatePanel(GameObject panelToIsolate, GameObject[] panelsToDisable)
        {
            // turn them all off, so that we can re-enable the one we want
            foreach(GameObject panel in panelsToDisable)
            {
                panel.SetActive(false);
            }
            // then enable only the panel that we want
            panelToIsolate.SetActive(true);
        }

        public static void ClearButton(Button buttonToClear)
        {
            buttonToClear.interactable = false;
            // also clear the button text
            Text buttonText = buttonToClear.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "";
            }
        }

        /// <summary>
        /// Quickly enable/disable layout group, to reorganize elements but avoid constant updates
        /// </summary>
        /// <param name="layoutGroupToToggle"></param>
        /// <returns></returns>
        public static IEnumerator ToggleLayoutGroup(LayoutGroup layoutGroupToToggle)
        {
            layoutGroupToToggle.enabled = true;
            yield return new WaitForSeconds(.1f);
            layoutGroupToToggle.enabled = false;
        }

        /// <summary>
        /// Quickly enable/disable layout groups, to reorganize elements but avoid constant updates
        /// </summary>
        /// <param name="layoutGroupToToggle"></param>
        /// <returns></returns>
        public static IEnumerator ToggleLayoutGroups(LayoutGroup[] layoutGroupsToToggle)
        {
            foreach (LayoutGroup layoutGroup in layoutGroupsToToggle)
            {
                layoutGroup.enabled = true;
            }
            yield return new WaitForSeconds(.1f);
            foreach (LayoutGroup layoutGroup in layoutGroupsToToggle)
            {
                layoutGroup.enabled = false;
            }
        }
    }
}

