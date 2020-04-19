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
        public static int BLUK_PRODUCT_PANEL_NUM_BY_COLUMN = 6;

        private List<List<PanelModel>> initialPanels = new List<List<PanelModel>>();

        public List<List<PanelModel>> Place()
        {
            PlacePanelsInitially();
            PutMark();
            return initialPanels;
        }

        public List<PanelModel> Produce()
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

        private void PlacePanelsInitially()
        {
            
            int totalPanelNum = 0;
            for (int i = 0; i < FrameModel.WIDTH_PANEL_NUM; i++)
            {
                initialPanels.Add(new List<PanelModel>());
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
                    initialPanels[i].Add(PanelModel.buildErasablePanel());
                }
                totalPanelNum += panelNum;
            }
            initialPanels.Shuffle();
        }

        private void PutMark()
        {
            for (int i = 0; i < initialPanels.Count; i++)
            {
                for (int j = 0; j < initialPanels[i].Count; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        initialPanels[i][j].SetMarkRandomly();
                        continue;
                    }
                    if (i == 0)
                    {
                        initialPanels[i][j].SetMarkRandomlyExceptFor(initialPanels[i][j - 1].Mark);
                        continue;
                    }
                    if (j == 0)
                    {
                        if (initialPanels[i - 1].Count > j)
                        {
                            initialPanels[i][j].SetMarkRandomlyExceptFor(initialPanels[i - 1][j].Mark);
                            continue;
                        }
                        initialPanels[i][j].SetMarkRandomly();
                        continue;
                    }
                    if (initialPanels[i - 1].Count > j)
                    {
                        initialPanels[i][j]
                            .SetMarkRandomlyExceptFor(new List<string>() { initialPanels[i][j - 1].Mark, initialPanels[i - 1][j].Mark });
                      continue;
                    }
                    initialPanels[i][j].SetMarkRandomlyExceptFor(initialPanels[i][j - 1].Mark);
                }
            }
        }
    }
}
