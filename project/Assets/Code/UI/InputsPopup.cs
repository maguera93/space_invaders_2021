using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MAG.Model;
using MAG.Popups;
using DG.Tweening;

namespace MAG.Popups
{
    public class InputsPopup : PopupStateModel<InputsPopup, PlayerModel>
    {
        [SerializeField] InputsButton rightButton;
        [SerializeField] InputsButton leftButton;
        [SerializeField] InputsButton shootButton;

        [Space]
        [SerializeField] CanvasGroup group;

        public override void Setup(PlayerModel model)
        {
            base.Setup(model);
            rightButton.onHold += MoveRight;
            leftButton.onHold += MoveLeft;
            shootButton.onHold += Shoot;

            model.die += OnDie;

            group.blocksRaycasts = true;
            group.interactable = true;

            group.alpha = 1;
            group.DOFade(0F, 1F).SetDelay(5F).SetEase(Ease.InExpo);
        }

        private void MoveRight()
        {
            model.MoveRight();
        }

        private void MoveLeft()
        {
            model.MoveLeft();
        }

        private void Shoot()
        {
            model.Shoot();
        }

        private void OnDie(PlayerModel model)
        {
            Close();

            group.blocksRaycasts = false;
            group.interactable = false;

            rightButton.onHold -= MoveRight;
            leftButton.onHold -= MoveLeft;
            shootButton.onHold -= Shoot;
        }
    }
}
