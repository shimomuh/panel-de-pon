using UnityEngine;
using PanelDePon.Domain;

namespace PanelDePon.UI
{
    /// <summary>
    /// Panel Factory
    /// </summary>
    public class PanelPresenter : MonoBehaviour
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

        private int speed;

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
            mark.SetActive(true);
        }

        public void SetParent(Transform p)
        {
            mark.transform.SetParent(p, false);
        }

        public void SetPosition(int x, int y)
        {
            mark.transform.position = new Vector2(x, y);
        }
    }
}
