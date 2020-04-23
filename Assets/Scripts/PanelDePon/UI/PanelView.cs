using UnityEngine;
using PanelDePon.Domain;
using UnityEngine.EventSystems;
using System;

namespace PanelDePon.UI
{
    /// <summary>
    /// Panel Factory
    /// </summary>
    public class PanelView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static int HEIGHT = 90, WIDTH = 90;

        [SerializeField] private GameObject sun;
        [SerializeField] private GameObject cloud;
        [SerializeField] private GameObject rain;
        [SerializeField] private GameObject moon;
        [SerializeField] private GameObject thunder;
        [SerializeField] private GameObject snow;
        [SerializeField] private GameObject rainbow;

        private PanelModel model;

        private GameObject mark;

        private int speed;

        private float beginDragX;

        public Action<Vector2, int, int> OnSwapLeft;
        public Action<Vector2, int, int> OnSwapRight;

        public int columnIndex, rowIndex;

        void Awake()
        {
            UnityEngine.Application.targetFrameRate = 60;
            speed = 10; // normal?
        }

        void Update()
        {
            if (model.IsRisingUp) {
                SetPosition(transform.localPosition.x, transform.localPosition.y + Time.deltaTime * speed);
            }
        }

        public void Initialize(PanelModel model, bool canAppearSpecial) // canAppearSpecial is not implement
        {
            this.model = model;
            switch (model.Mark)
            {
                case "Sun":
                    mark = sun;
                    break;
                case "Cloud":
                    mark = cloud;
                    break;
                case "Rain":
                    mark = rain;
                    break;
                case "Moon":
                    mark = moon;
                    break;
                case "Thunder":
                    mark = thunder;
                    break;
                case "Snow":
                    mark = snow;
                    break;
                case "Rainbow":
                    mark = rainbow;
                    break;
            }
            gameObject.SetActive(true);
            mark.SetActive(true);
        }

        public void SetParent(Transform p)
        {
            transform.SetParent(p, false);
        }

        private void SetPosition(float x, float y)
        {
            transform.localPosition = new Vector3(x, y, 0);
        }

        private void SetCoordinate(int column, int row)
        {
            columnIndex = column;
            rowIndex = row;
        }

        public void SetPositionWithCoordinate(float x, float y, int column, int row)
        {
            SetPosition(x, y);
            SetCoordinate(column, row);
        }

        public void IncrementRow()
        {
            rowIndex++;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            beginDragX = GetLocalPosition(eventData.position).x;
        }

        public void OnEndDrag(PointerEventData eventData)
        {

            if (GetLocalPosition(eventData.position).x < beginDragX)
            {
                OnSwapLeft(transform.localPosition, columnIndex, rowIndex);
            }
            if (GetLocalPosition(eventData.position).x > beginDragX)
            {
                OnSwapRight(transform.localPosition, columnIndex, rowIndex);
            }
        }

        /// <summary>
        /// In case of "Screen Space - Overlay" of "Canvas" render mode
        /// must chane screenPosition to localPosition
        /// (if UI canvas may be able to ignore this problem...)
        /// </summary>
        private Vector2 GetLocalPosition(Vector2 screenPosition)
        {
            return transform.InverseTransformPoint(screenPosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // do nothing
        }
    }
}
