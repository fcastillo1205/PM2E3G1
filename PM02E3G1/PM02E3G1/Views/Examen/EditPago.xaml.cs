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
    public partial class EditPago : ContentPage
    {
        public EditPago()
        {
            InitializeComponent();
            BindingContext = new ViewModels.EditPagoViewModel();
        }
        public EditPago(Models.ModeloPagos _modeloPagos)
        {
            InitializeComponent();
            BindingContext = new ViewModels.EditPagoViewModel(_modeloPagos);
        }
    }
}