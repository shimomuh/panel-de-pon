using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private GameObject mark;

        private void Awake()
        {
            weathers = new GameObject[] { sun, cloud, rain, moon, thunder, snow };
        }

        public void Initialize(int weatherIndex, bool canAppearSpecial) // canAppearSpecial is not implement
        {
            mark = weathers[weatherIndex];
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
