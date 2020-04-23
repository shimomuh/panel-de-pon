using System.Collections.Generic;
using PanelDePon.Domain;

namespace PanelDePon.Application
{
    public sealed class BattleSystem
    {

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

        public List<List<PanelModel>> PutVisiblePanelsRandomly()
        {
            return PanelFactory.Instance.PutVisiblePanelsRandomly();
        }

        public List<PanelModel> InsertHiddenPanels()
        {
            return PanelFactory.Instance.InsertHiddenPanels();
        }
    }
}
