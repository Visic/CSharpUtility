namespace WPFUtility {
    public interface ISelectableViewModel : IViewModelBase {
        NotifyingProperty<bool> IsSelected { get; }
    }
}
