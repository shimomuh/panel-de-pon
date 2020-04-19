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

        public Action<Vector2> OnSwapLeft;
        public Action<Vector2> OnSwapRight;

        void Awake()
        {
            UnityEngine.Application.targetFrameRate = 60;
            speed = 20; // normal?
        }

        void Update()
        {
            if (model.IsRisingUp) {
                mark.transform.position = new Vector2(mark.transform.position.x, mark.transform.position.y + Time.deltaTime * speed);
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

        public void SetPosition(int x, int y)
        {
            transform.localPosition = new Vector2(x, y);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            beginDragX = eventData.position.x;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.position.x < beginDragX)
            {
                OnSwapLeft(transform.localPosition);
            }
            if (eventData.position.x > beginDragX)
            {
                OnSwapRight(transform.localPosition);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            // do nothing
        }
    }
}
