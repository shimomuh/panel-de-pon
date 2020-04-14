using System.Collections.Generic;
using SupportDomain;
using UnityEngine;

namespace PanelDePon.Domain
{
    public sealed class PanelFactory
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

        private List<List<PanelModel>> panels = new List<List<PanelModel>>();

        public List<List<PanelModel>> Place()
        {
            PlacePanelsInitially();
            PutMark();
            return panels;
        }

        public void Produce()
        {
        }

        private void PlacePanelsInitially()
        {
            int totalPanelNum = 0;
            for (int i = 0; i < FrameModel.WIDTH_PANEL_NUM; i++)
            {
                panels.Add(new List<PanelModel>());
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
                for (int j = 0; j < panelNum; j++)
                {
                    panels[i].Add(new PanelModel());
                }
                totalPanelNum += panelNum;
            }
            panels.Shuffle();
        }

        private void PutMark()
        {
            for (int i = 0; i < panels.Count; i++)
            {
                for (int j = 0; j < panels[i].Count; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        panels[i][j].SetMarkRandomly();
                        continue;
                    }
                    if (i == 0)
                    {
                        panels[i][j].SetMarkRandomlyExceptFor(panels[i][j - 1].Mark);
                        continue;
                    }
                    if (j == 0)
                    {
                        if (panels[i - 1].Count > j)
                        {
                            panels[i][j].SetMarkRandomlyExceptFor(panels[i - 1][j].Mark);
                            continue;
                        }
                        panels[i][j].SetMarkRandomly();
                        continue;
                    }
                    if (panels[i - 1].Count > j)
                    {
                        panels[i][j]
                            .SetMarkRandomlyExceptFor(new List<string>() { panels[i][j - 1].Mark, panels[i - 1][j].Mark });
                      continue;
                    }
                    panels[i][j].SetMarkRandomlyExceptFor(panels[i][j - 1].Mark);
                }
            }
        }
    }
}
