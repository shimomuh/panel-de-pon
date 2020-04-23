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

        [SerializeField] private PanelView panelSkeleton;

        void Awake()
        {
            RectTransform frame = GetComponent<RectTransform>();
            List<List<PanelModel>> panels = BattleSystem.Instance.PlaceInitialPanels();
            PanelBunlder.Instance.BundleModelWithView(frame, panelSkeleton, panels);
            PanelBunlder.Instance.AddBundleModel(BattleSystem.Instance.PrepareHiddenPanels());
            PanelBunlder.Instance.AddBundleModel(BattleSystem.Instance.PrepareHiddenPanels());
            PanelBunlder.Instance.AddBundleModel(BattleSystem.Instance.PrepareHiddenPanels());
        }

        private void Update()
        {
            float deltaUp = PanelBunlder.Instance.LastPanelPositonY() - PanelBunlder.LAST_PANEL_INITIAL_POSITION;
            if (deltaUp >= PanelView.HEIGHT)
            {
                PanelBunlder.Instance.AddBundleModel(BattleSystem.Instance.PrepareHiddenPanels(), deltaUp - PanelView.HEIGHT);
            }
        }
    }
}