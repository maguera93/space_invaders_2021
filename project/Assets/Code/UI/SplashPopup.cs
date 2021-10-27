using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MAG.Popups
{
	public class SplashPopup : PopupState<SplashPopup>
	{
		[SerializeField] private Button playButton;

		public override void Init()
		{
			base.Init();
			playButton.onClick.AddListener(OnPlayButtonClicked);
		}

		private void OnPlayButtonClicked()
		{
			SceneManager.LoadScene(1);
			Close();
		}

		public override void Dispose()
		{
			base.Dispose();
			playButton.onClick.RemoveListener(OnPlayButtonClicked);
		}
	}
}