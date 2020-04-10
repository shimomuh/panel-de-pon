using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PanelDePon.UI
{
    /// <summary>
    /// PanelPresenter を包含するビジネスプレゼンター
    /// </summary>
    public class FramePresenter : MonoBehaviour
    {
        public static int WIDTH_PANEL_NUM = 6, HEIGHT_PANEL_NUM = 12;

        [SerializeField] private PanelPresenter panelSkeleton;

        void Start()
        {
            RectTransform frame = GetComponent<RectTransform>();
            for (var i = 0; i < WIDTH_PANEL_NUM * HEIGHT_PANEL_NUM; i++)
            {
                PanelPresenter a = Instantiate<PanelPresenter>(panelSkeleton);
                a.Initialize();
                SetPanelPosition(a, i);
                a.SetParent(frame);
            }
        }

        private void SetPanelPosition(PanelPresenter panel, int index)
        {
            var x = (int)(-PanelPresenter.WIDTH * WIDTH_PANEL_NUM / 2 + PanelPresenter.WIDTH / 2) + index % WIDTH_PANEL_NUM * PanelPresenter.WIDTH;
            var y = (int)(-PanelPresenter.HEIGHT * HEIGHT_PANEL_NUM / 2 + PanelPresenter.HEIGHT / 2 + index / WIDTH_PANEL_NUM * PanelPresenter.HEIGHT);
            panel.SetPosition(x, y);
        }
    }

}