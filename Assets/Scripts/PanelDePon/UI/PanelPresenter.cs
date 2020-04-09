﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PanelDePon.UI
{
    public class PanelPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject sun;
        [SerializeField] private GameObject cloud;
        [SerializeField] private GameObject rain;
        [SerializeField] private GameObject moon;
        [SerializeField] private GameObject thunder;
        [SerializeField] private GameObject snow;
        [SerializeField] private GameObject rainbow;

        private GameObject mark;

        public void Initialize()
        {
            switch((int)Random.Range(0, 5))
            {
                case 0:
                    mark = sun;
                    break;
                case 1:
                    mark = cloud;
                    break;
                case 2:
                    mark = rain;
                    break;
                case 3:
                    mark = moon;
                    break;
                case 4:
                    mark = thunder;
                    break;
                case 5:
                    mark = snow;
                    break;
                default:
                    throw new System.Exception("Out!");
            }
            mark.SetActive(true);
        }

        public void SetParent(Transform p, bool worldPositionStays)
        {
            mark.transform.SetParent(p, worldPositionStays);
        }

        public void SetPosition(int x, int y)
        {
            mark.transform.position = new Vector2(x, y);
        }
    }
}