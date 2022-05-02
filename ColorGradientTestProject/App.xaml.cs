using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ColorGradientTestProject {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void Application_Startup(object sender, StartupEventArgs e) {
            ViewModel viewModel = new ViewModel();
            viewModel.Colors = new System.Collections.ObjectModel.ObservableCollection<Color>() {
                new Color() {
                    ColorName = "Apple green",
                    R = 141,
                    G = 182,
                    B = 0,
                },
                new Color() {
                    ColorName = "Banana yellow",
                    R = 255,
                    G = 255,
                    B = 53,
                },
                new Color() {
                    ColorName = "Flamingo pink",
                    R = 252,
                    G = 142,
                    B = 172,
                },
                new Color() {
                    ColorName = "Caribbean green",
                    R = 0,
                    G = 204,
                    B = 153,
                },
                new Color() {
                    ColorName = "Dark orange",
                    R = 255,
                    G = 140,
                    B = 0,
                },
                new Color() {
                    ColorName = "Lavender indigo",
                    R = 148,
                    G = 87,
                    B = 235,
                }
            };
            viewModel.testWindowBrush = new System.Windows.Media.LinearGradientBrush(System.Windows.Media.Colors.LightGray, System.Windows.Media.Colors.DarkGray, 0);

            var window = new MainWindow { DataContext = viewModel };
            window.Show();
        }
    }
}
