namespace ExampleSagoTouch {

    using SagoTouch;
    using Touch = SagoTouch.Touch;
    using UnityEngine;

    public class ExampleTouchAreaObserver : MonoBehaviour {

        #region Fields

        [System.NonSerialized]
        private TouchArea m_TouchArea;

        [System.NonSerialized]
        private TouchAreaObserver m_TouchAreaObserver;

        private bool m_TouchInBounds;

        #endregion


        #region Properties

        public TouchArea TouchArea {
            get { return m_TouchArea = m_TouchArea ?? GetComponent<TouchArea>(); }
        }

        public TouchAreaObserver TouchAreaObserver {
            get { return m_TouchAreaObserver = m_TouchAreaObserver ?? GetComponent<TouchAreaObserver>(); }
        }

        #endregion


        #region Methods

        public void OnTouchDown(TouchArea touchArea, Touch touch) {
            if (touchArea.HitTest(touch)) {
                Debug.Log("Touch Detected In Bounds!", this);
            }
        }

        public void OnTouchUp(TouchArea touchArea, Touch touch) {
            if (!touchArea.HitTest(touch)) {
                Debug.Log("Touch Release Detected Out Of Bounds", this);
            }
        }

        public void OnTouchEnter(TouchArea touchArea, Touch touch) {
            if (touchArea.HitTest(touch)) {
                Debug.Log("Touch has Entered the Bounds!", this);
            }
        }

        public void OnTouchExit(TouchArea touchArea, Touch touch) {
            Debug.Log("Touch has Exited the Bounds!", this);
        }

        public void OnTouchCancelled(TouchArea touchArea, Touch touch) {
            OnTouchExit(touchArea, touch);
        }

        public void EnableTouchInput() {
            this.TouchArea.enabled = true;

            this.TouchAreaObserver.TouchDownDelegate = OnTouchDown;
            this.TouchAreaObserver.TouchUpDelegate = OnTouchUp;
            this.TouchAreaObserver.TouchEnterDelegate = OnTouchEnter;
            this.TouchAreaObserver.TouchExitDelegate = OnTouchExit;
            this.TouchAreaObserver.TouchCancelledDelegate = OnTouchCancelled;
        }

        public void DisableTouchInput() {
            this.TouchArea.enabled = false;

            this.TouchAreaObserver.TouchDownDelegate = null;
            this.TouchAreaObserver.TouchUpDelegate = null;
            this.TouchAreaObserver.TouchEnterDelegate = null;
            this.TouchAreaObserver.TouchExitDelegate = null;
            this.TouchAreaObserver.TouchCancelledDelegate = null;
        }

        #endregion


        #region MonoBehaviour Methods

        private void OnEnable() {
            EnableTouchInput();
        }

        private void OnDisable() {
            DisableTouchInput();
        }

        #endregion

    }

}
