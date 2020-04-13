using System.Collections.Generic;
using System.Linq;
using PanelDePon.UI;
using UnityEngine;
using SupportDomain;

namespace PanelDePon.Application
{
    public sealed class BattleSystem
    {
        private static BattleSystem instance = new BattleSystem();
        public static int INITIAL_PANEL_NUM = 30;
        public static int MAX_INITIAL_PANEL_NUM_BY_COLUMN = 7;

        public static BattleSystem Instance
        {
            get { return instance; }
        }

        private BattleSystem()
        {

        }

        public List<List<int>> InitializePanelMatrix()
        {
            List<List<int>> matrix = DeployPanelRandomly();
            OverrideMark(matrix);
            return matrix;
        }

        private List<List<int>> DeployPanelRandomly()
        {

            List<List<int>> matrix = new List<List<int>>();
            int totalPanelNum = 0;
            for (int i = 0; i < FramePresenter.WIDTH_PANEL_NUM; i++)
            {
                matrix.Add(new List<int>());
                int panelNum = 0;
                int essentialPanelNum = INITIAL_PANEL_NUM - totalPanelNum - (FramePresenter.WIDTH_PANEL_NUM - i - 1) * MAX_INITIAL_PANEL_NUM_BY_COLUMN;
                if (i == FramePresenter.WIDTH_PANEL_NUM - 1)
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
                    matrix[i].Add(-1); // temporary value
                }
                totalPanelNum += panelNum;
            }
            matrix.Shuffle();
            return matrix;
        }

        private void OverrideMark(List<List<int>> matrix)
        {
            List<int> panels = new List<int>() { 0, 1, 2, 3, 4, 5 };
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        matrix[i][j] = panels.RandomTake();
                        continue;
                    }
                    List<int> selectedPanels = new List<int>();
                    int unduplicatedMark;
                    if (i == 0)
                    {
                        selectedPanels = (List<int>)panels.Extract(matrix[i][j - 1]);
                        unduplicatedMark = selectedPanels.RandomTake();
                        matrix[i][j] = unduplicatedMark;
                        continue;
                    }
                    if (j == 0)
                    {
                        if (matrix[i - 1].Count > j)
                        {
                            selectedPanels = (List<int>)panels.Extract(matrix[i - 1][j]);
                            unduplicatedMark = selectedPanels.RandomTake();
                            matrix[i][j] = unduplicatedMark;
                            continue;
                        }
                        matrix[i][j] = panels.RandomTake();
                        continue;
                    }
                    if (matrix[i - 1].Count > j)
                    {
                        selectedPanels = (List<int>)panels.Extract(new List<int>() { matrix[i][j - 1], matrix[i - 1][j] });
                        unduplicatedMark = selectedPanels.RandomTake();
                        matrix[i][j] = unduplicatedMark;
                        continue;
                    }
                    matrix[i][j] = panels.RandomTake();
                }
            }
        }
    }
}
