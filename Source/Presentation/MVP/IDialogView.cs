namespace Presentation.MVP
{
    public interface IDialogView
    {
        bool? Result { get; }

        bool? ShowDialog();
    }
}