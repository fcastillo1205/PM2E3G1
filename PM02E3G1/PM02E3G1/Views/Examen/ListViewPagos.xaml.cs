using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM02E3G1.Views.Examen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPagos : ContentPage
    {
        public ListViewPagos()
        {
            InitializeComponent();
            BindingContext = new ViewModels.ViewModelPagos();
        }

        public async void listViewPagos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditPago(e.SelectedItem as Models.ModeloPagos));
        }
    }
}