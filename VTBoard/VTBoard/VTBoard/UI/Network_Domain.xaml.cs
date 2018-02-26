using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VTBoard.UI
{
    public partial class Network_Domain : ContentPage
    {
        public Network_Domain()
        {
            InitializeComponent();
            BindingContext = new Network_DomainViewModel();
        }
    }
}
