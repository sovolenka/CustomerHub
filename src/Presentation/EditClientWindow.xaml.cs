using System.Windows;


namespace Presentation;


public partial class EditClientWindow : Window
{
    public EditClientWindow()
    {
        InitializeComponent();
    }

    private void editButtonClick(object sender, RoutedEventArgs e)
    {

    }

    private void cancelButtonClick(object sender, RoutedEventArgs e)
    {
        this.Close(); 
    }
}
