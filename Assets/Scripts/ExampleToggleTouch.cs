namespace ExampleSagoTouch {

    using SagoTouch;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class ExampleToggleTouch : MonoBehaviour {


        #region Fields

        [System.NonSerialized]
        private Button m_Button;

        [System.NonSerialized]
        private Text m_ButtonText;

        #endregion


        #region Properties

        public Button Button
        {
            get { return m_Button = m_Button ?? GetComponent<Button>(); }
        }

        public Text ButtonText
        {
            get { return m_ButtonText = m_ButtonText ?? GetComponentInChildren<Text>(); }
        }

        #endregion


        #region Methods

        /// <summary>
        ///     Toggles the Touch Dispatcher on and off, and updates the assocated Button accordingly.
        /// </summary>
        public void ToggleAllTouchInput()
        {
            if (TouchDispatcher.Instance)
            {
                TouchDispatcher.Instance.enabled = !TouchDispatcher.Instance.enabled;
                if (TouchDispatcher.Instance.enabled)
                {
                    ButtonText.text = "DISABLE TOUCH";
                }
                else
                {
                    ButtonText.text = "ENABLE TOUCH";
                }
            }
        }

        #endregion

    }

}