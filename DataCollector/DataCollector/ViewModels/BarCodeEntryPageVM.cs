
using DataCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels
{
    public class BarCodeEntryPageVM : BaseViewModel
    {
        private OrderProd _SelectedOrderProd;
        public OrderProd SelectedOrderProd
        {
            get { return _SelectedOrderProd; }
            set
            {
                _SelectedOrderProd = value;
                OnPropertyChanged("SelectedOrderProd");
            }
        }

        private string _VCHRNO;
        public string VCHRNO
        {
            get { return _VCHRNO; }
            set
            {
                _VCHRNO = value;
                OnPropertyChanged("VCHRNO");
            }
        }

        public Command AddCommand { get; set; }

        public List<OrderProd> OrderProdList { get; set; }

        public BarCodeEntryPageVM()
        {
            OrderProdList = Helpers.Data.OrderProdList;
            SelectedOrderProd = new OrderProd();
            AddCommand = new Command(ExecuteAddCommand);
        }

        private async void ExecuteAddCommand()
        {
            Helpers.Data.SelectedOrderProdList.Add(new OrderProd(SelectedOrderProd));
            await App.Current.MainPage.DisplayAlert("Success","Added Successfully","OK");
        }

        public void Entry_TextChanged(string oldText, string newText)
        {
            if (oldText != newText)
            {
                OrderProd orderProd = null;
                orderProd = OrderProdList.Where(op => op.MCODE.ToLower() == newText.ToLower()).FirstOrDefault();
                if (orderProd != null && orderProd.MCODE.ToLower() == newText.ToLower())
                {
                    //SelectedOrderProd = new OrderProd
                    //{
                    //    MCODE = orderProd.MCODE,
                    //    BARCODE = orderProd.BARCODE,
                    //    QUANTITY = orderProd.QUANTITY,
                    //    RATE = orderProd.RATE,
                    //    SUPPLIERCODE = orderProd.SUPPLIERCODE,
                    //    VCHRNO  = orderProd.VCHRNO
                    //};

                    SelectedOrderProd = new OrderProd(orderProd);
                }
                else
                {
                    SelectedOrderProd = new OrderProd();
                    orderProd = null;
                    SelectedOrderProd.MCODE = newText;
                }
            }

        }

    }
}
