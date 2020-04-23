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
            List<List<PanelModel>> panels = BattleSystem.Instance.PutVisiblePanelsRandomly();
            PanelBunlder.Instance.Build(frame, panelSkeleton).BundleModelWithView(panels);
            for (int i = 0; i < FrameModel.HEIGHT_PANEL_NUM_UNDER_HIDDEN; i++)
            {
                PanelBunlder.Instance.AddBundleModel(BattleSystem.Instance.InsertHiddenPanels());
            }
        }

        void Update()
        {
            float deltaUp = PanelBunlder.Instance.LastPanelPositonY() - PanelBunlder.LAST_PANEL_INITIAL_POSITION;
            if (deltaUp >= PanelView.HEIGHT)
            {
                PanelBunlder.Instance.AddBundleModel(BattleSystem.Instance.InsertHiddenPanels(), deltaUp - PanelView.HEIGHT);
            }
        }
    }
}