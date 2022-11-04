namespace ExampleSagoTouch {

    using SagoTouch;
    using UnityEngine;
    using Touch = SagoTouch.Touch;

    public class ExampleSingleTouchObserver : MonoBehaviour, ISingleTouchObserver {

        #region Fields

        [System.NonSerialized]
        private Camera m_Camera;

        [System.NonSerialized]
        private Renderer m_Renderer;

        [System.NonSerialized]
        private Touch m_Touch;

        [System.NonSerialized]
        private Transform m_Transform;

        [SerializeField]
        private Vector2 m_Velocity;

        [SerializeField]
        private float m_MaximumVelocity = 1.0f;

        #endregion


        #region Properties

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

        private void OnEnable() {
            if (TouchDispatcher.Instance) {
                TouchDispatcher.Instance.Add(this, 0, true);
            }
        }

        private void OnDisable() {
            if (TouchDispatcher.Instance) {
                TouchDispatcher.Instance.Remove(this);
            }
        }

        #endregion


        #region Methods

        private bool HitTest(Touch touch) {
            var bounds = this.Renderer.bounds;
            bounds.extents += Vector3.forward;
            return bounds.Contains(CameraUtils.TouchToWorldPoint(touch, this.Transform, this.Camera));
        }

        #endregion


        #region ISingleTouchObserver Methods

        public bool OnTouchBegan(Touch touch) {
            if (m_Touch == null && HitTest(touch)) {
                m_Touch = touch;
                return true;
            }
            return false;
        }

        public void OnTouchCancelled(Touch touch) {
            OnTouchEnded(touch);
        }

        public void OnTouchEnded(Touch touch) {
            m_Touch = null;
            m_Velocity = Vector2.zero;
        }

        public void OnTouchMoved(Touch touch) {
            transform.position = Vector2.SmoothDamp(transform.position, CameraUtils.TouchToWorldPoint(touch, this.Transform, this.Camera), ref m_Velocity, m_MaximumVelocity * Time.fixedDeltaTime);
        }

        #endregion

    }

}