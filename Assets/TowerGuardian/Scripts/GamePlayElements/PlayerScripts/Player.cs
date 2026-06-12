using TowerGuardian.Scripts.GamePlayElements.Ability.AbilityInfrastructure;
using UnityEngine;
using YG;

namespace TowerGuardian.Scripts.GamePlayElements.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        private float _saveInterval = 0.5f;
        private float _lastSaveTime;

        [field: SerializeField]
        public PlayerAttacker Attacker { get; private set; }

        [field: SerializeField]
        public Inventory Inventory { get; private set; }

        [field: SerializeField]
        public AttackZone AttackZone { get; private set; }

        [field: SerializeField]
        public PlayerExperience Experience { get; private set; }

        [field: SerializeField]
        public PlayerCardConfigContainer CardHolder { get; private set; }

        [field: SerializeField]
        public AllAbilities AllAbilities { get; private set; }

        [field: SerializeField]
        public EnemyDetector Detector { get; private set; }

        [field: SerializeField]
        public PlayerHealth Health { get; private set; }

        [field: SerializeField]
        public PlayerMover PlayerMover { get; private set; }

        [field: SerializeField]
        public QuestPointer QuestPointer { get; private set; }

        [field: SerializeField]
        public Fist Fist { get; private set; }

        [field: SerializeField]
        public ResourceCollector ResourceCollector { get; private set; }

        [field: SerializeField]
        public HealthRegeneration HealthRegeneration { get; private set; }

        [field: SerializeField]
        public Mover Mover { get; private set; }

        [field: SerializeField]
        public Camera ModelViewCamera { get; private set; }

        [field: SerializeField]
        public Animator Animator { get; private set; }

        [field: SerializeField]
        public Rotator Rotator { get; private set; }

        [field: SerializeField]
        public PlayerAnimator PlayerAnimator { get; private set; }

        [field: SerializeField]
        public EnemyDetector EnemyDetector { get; private set; }

        public bool IsAlive { get; private set; } = true;

        private void Start()
        {
            YG2.saves.PlayerPosition = transform.position;
            YG2.SaveProgress();
        }

        private void Update()
        {
            if (Time.time >= _lastSaveTime + _saveInterval)
            {
                _lastSaveTime = Time.time;
                YG2.saves.PlayerPosition = transform.position;
                YG2.SaveProgress();
            }
        }

        private void OnDestroy()
        {
            IsAlive = false;
        }
    }
}
