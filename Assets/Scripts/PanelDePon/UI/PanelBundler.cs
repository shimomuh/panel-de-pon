using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PanelDePon.Domain;

namespace PanelDePon.UI {
    /// <summary>
	/// bundle model with view
	/// </summary>
    public class PanelBunlder : MonoBehaviour
    {
        #region singleton
        private static PanelBunlder instance = new PanelBunlder();

        public static PanelBunlder Instance
        {
            get { return instance; }
        }

        private PanelBunlder()
        {

        }
        #endregion

        private List<List<PanelView>> panelViewModels;
        private RectTransform frame;
        private PanelView panelSkeleton;
        private static int HIDDEN_PANEL_INDEX_BY_COLUMN = -3;
        private int hiddenPanelNumByColumn = 0;

        public void BundleModelWithView(RectTransform frame, PanelView panelSkeleton, List<List<PanelModel>> panelModels)
        {
            panelViewModels = new List<List<PanelView>>();
            this.frame = frame;
            this.panelSkeleton = panelSkeleton;
            for (var column = 0; column < panelModels.Count; column++)
            {
                panelViewModels.Add(new List<PanelView>());
                for (var row = 0; row < panelModels[column].Count; row++)
                {
                    PanelView panel = Instantiate<PanelView>(panelSkeleton);
                    panel.Initialize(panelModels[column][row], false);
                    SetPanelPosition(panel, column, row);
                    panel.SetParent(frame);
                    panelViewModels[column].Add(panel);
                }
            }
        }

        public void AddBundleModel(List<PanelModel> panelModels)
        {
            int row = DecrementHiddenPanelNumByColumn();
            for (var column = 0; column < panelModels.Count; column++)
            {
                PanelView panel = Instantiate<PanelView>(panelSkeleton);
                panel.Initialize(panelModels[column], false);
                SetPanelPosition(panel, column, row);
                panel.SetParent(frame);
                panelViewModels[column].Add(panel);
            }
        }

        /// <summary>
        /// change column to x coordinate, row to y coordinate.
        /// </summary>
        private void SetPanelPosition(PanelView panel, int column, int row)
        {
            var x = (int)(-PanelView.WIDTH * FrameModel.WIDTH_PANEL_NUM / 2 + PanelView.WIDTH / 2) + column * PanelView.WIDTH;
            var y = (int)(-PanelView.HEIGHT * FrameModel.HEIGHT_PANEL_NUM / 2 + PanelView.HEIGHT / 2 + row * PanelView.HEIGHT);
            panel.SetPosition(x, y);
        }

        private int DecrementHiddenPanelNumByColumn()
        {
            if (HIDDEN_PANEL_INDEX_BY_COLUMN >= hiddenPanelNumByColumn)
            {
                return HIDDEN_PANEL_INDEX_BY_COLUMN;
            }
            return --hiddenPanelNumByColumn;
        }
    }
}
