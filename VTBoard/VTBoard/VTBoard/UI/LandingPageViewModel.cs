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
    public class LandingPageViewModel : ReactiveObject
    {
        private IProgressDialogService _progressDialogService;
        private IBtDiscovery _btDiscovery;
        private IBtClient _btClient;

        private List<BtDeviceInfo> _pairedDevices;
        /// <summary>
        /// List of paired devices
        /// </summary>
        public List<BtDeviceInfo> PairedDevices
        {
            get { return _pairedDevices; }
            set { this.RaiseAndSetIfChanged(ref _pairedDevices, value); }
        }

        private BtDeviceInfo _selectedDevice;
        /// <summary>
        /// Current selected device
        /// </summary>
        public BtDeviceInfo SelectedDevice
        {
            get { return _selectedDevice; }
            set { this.RaiseAndSetIfChanged(ref _selectedDevice, value); }
        }

        private string _postInput;

        public string PostInput
        {
            get { return _postInput; }
            set { this.RaiseAndSetIfChanged(ref _postInput, value); }
        }

        private string _postTitle;

        public string PostTitle
        {
            get { return _postTitle; }
            set { this.RaiseAndSetIfChanged(ref _postTitle, value); }
        }

        private string _message;
        /// <summary>
        /// Last message from server
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { this.RaiseAndSetIfChanged(ref _message, value); }
        }

        private string _output;
        public string Output
        {
            get { return _output; }
            set { this.RaiseAndSetIfChanged(ref _output, value); }
        }

        private Dictionary<string, string> posts;

        /// <summary>
        /// List of supported operations by the server
        /// </summary>
        public ObservableCollection<string> Operations { get; set; }

        /// <summary>
        /// Refreshes the list of paired devices
        /// </summary>
        public ReactiveCommand<Unit, Unit> Refresh { get; set; }

        /// <summary>
        /// Opens the menu
        /// </summary>
        public ReactiveCommand<Unit, Unit> Menu { get; set; }

        /// <summary>
        /// Posts typed massage
        /// </summary>
        public ReactiveCommand<Unit, Unit> Post { get; set; }
        
        /// <summary>
        /// Requests post
        /// </summary>
        public ReactiveCommand<Unit, Unit> Request { get; set; }

        /// <summary>
        /// Executes an operation from the available list
        /// </summary>
        public ReactiveCommand<string, Unit> Execute { get; set; }
        public Dictionary<string, string> Posts { get => posts; set => posts = value; }

        public LandingPageViewModel()
        {
            _progressDialogService = Locator.Current.GetService<IProgressDialogService>();
            _btDiscovery = Locator.Current.GetService<IBtDiscovery>();
            _btClient = Locator.Current.GetService<IBtClient>();

            _btClient.ReceivedData += _btClient_ReceivedData;

            Operations = new ObservableCollection<string>();

            // Subscribe to the update of paired devices property
            this.WhenAnyValue(vm => vm._btDiscovery.PairedDevices)
                .Subscribe((pairedDevices) =>
                {
                    PairedDevices = pairedDevices;
                });

            // Display a progress dialog when the bluetooth discovery takes place
            this.WhenAnyValue(vm => vm._btDiscovery.IsBusy)
                .DistinctUntilChanged()
                .Subscribe((isBusy) =>
                {
                    if (isBusy)
                    {
                        _progressDialogService.Show("Please wait...");
                    }
                    else
                    {
                        _progressDialogService.Hide();
                    }
                });
            //Dictionary to hold posts
            posts = new Dictionary<string, string>();


            //Testing
            void DemoMode()
            {
                var tempstr = "Demo1,Demo Post 2,Latin";
                var titles = tempstr.Split(',');
                foreach (var op in titles)
                {
                    Operations.Add(op);
                }


                posts.Add("Demo1", "This is demo post 1.");
                posts.Add("Demo Post 2", "This is a post to demo without a Raspberry Pi");
                posts.Add("Latin", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vel ante velit. Ut congue est id mi faucibus, sed elementum mi bibendum. Phasellus volutpat, ex a ornare varius, mi ante ullamcorper dui, et finibus urna turpis in libero. Interdum et malesuada fames ac ante ipsum primis in faucibus. Nullam elit ante, malesuada at enim eu, gravida dignissim massa. In nulla metus, sagittis id faucibus ut, rutrum pulvinar est. Aliquam erat volutpat. Aenean suscipit lorem id nibh porta, vel iaculis metus sollicitudin. Praesent at commodo felis. Aenean scelerisque laoreet orci at vehicula. Nunc accumsan vel nibh ac tempus. Maecenas hendrerit magna dui, ac pulvinar purus bibendum nec. Curabitur pretium, dui id vehicula tincidunt, ipsum justo finibus mi, et lacinia quam enim eu lacus.");

            }

            DemoMode();

            // The refresh command
            Refresh = ReactiveCommand.Create(() =>
            {
                _btDiscovery.Refresh();
                Message = "";
                PostTitle = "";
                PostInput = "";
                //Operations.Clear();
            });
            // The Menu command
            Menu = ReactiveCommand.Create(() =>
            {
                Application.Current.MainPage = new Menu();
                Application.Current.Parent = new LandingPage();
            });
            // The post command
            Post = ReactiveCommand.Create(() =>
            {
                _btClient.SendData("pst:" + PostTitle + "^" + PostInput);
                PostTitle = "";
                PostInput = "";
            });
            // The request command
            Request = ReactiveCommand.Create(() =>
            {
                _btClient.SendData("rqst:uniq");
            });

            // Handle the selection of a device from the list
            this.WhenAnyValue(vm => vm.SelectedDevice)
                .Subscribe((device) =>
                {
                    if (device == null)
                    {
                        return;
                    }

                    if (!device.HasRequiredServiceID)
                    {
                        Message = "Device not supported or service not running";
                        return;
                    }

                    if (_btClient.Connect(device.Address))
                    {
                        // Fetch posts from the server
                        _btClient.SendData("rqst:uniq");
                    }
                    else
                    {
                        Message = "Unable to connect to device";
                        return;
                    }
                });

            // Execute an operation
            this.Execute = ReactiveCommand.Create<string>((operation) =>
            {
                if (posts.TryGetValue(operation, out string description))
                {
                    Message = description;
                    
                }
                else
                {
                    Message = "Fail";
                }
            });

            Refresh.Execute().Subscribe();
        }

        /// <summary>
        /// Handles data received from server
        /// </summary>
        /// <param name="sender">Client instance</param>
        /// <param name="data">The data</param>
        private void _btClient_ReceivedData(object sender, string data)
        {
            if (data.StartsWith("msg"))
            {
                var parts = data.Split(':');

                Message = parts[1];
            }
            else if (data.StartsWith("psts"))
            {
                var parts = data.Split(':');
                var postTotal = parts[1].Split('#');

                Operations.Clear();
                foreach (var pos in postTotal )
                {
                    var postSplt = pos.Split('^');
                    if(posts.ContainsKey(postSplt[0]))
                    {
                        posts[postSplt[0]] = postSplt[1];
                    }
                    else
                    {
                        posts.Add(postSplt[0], postSplt[1]);
                    }
                    Operations.Add(postSplt[0]);
                }
                Message = "Done";
                

            }
            else
            {
                Message = $"Unknown response {data}";
            }
        }
    }
}
