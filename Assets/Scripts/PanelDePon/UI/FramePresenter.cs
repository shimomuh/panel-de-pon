using System.Collections.Generic;
using UnityEngine;
using PanelDePon.Application;
using PanelDePon.Domain;

namespace PanelDePon.UI
{
    /// <summary>
    /// Business Presenter within PanelPresenter
    /// </summary>
    public class FramePresenter : MonoBehaviour
    {

        [SerializeField] private PanelPresenter panelSkeleton;

        void Awake()
        {
            RectTransform frame = GetComponent<RectTransform>();
            List<List<PanelModel>> panels = BattleSystem.Instance.PlaceInitialPanels();

            for (var column = 0; column < panels.Count; column++)
            {
                for (var row = 0; row < panels[column].Count; row++)
                {
                    PanelPresenter panel = Instantiate<PanelPresenter>(panelSkeleton);
                    panel.Initialize(panels[column][row].Mark, false);
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
            var x = (int)(-PanelPresenter.WIDTH * FrameModel.WIDTH_PANEL_NUM / 2 + PanelPresenter.WIDTH / 2) + column * PanelPresenter.WIDTH;
            var y = (int)(-PanelPresenter.HEIGHT * FrameModel.HEIGHT_PANEL_NUM / 2 + PanelPresenter.HEIGHT / 2 + row * PanelPresenter.HEIGHT);
            panel.SetPosition(x, y);
        }
    }
}