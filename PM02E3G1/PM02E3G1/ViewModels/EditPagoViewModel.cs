using Plugin.Media;
using Plugin.Media.Abstractions;
using PM02E3G1.Models;
using PM02E3G1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PM02E3G1.ViewModels
{
    public class EditPagoViewModel : BaseViewModel
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        #region Atributos
        public Guid _Id_Pago;
        public string _Decripcion;
        public DateTime _Fecha;
        public double _Monto;
        public string _Photo_recibo;
        private ImageSource _Camera;
        public bool isRefreshing = false;
        public object listViewSource;
        #endregion


        #region propiedades
        public Guid Id_Pago
        {
            get { return _Id_Pago; }
            set { _Id_Pago = value; OnPropertyChanged(); }
        }

        public string Descripcion
        {
            get { return _Decripcion; }
            set { _Decripcion = value; OnPropertyChanged(); }
        }

        public double Monto
        {
            get { return _Monto; }
            set { _Monto = value; OnPropertyChanged(); }
        }

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; OnPropertyChanged(); }
        }
        public ImageSource Camera
        {
            get { return _Camera; }
            set
            {
                _Camera = value; OnPropertyChanged();
            }
        }
        public string Photo_recibo
        {
            get { return _Photo_recibo; }
            set { _Photo_recibo = value; OnPropertyChanged(); }
        }



        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged(); }
        }
        public object ListViewSource
        {
            get { return this.listViewSource; }
            set { listViewSource = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ModeloPagos> _pagos = new ObservableCollection<ModeloPagos>();

        public ObservableCollection<ModeloPagos> listPagos
        {
            get { return _pagos; }
            set { _pagos = value; OnPropertyChanged(); }
        }
        #endregion

        #region comandos
        public ICommand CommandGuardarPago { get; set; }
        public ICommand CommandTomarFoto { get; set; }

        public ICommand CommandGoToNewPago { get; set; }
        public ICommand CommandModificarPago { get; set; }
        public ICommand CommandEliminarPago { get; set; }

        #endregion

        #region Metodos
        async void takeFoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", "Camara no disponible", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "AppRun",
                Name = "recibo",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Rear
            });

            if (file == null)
                return;



            if (file != null)
            {

                Camera = ImageSource.FromStream(() => file.GetStream());
                Byte[] imagenByte = null;
                using (MemoryStream memory = new MemoryStream())
                {

                    Stream stream = file.GetStream();
                    stream.CopyTo(memory);
                    imagenByte = memory.ToArray();
                    Photo_recibo = Convert.ToBase64String(imagenByte);
                }

            }

        }

        public async Task LoadData()
        {
            listPagos = firebaseHelper.getAllPagos();
        }


        public async Task Modificar()
        {
            if (string.IsNullOrEmpty(Descripcion))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe Completar el campo descripcion", "Salir");
                return;
            }
            else if (string.IsNullOrEmpty(Fecha.ToString()))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe Completar el campo fecha", "Salir");
                return;
            }
            else if (string.IsNullOrEmpty(Monto.ToString()) || Monto <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe Completar el campo monto", "Salir");
                return;
            }
            else if (string.IsNullOrEmpty(Photo_recibo))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe tomar una foto", "Salir");
                return;
            }
            else
            {
                var pago = new ModeloPagos()
                {
                    Id_Pago = _Id_Pago,
                    Descripcion = _Decripcion,
                    Fecha = _Fecha,
                    Monto = _Monto,
                    Photo_recibo = _Photo_recibo
                };

                await firebaseHelper.UpdatePago(pago);
                await App.Current.MainPage.Navigation.PushAsync(new Views.Examen.ListViewPagos());
            }
            
        }

        public async Task Eliminar()
        {
            await firebaseHelper.DeletePago(Id_Pago);
            await App.Current.MainPage.Navigation.PushAsync(new Views.Examen.ListViewPagos());  
        }
        #endregion

        #region contructor
        public EditPagoViewModel(ModeloPagos _modeloPagos)
        {

            Id_Pago = _modeloPagos.Id_Pago;
            Descripcion = _modeloPagos.Descripcion;
            Fecha = _modeloPagos.Fecha;
            Monto = _modeloPagos.Monto;
            Photo_recibo = _modeloPagos.Photo_recibo;
            
            LoadData();
            CommandTomarFoto = new Command(() => takeFoto());
            CommandModificarPago = new Command(() => Modificar());
            CommandEliminarPago = new Command(() => Eliminar());

        }

        public EditPagoViewModel()
        {
        }
        #endregion
    }
}
