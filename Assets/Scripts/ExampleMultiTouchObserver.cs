namespace ExampleSagoTouch {

    using SagoTouch;
    using System.Collections.Generic;
    using Touch = SagoTouch.Touch;
    using UnityEngine;

    public class ExampleMultiTouchObserver : MonoBehaviour, ISingleTouchObserver {

        #region Fields

        [System.NonSerialized]
        private Camera m_Camera;

        [System.NonSerialized]
        private Renderer m_Renderer;

        [System.NonSerialized]
        private List<Touch> m_Touches = new List<Touch>();

        [System.NonSerialized]
        private Transform m_Transform;

        [SerializeField]
        private Vector3 m_CollectiveCenter;

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

        public List<Touch> Touches {
            get { return m_Touches = m_Touches ?? new List<Touch>(); }
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
            if (HitTest(touch)) {
                this.Touches.Add(touch);
                return true;
            }
            return false;
        }

        public void OnTouchCancelled(Touch touch) {
            OnTouchEnded(touch);
        }

        public void OnTouchEnded(Touch touch) {
            this.Touches.Remove(touch);
        }

        public void OnTouchMoved(Touch touch) {
            m_CollectiveCenter = Vector3.zero;
            for (int i = 0; i < this.Touches.Count; i++) {
                m_CollectiveCenter += CameraUtils.TouchToWorldPoint(Touches[i], this.Transform, this.Camera);
            }
            m_CollectiveCenter /= this.Touches.Count;

            transform.position = Vector2.SmoothDamp(transform.position, m_CollectiveCenter, ref m_Velocity, m_MaximumVelocity * Time.fixedDeltaTime);
        }

        #endregion

    }

}