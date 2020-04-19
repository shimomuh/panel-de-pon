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

        private GameObject[] weathers;

        private PanelModel model;

        private GameObject mark;

        private Vector2 onBeginDragPosition;

        private int speed;

        public Action<Vector2> OnSwapLeft;
        public Action<Vector2> OnSwapRight;

        void Awake()
        {
            UnityEngine.Application.targetFrameRate = 60;
            speed = 20; // normal?
            weathers = new GameObject[] { sun, cloud, rain, moon, thunder, snow };
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
            gameObject.transform.SetParent(p, false);
        }

        public void SetPosition(int x, int y)
        {
            mark.transform.position = new Vector2(x, y);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDragPosition = eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.delta.x > onBeginDragPosition.x)
            {
                OnSwapLeft(mark.transform.position);
            }
            else {
                OnSwapRight(mark.transform.position);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            // do nothing
        }
    }
}
