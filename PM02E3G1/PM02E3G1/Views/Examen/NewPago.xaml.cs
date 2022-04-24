using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM02E3G1.Views.Examen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPago : ContentPage
    {
        public NewPago()
        {
            InitializeComponent();
            BindingContext = new ViewModels.ViewModelPagos();
        }

        


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            
        }
    }
}