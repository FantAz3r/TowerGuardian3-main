using System.Collections.Generic;
using UnityEngine.SceneManagement;
using YG;

public class PortalSwitcher
{
    private List<Portal> _portals = new();

    public void Init(List<Portal> portals)
    {
        _portals = portals;

        if (SceneManager.GetActiveScene().name == LevelID.Tower.ToString())
        {
            EnableTowerPortal();
        }
        else
        {
            DisablePortals();
        }
    }

    private void EnablePortals()
    {
        foreach (Portal portal in _portals)
        {
            portal.gameObject.SetActive(true);
        }
    }

    private void DisablePortals()
    {
        foreach (Portal portal in _portals)
        {
            portal.gameObject.SetActive(false);
        }
    }

    private void EnableTowerPortal()
    {
        DisablePortals();

         var firstPortal = _portals.Find(portal => portal.NextLevel == LevelID.Level1);
        firstPortal.gameObject.SetActive(true);

        if (YG2.saves.LevelsProgress == null || YG2.saves.LevelsProgress.Count == 0)
            return;

        for (int i = 1; i < _portals.Count; i++)
        {
            var prevLevelData = YG2.saves.LevelsProgress[i];

            if (prevLevelData.IsComplite)
            {
                var portal = _portals.Find(portal => portal.NextLevel == YG2.saves.LevelsProgress[i].Level +1);
                if (portal != null)
                    portal.gameObject.SetActive(true);
            }
            else
            {
                break;
            }
        }
    }
}
