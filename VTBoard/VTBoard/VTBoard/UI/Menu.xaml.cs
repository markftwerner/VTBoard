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
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
            BindingContext = new MenuViewModel();
        }
    }
}
