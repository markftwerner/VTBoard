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
    public partial class Setting : ContentPage
    {
        public Setting()
        {
            InitializeComponent();
            BindingContext = new Setting_ViewModel();
        }
    }
}
