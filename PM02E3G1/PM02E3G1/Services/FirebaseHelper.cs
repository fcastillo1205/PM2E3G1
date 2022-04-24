using System;
using System.Collections.Generic;
using System.Text;
using PM02E3G1.Models;
using System.Threading.Tasks;
using Firebase.Database;
using System.Linq;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace PM02E3G1.Services
{
    public class FirebaseHelper
    {
        FirebaseClient firebase;
        public FirebaseHelper()
        {
            firebase = new FirebaseClient("https://pm2e3g1-default-rtdb.firebaseio.com/");
        }
        public ObservableCollection<ModeloPagos> getAllPagos()
        {
            var listPagos = firebase
                .Child("Pagos")
                .AsObservable<ModeloPagos>()
                .AsObservableCollection();
            return listPagos;
                          
        }
        public async Task addPago(ModeloPagos _modeloPagos)
        {
            await firebase
                .Child("Pagos")
                .PostAsync(new ModeloPagos()
                {
                    Id_Pago = Guid.NewGuid(),
                    Descripcion = _modeloPagos.Descripcion,
                    Monto = _modeloPagos.Monto,
                    Fecha = _modeloPagos.Fecha,
                    Photo_recibo = _modeloPagos.Photo_recibo
                });
        }

        public async Task UpdatePago(ModeloPagos _modeloPagos)
        {
            var toUpdatePago = (await firebase
                .Child("Pagos")
                .OnceAsync<ModeloPagos>()).Where(a => a.Object.Id_Pago == _modeloPagos.Id_Pago).FirstOrDefault();

            await firebase
                .Child("Pagos")
                .Child(toUpdatePago.Key)
                .PutAsync(new ModeloPagos() {   
                    Id_Pago = _modeloPagos.Id_Pago,
                    Descripcion = _modeloPagos.Descripcion,
                    Monto = _modeloPagos.Monto,
                    Fecha = _modeloPagos.Fecha,
                    Photo_recibo = _modeloPagos.Photo_recibo                                                
                });
        }

        public async Task DeletePago (Guid idPago)
        {
            var toDeletePago = (await firebase
                .Child("Pagos")
                .OnceAsync<ModeloPagos>())
                .Where(a=> a.Object.Id_Pago == idPago)
                .FirstOrDefault();

            await firebase 
                .Child("Pagos")
                .Child(toDeletePago.Key)
                .DeleteAsync();
        }

    }
}
