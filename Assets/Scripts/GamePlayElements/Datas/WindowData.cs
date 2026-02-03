using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Datas", menuName = "Datas/WindowData")]
public class WindowData : ScriptableObject
{
    public List<WindowInfo> WindowInfos = new List<WindowInfo>();
}
