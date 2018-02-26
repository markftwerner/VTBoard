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
    public partial class PosterBoard_Main : ContentPage
    {
        public PosterBoard_Main()
        {
            InitializeComponent();
            BindingContext = new PosterBoard_MainViewModel();
        }
    }
}
