using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanelDePon.Application;

namespace PanelDePon.UI
{
    /// <summary>
    /// Business Presenter within PanelPresenter
    /// </summary>
    public class FramePresenter : MonoBehaviour
    {
        public static int WIDTH_PANEL_NUM = 6, HEIGHT_PANEL_NUM = 12; // FIXME: domainに定義されるべき

        [SerializeField] private PanelPresenter panelSkeleton;

        void Awake()
        {
            RectTransform frame = GetComponent<RectTransform>();
            List<List<int>> matrix = BattleSystem.Instance.InitializePanelMatrix();

            for (var column = 0; column < matrix.Count; column++)
            {
                for (var row = 0; row < matrix[column].Count; row++)
                {
                    PanelPresenter panel = Instantiate<PanelPresenter>(panelSkeleton);
                    panel.Initialize(matrix[column][row], false);
                    SetPanelPosition(panel, column, row);
                    panel.SetParent(frame);
                }
            }
        }

        /// <summary>
        /// change column to x coordinate, row to y coordinate.
        /// </summary>
        private void SetPanelPosition(PanelPresenter panel, int column, int row)
        {
            var x = (int)(-PanelPresenter.WIDTH * WIDTH_PANEL_NUM / 2 + PanelPresenter.WIDTH / 2) + column * PanelPresenter.WIDTH;
            var y = (int)(-PanelPresenter.HEIGHT * HEIGHT_PANEL_NUM / 2 + PanelPresenter.HEIGHT / 2 + row * PanelPresenter.HEIGHT);
            panel.SetPosition(x, y);
        }
    }
}