using System;
using System.Collections.Generic;
using TowerGuardian.Scripts.StaticData.Configs.Interfaces;
using TowerGuardian.Scripts.StaticData.Structs;
using UnityEditor;
using UnityEngine;
using YG;

namespace TowerGuardian.Scripts.StaticData.Configs
{
    public abstract class ShopConfig : ScriptableObject, IShopConfig
    {
        private const string RuLanguage = "ru";
        private const string EnLanguage = "en";
        private const string TRLanguage = "tr";

        [SerializeField]
        private Sprite _icon;

        [SerializeField]
        private string _nameRu;
        [SerializeField]
        private string _nameEn;
        [SerializeField]
        private string _nameTr;

        [SerializeField]
        private string _descriptionRU;
        [SerializeField]
        private string _descriptionEN;
        [SerializeField]
        private string _descriptionTR;

        [SerializeField]
        private List<CostInfo> _costs = new List<CostInfo>();

        [SerializeField]
        [HideInInspector]
        private string _id;

        public string ID
        {
            get
            {
                if (string.IsNullOrEmpty(_id))
                {
#if UNITY_EDITOR
                    _id = Guid.NewGuid().ToString();
                    EditorUtility.SetDirty(this);
#endif
                }

                return _id;
            }
        }

        public Sprite Icon => _icon;

        public string Name => OnCorrectLanguage(_nameRu, _nameEn, _nameTr);

        public string Description => OnCorrectLanguage(_descriptionRU, _descriptionEN, _descriptionTR);

        public IReadOnlyList<CostInfo> Costs => _costs;

        public virtual List<CostInfo> GetCosts() => _costs;

        public virtual List<CostInfo> GetSellCosts() => _costs;

        private string OnCorrectLanguage(string ru, string en, string tr)
        {
            string lang = YG2.lang;

            switch (lang)
            {
                case RuLanguage:
                    return ru;
                case EnLanguage:
                    return en;
                case TRLanguage:
                    return tr;
                default:
                    return string.Empty;
            }
        }
    }
}