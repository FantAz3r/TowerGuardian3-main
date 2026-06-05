using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.UI.Elements
{
    public class ScoreViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rewardCount;
        [SerializeField] private TMP_Text _scoreCount;
        [SerializeField] private TMP_Text _time;

        [SerializeField] private List<Image> _stars;

        private ScoreCounter _scoreCounter;

        private void Awake()
        {
            _scoreCounter = ServiceLocator.Get<IGameFactory>().ScoreCounter;
            _scoreCounter.LevelEnded += View;
        }

        private void OnDestroy()
        {
            _scoreCounter.LevelEnded -= View;
        }

        public void View(float score, float time, int stars, int reward = default)
        {
            if (reward != default && _rewardCount != null)
            {
                _rewardCount.text = reward.ToString();
            }

            _scoreCount.text = score.ToString();

            int minutes = (int)time / 60;
            int seconds = (int)time % 60;
            _time.text = $"{minutes:D2}:{seconds:D2}";

            DrowStars(stars);
        }

        private void DrowStars(int count)
        {
            if (count == 0)
                return;

            for (int i = 0; i < _stars.Count; i++)
            {
                _stars[i].gameObject.SetActive(false);
                _stars[i].transform.localScale = Vector3.zero;
            }

            Sequence seq = DOTween.Sequence();
            seq.SetUpdate(true);

            for (int i = 0; i < count && i < _stars.Count; i++)
            {
                var star = _stars[i];
                star.gameObject.SetActive(true);

                seq.Append(
                    star.transform.DOScale(Vector3.one, 0.5f)
                    .SetEase(Ease.OutElastic));

                seq.AppendInterval(0.3f).SetUpdate(true);
            }
        }
    }
}