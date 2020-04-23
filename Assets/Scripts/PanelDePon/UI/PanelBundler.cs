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
        private static int HIDDEN_PANEL_INDEX_BY_COLUMN = -2;
        public static float LAST_PANEL_INITIAL_POSITION = -1215f;
        private int hiddenPanelNumByColumn = 0;

        public PanelBunlder Build(RectTransform frame, PanelView panelSkeleton)
        {
            this.frame = frame;
            this.panelSkeleton = panelSkeleton;
            return this;
        }

        public void BundleModelWithView(List<List<PanelModel>> panelModels)
        {
            panelViewModels = new List<List<PanelView>>();
            for (var column = 0; column < panelModels.Count; column++)
            {
                panelViewModels.Add(new List<PanelView>());
                for (var row = 0; row < panelModels[column].Count; row++)
                {
                    if (panelModels[column][row] == null) { continue; }
                    PanelView panel = Instantiate<PanelView>(panelSkeleton);
                    panel.Initialize(panelModels[column][row], false);
                    SetPanelPosition(panel, column, row, column, row);
                    SetActions(panel);
                    panel.SetParent(frame);
                    panelViewModels[column].Add(panel);
                }
            }
        }

        public void AddBundleModel(List<PanelModel> panelModels, float deltaUp = 0f)
        {
            IncrementCoordinate();
            int row = DecrementHiddenPanelNumByColumn();
            for (var column = 0; column < panelModels.Count; column++)
            {
                PanelView panel = Instantiate<PanelView>(panelSkeleton);
                panel.Initialize(panelModels[column], false);
                SetPanelPosition(panel, column, row, column, 0, deltaUp);
                SetActions(panel);
                panel.SetParent(frame);
                panelViewModels[column].Insert(0, panel);
            }
        }

        private void IncrementCoordinate()
        {
            for (var column = 0; column < panelViewModels.Count; column++)
            {
                for (var row = 0; row < panelViewModels[column].Count; row++)
                {
                    panelViewModels[column][row].IncrementRow();
                }
            }
        }

        public float LastPanelPositonY()
        {
            return panelViewModels[0][0].transform.localPosition.y;
        }

        /// <summary>
        /// change column to x coordinate, row to y coordinate.
        /// </summary>
        private void SetPanelPosition(PanelView panel, int column, int row, int columnIndex, int rowIndex, float deltaUp = 0f)
        {
            float x = -PanelView.WIDTH * FrameModel.WIDTH_PANEL_NUM / 2 + PanelView.WIDTH / 2 + column * PanelView.WIDTH;
            float y = -PanelView.HEIGHT * FrameModel.HEIGHT_VISIBLE_PANEL_NUM / 2 + PanelView.HEIGHT / 2 + row * PanelView.HEIGHT;
            panel.SetPositionWithCoordinate(x, y + deltaUp, columnIndex, rowIndex);
        }

        private int DecrementHiddenPanelNumByColumn()
        {
            if (HIDDEN_PANEL_INDEX_BY_COLUMN >= hiddenPanelNumByColumn)
            {
                return HIDDEN_PANEL_INDEX_BY_COLUMN;
            }
            return --hiddenPanelNumByColumn;
        }

        private void SetActions(PanelView panel)
        {
            panel.OnSwapLeft = SwapLeft;
            panel.OnSwapRight = SwapRight;
        }

        private void SwapLeft(Vector2 position, int columnIndex, int rowIndex)
        {
            if (columnIndex == 0)
            {
                return;
            }
            for (int column = 0; column < panelViewModels.Count; column++)
            {
                for (int row = 0; row < panelViewModels[column].Count; row++)
                {
                    if (columnIndex == column && rowIndex == row)
                    {
                        var tmp = panelViewModels[column][row];
                        panelViewModels[column][row] = panelViewModels[column - 1][row];
                        panelViewModels[column - 1][row] = tmp;
                        var tmpPos = panelViewModels[column][row].transform.localPosition;
                        panelViewModels[column][row].SetPositionWithCoordinate(panelViewModels[column - 1][row].transform.localPosition.x, panelViewModels[column - 1][row].transform.localPosition.y, column, row);
                        panelViewModels[column - 1][row].SetPositionWithCoordinate(tmpPos.x, tmpPos.y, column - 1, row);
                        break;
                    }
                }
            }
        }

        private void SwapRight(Vector2 position, int columnIndex, int rowIndex)
        {
            if (columnIndex == FrameModel.WIDTH_PANEL_NUM - 1)
            {
                return;
            }
            for (int column = 0; column < panelViewModels.Count; column++)
            {
                for (int row = 0; row < panelViewModels[column].Count; row++)
                {
                    if (columnIndex == column && rowIndex == row)
                    {
                        var tmp = panelViewModels[column][row];
                        panelViewModels[column][row] = panelViewModels[column + 1][row];
                        panelViewModels[column + 1][row] = tmp;
                        var tmpPos = panelViewModels[column][row].transform.localPosition;
                        panelViewModels[column][row].SetPositionWithCoordinate(panelViewModels[column + 1][row].transform.localPosition.x, panelViewModels[column + 1][row].transform.localPosition.y, column, row);
                        panelViewModels[column + 1][row].SetPositionWithCoordinate(tmpPos.x, tmpPos.y, column + 1, row);
                        break;
                    }
                }
            }
        }
    }
}
