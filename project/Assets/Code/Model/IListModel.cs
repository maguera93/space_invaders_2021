namespace MAG.Model
{
	public interface IListModel<TItemModel>
	{
		TItemModel GetItem(int index);
		int NumItems { get; }
	}
}