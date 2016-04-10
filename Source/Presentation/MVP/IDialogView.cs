namespace Presentation.MVP
{
    public interface IDialogView : IView
    {
        bool? Result { get; }

        bool? ShowDialog();
    }
}