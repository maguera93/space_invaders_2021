using MAG.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MAG.Popups
{
	public class EndPopup : PopupStateModel<EndPopup, PlayerModel>
	{
		[SerializeField] private Button playButton;
        [SerializeField] private GameObject leaderboardEntryPrefab;
        [SerializeField] private Transform leaderboradContent;

        private List<LeaderboardEntry> leaderboardEntries;
        private LeaderboardModel leaderboardModel;
		
		public override void Init()
		{
			base.Init();
			playButton.onClick.AddListener(OnPlayButtonClicked);
            leaderboardModel = new LeaderboardModel();
            leaderboardEntries = new List<LeaderboardEntry>();
            CreateLeaderboard();
        }

        protected override void OnOpen()
        {
            DisplayLeaderboard();
        }

        private void OnPlayButtonClicked()
		{
			SceneManager.LoadScene(1);
			Close();
		}

        private void CreateLeaderboard()
        {
            for (int i = 0; i < leaderboardModel.NumItems; ++i)
            {
                GameObject entryGo = Instantiate(leaderboardEntryPrefab, leaderboradContent);
                LeaderboardEntry entry = entryGo.GetComponent<LeaderboardEntry>();
                leaderboardEntries.Add(entry);
            }
        }

        private void DisplayLeaderboard()
        {
            LeaderboardEntryModel playerEntry = new LeaderboardEntryModel("Player", model.Score);
            leaderboardModel.AddItem(playerEntry);

            for (int i = 0; i < leaderboardEntries.Count; ++i)
            {
                leaderboardEntries[i].Setup(leaderboardModel.GetItem(i), i + 1);
            }
        }
		
		public override void Dispose()
		{
			base.Dispose();
			playButton.onClick.RemoveListener(OnPlayButtonClicked);
		}
	}
}
