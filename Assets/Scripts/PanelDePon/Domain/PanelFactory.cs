using System.Collections.Generic;
using SupportDomain;
using UnityEngine;

namespace PanelDePon.Domain
{
    public interface IPanelFactory
    {
        List<List<PanelModel>> PutVisiblePanelsRandomly();
        List<PanelModel> InsertHiddenPanels();
    }

    public sealed class PanelFactory : IPanelFactory
    {
        #region singleton
        private static PanelFactory instance = new PanelFactory();

        public static PanelFactory Instance
        {
            get { return instance; }
        }

        private PanelFactory()
        {

        }
        #endregion

        public static int INITIAL_PANEL_NUM = 30;
        public static int MAX_INITIAL_PANEL_NUM_BY_COLUMN = 7;

        public List<List<PanelModel>> PutVisiblePanelsRandomly()
        {
            List<List<PanelModel>> visiblePanels = AssignVisiblePanels(GetRandomPanelNums());
            visiblePanels = PutMark(visiblePanels);
            return visiblePanels;
        }

        public List<PanelModel> InsertHiddenPanels()
        {
            List<PanelModel> panels = new List<PanelModel>();
            for (int i = 0; i < FrameModel.WIDTH_PANEL_NUM; i++)
            {
                PanelModel model = new PanelModel();
                if (i == 0 || i == 1)
                {
                    model.SetMarkRandomly();
                    panels.Add(model);
                    continue;
                }
                if (panels[i - 2].Mark == panels[i - 1].Mark)
                {
                    model.SetMarkRandomlyExceptFor(panels[i - 1].Mark);
                    panels.Add(model);
                    continue;
                }
                model.SetMarkRandomly();
                panels.Add(model);
            }
            return panels;
        }

        private List<int> GetRandomPanelNums()
        {
            List<int> panelNums = new List<int>();
            int totalPanelNum = 0;
            for (int i = 0; i < FrameModel.WIDTH_PANEL_NUM; i++)
            {
                int panelNum = 0;
                int essentialPanelNum = INITIAL_PANEL_NUM - totalPanelNum - (FrameModel.WIDTH_PANEL_NUM - i - 1) * MAX_INITIAL_PANEL_NUM_BY_COLUMN;
                if (i == FrameModel.WIDTH_PANEL_NUM - 1)
                {
                    panelNum = essentialPanelNum;
                }
                else if (essentialPanelNum < 0)
                {
                    panelNum = Random.Range(0, MAX_INITIAL_PANEL_NUM_BY_COLUMN + 1);
                }
                else
                {
                    panelNum = Random.Range(essentialPanelNum, MAX_INITIAL_PANEL_NUM_BY_COLUMN + 1);
                }
                panelNums.Add(panelNum);
                totalPanelNum += panelNum;
            }
            panelNums.Shuffle();
            return panelNums;
        }

        private List<List<PanelModel>> AssignVisiblePanels(List<int> panelNums)
        {
            List<List<PanelModel>> visiblePanels = new List<List<PanelModel>>();
            for (int i = 0; i < FrameModel.WIDTH_PANEL_NUM; i++)
            {
                visiblePanels.Add(new List<PanelModel>());
                for (int j = 0; j < FrameModel.HEIGHT_VISIBLE_PANEL_NUM; j++)
                {
                    if (panelNums[i] > j)
                    {
                        visiblePanels[i].Add(PanelModel.buildErasablePanel());
                        continue;
                    }
                    visiblePanels[i].Add(null);
                }
            }
            return visiblePanels;
        }

        private List<List<PanelModel>> PutMark(List<List<PanelModel>> visiblePanels)
        {
            for (int i = 0; i < visiblePanels.Count; i++)
            {
                for (int j = 0; j < visiblePanels[i].Count; j++)
                {
                    if (visiblePanels[i][j] == null) { continue; }
                    if (i == 0 && j == 0)
                    {
                        visiblePanels[i][j].SetMarkRandomly();
                        continue;
                    }
                    if (i == 0)
                    {
                        visiblePanels[i][j].SetMarkRandomlyExceptFor(visiblePanels[i][j - 1].Mark);
                        continue;
                    }
                    if (j == 0)
                    {
                        if (visiblePanels[i - 1][j] != null)
                        {
                            visiblePanels[i][j].SetMarkRandomlyExceptFor(visiblePanels[i - 1][j].Mark);
                            continue;
                        }
                        visiblePanels[i][j].SetMarkRandomly();
                        continue;
                    }
                    if (visiblePanels[i - 1][j] != null)
                    {
                        visiblePanels[i][j]
                            .SetMarkRandomlyExceptFor(new List<string>() { visiblePanels[i][j - 1].Mark, visiblePanels[i - 1][j].Mark });
                      continue;
                    }
                    visiblePanels[i][j].SetMarkRandomlyExceptFor(visiblePanels[i][j - 1].Mark);
                }
            }
            return visiblePanels;
        }
    }
}
