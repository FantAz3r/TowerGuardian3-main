using TowerGuardian.Scripts.Enums;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Entity
{
    public interface ITransfomable
    {
        Transform GetTransform();
        EntityType GetHealthType();
    }
}