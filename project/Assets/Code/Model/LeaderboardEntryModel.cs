namespace MAG.Model
{
	public class LeaderboardEntryModel
	{
		public string Name { get; }
		public int Score { get; }

		public LeaderboardEntryModel(string name, int score)
		{
			Name = name;
			Score = score;
		}
	}
}