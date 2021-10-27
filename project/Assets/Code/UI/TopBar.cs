using TMPro;
using MAG.Model;
using MAG.Popups;
using UnityEngine;

namespace MAG.Game
{
    public class TopBar : PopupStateModel<TopBar, PlayerModel>
    {
        [SerializeField] private TextMeshProUGUI playerHealth;
        [SerializeField] private TextMeshProUGUI score;

        public override void Setup(PlayerModel model)
        {
            base.Setup(model);
            model.damageTaken += OnModelTakenDamage;
            model.killedEnemy += OnModelKilledEnemy;

            UpdateUI();
        }

        private void OnModelKilledEnemy(PlayerModel obj)
        {
            UpdateUI();
        }

        private void OnModelTakenDamage(PlayerModel arg1, float arg2)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            playerHealth.text = model.HitPoints.ToString();
            score.text = model.Score.ToString();
        }
    }
}
