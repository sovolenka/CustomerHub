using System.Windows;


namespace Presentation;

public partial class AddClientWindow : Window
{
    public AddClientWindow()
    {
        InitializeComponent();
    }

    private void addButtonClick(object sender, RoutedEventArgs e)
    {

    }

    private void cancelButtonClick(object sender, RoutedEventArgs e)
    {
       this.Close(); 
    }

}
