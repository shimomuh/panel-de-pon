using System.Collections.Generic;
using PanelDePon.Domain;

namespace PanelDePon.Application
{
    public sealed class BattleSystem
    {

        public static int INITIAL_PANEL_NUM = 30;
        public static int MAX_INITIAL_PANEL_NUM_BY_COLUMN = 7;

        #region singleton
        private static BattleSystem instance = new BattleSystem();

        public static BattleSystem Instance
        {
            get { return instance; }
        }

        private BattleSystem()
        {

        }
        #endregion

        public List<List<PanelModel>> PlaceInitialPanels()
        {
            return PanelFactory.Instance.Place();
        }

        public List<List<PanelModel>> PrepareHiddenPanels()
        {
            return PanelFactory.Instance.Produce();
        }
    }
}
