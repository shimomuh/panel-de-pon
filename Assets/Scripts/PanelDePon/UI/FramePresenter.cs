using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PanelDePon.UI
{
    public class FramePresenter : MonoBehaviour
    {
        [SerializeField] private PanelPresenter panelSkeleton;

        void Start()
        {
            RectTransform frame = GetComponent<RectTransform>();
            for (var i = 0; i < 24; i++)
            {
                PanelPresenter a = Instantiate<PanelPresenter>(panelSkeleton);
                a.Initialize();
                a.SetPosition(-250 + i % 6 * 100, -450 + (int)(i / 6) * 100);
                a.SetParent(frame, false);
            }
        }
    }

}