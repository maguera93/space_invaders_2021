using System.Collections.Generic;
using UnityEngine;

namespace MAG.Model
{
	public class LeaderboardModel : IListModel<LeaderboardEntryModel>
	{
        private const int LEADERBOARD_LENGHT = 100;

		public int NumItems => entries.Count;

		public LeaderboardModel()
		{
			//mock data
			Random.InitState(123);

			for (int i = 0; i < LEADERBOARD_LENGHT; ++i)
			{
				var r = Random.Range(0, NameList.Names.Length);
				var p = new LeaderboardEntryModel($"{NameList.Names[r]}{(i * 317) % 100}", Random.Range(0, 1000) * 50);
				entries.Add(p);
			}

            SortEntries();
		}

		public LeaderboardEntryModel GetItem(int index) => entries[index];

		public void AddItem(LeaderboardEntryModel model)
		{
            entries.RemoveAt(LEADERBOARD_LENGHT - 1);
			entries.Add(model);
            SortEntries();
        }

        public void SortEntries()
        {
            entries.Sort((a, b) => a.Score.CompareTo(b.Score));
            entries.Reverse();
        }

		private readonly List<LeaderboardEntryModel> entries = new List<LeaderboardEntryModel>();
	}
}