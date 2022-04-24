using PM02E3G1.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PM02E3G1.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}