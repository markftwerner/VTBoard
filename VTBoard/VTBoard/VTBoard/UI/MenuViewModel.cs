using System;
using VTBoard.Model;
using VTBoard.Services;
using ReactiveUI;
using Splat;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Xamarin.Forms;
using System.Reactive;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace VTBoard.UI
{
    public class MenuViewModel : ReactiveObject
    {

        /// <summary>
        /// Changes view to the Main Board
        /// </summary>
        public ReactiveCommand<Unit, Unit> Board { get; set; }
        /// <summary>
        /// Refreshes the list of paired devices
        /// </summary>
        public ReactiveCommand<Unit, Unit> CreatePost { get; set; }
        /// <summary>
        /// Refreshes the list of paired devices
        /// </summary>
        public ReactiveCommand<Unit, Unit> History { get; set; }
        /// <summary>
        /// Refreshes the list of paired devices
        /// </summary>
        public ReactiveCommand<Unit, Unit> Networks { get; set; }
        /// <summary>
        /// Refreshes the list of paired devices
        /// </summary>
        public ReactiveCommand<Unit, Unit> Settings { get; set; }

        public MenuViewModel()
        {

            // The board command
            Board = ReactiveCommand.Create(() =>
            {
                
            });
            // The create post command
            CreatePost = ReactiveCommand.Create(() =>
            {
                
            });
            // The history command
            History = ReactiveCommand.Create(() =>
            {
                
            });
            // The networks command
            Networks = ReactiveCommand.Create(() =>
            {
                
            });
            // The settings command
            Settings = ReactiveCommand.Create(() =>
            {
                
            });


        }
    }
}
