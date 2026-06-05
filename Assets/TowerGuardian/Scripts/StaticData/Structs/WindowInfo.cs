using System;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.UI;

namespace TowerGuardian.Scripts.StaticData.Structs
{
    [Serializable]
    public struct WindowInfo
    {
        public WindowType Type;
        public WindowBase Pefab;
    }
}
