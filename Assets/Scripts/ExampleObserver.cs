namespace ExampleSagoTouch {

    using SagoTouch;
    using UnityEngine;
    using Touch = SagoTouch.Touch;

    public class ExampleObserver : MonoBehaviour, ISingleTouchObserver
    {

        #region Properties

        [System.NonSerialized]
        private Camera m_Camera;

        [System.NonSerialized]
        private Renderer m_Renderer;

        [System.NonSerialized]
        private Transform m_Transform;

        public Camera Camera {
            get { return m_Camera = m_Camera ?? CameraUtils.FindRootCamera(this.Transform); }
        }

        public Renderer Renderer { 
            get { return m_Renderer = m_Renderer ?? GetComponent<Renderer>(); }
        }

        public Transform Transform {
            get { return m_Transform = m_Transform ?? GetComponent<Transform>(); }
        }

        #endregion


        #region MonoBehaviour Methods

        private void OnEnable()
        {
            if (TouchDispatcher.Instance) {
                TouchDispatcher.Instance.Add(this, 0, true);
            }
        }

        private void OnDisable()
        {
            if (TouchDispatcher.Instance) {
                TouchDispatcher.Instance.Remove(this);
            }
        }

        #endregion


        #region ISingleTouchObserver Methods

        public bool OnTouchBegan(Touch touch) {
            return HitTest(touch);
        }

        public void OnTouchCancelled(Touch touch) {
            Debug.Log("Example Observer - " + gameObject.name + " - Touch Cancelled");
        }

        public void OnTouchEnded(Touch touch) {
            Debug.Log("Example Observer - " + gameObject.name + " - Touch Ended: " + touch.Position);
        }

        public void OnTouchMoved(Touch touch) {
            Debug.Log("Example Observer - " + gameObject.name + " - Touch Moved: " + touch.Position);
        }

        #endregion

        private bool HitTest(Touch touch) {
            var bounds = this.Renderer.bounds;
            bounds.extents += Vector3.forward;
            return bounds.Contains(CameraUtils.TouchToWorldPoint(touch, this.Transform, this.Camera));
        }
    }

}