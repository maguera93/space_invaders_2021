using MAG.Utils;

namespace MAG.Popups
{
	public class PopupStateModel<T, V> : PopupState<T>, IPopupModel<V> where V : IModel
	{
		protected V model;
		
		public virtual void Setup(V m)
		{
			model = m;
		}
	}
}