using System.Windows.Controls;
using CryptoApp.ApplicationCore.ViewModels;
using ScottPlot;
using System.Windows;
using System.Windows.Media;

namespace CryptoApp.Presentation.Pages
{
    public partial class CoinDetailPage : Page
    {
        public CoinDetailPage()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is CoinDetailViewModel vm)
            {
                vm.PropertyChanged += (s, ev) =>
                {
                    if (ev.PropertyName == nameof(vm.PriceSeries))
                        RenderPlot(vm);
                };
            }
        }

        private void RenderPlot(CoinDetailViewModel vm)
        {
            if (vm.PriceSeries == null || vm.PriceSeries.Length == 0)
                return;

            var xs = vm.PriceSeries.Select(p => p.TimeOADate).ToArray();
            var ys = vm.PriceSeries.Select(p => p.Price).ToArray();

            var plt = PricePlot.Plot;
            plt.Clear();

            plt.AddScatter(xs, ys,
                color: System.Drawing.Color.LimeGreen,
                lineWidth: 3,
                markerSize: 0);

            plt.AddFill(xs, ys,
                baseline: ys.Min(),
                color: System.Drawing.Color.FromArgb(60, 50, 205, 50));

            plt.Title($"{vm.Coin?.Name} price history", bold: true, size: 20, color: System.Drawing.Color.White);
            plt.XLabel("Time");
            plt.YLabel("Price (USD)");
            plt.XAxis.DateTimeFormat(true);

            plt.Style(
                figureBackground: System.Drawing.Color.FromArgb(18, 18, 18),
                dataBackground: System.Drawing.Color.FromArgb(30, 30, 30),
                tick: System.Drawing.Color.White,
                axisLabel: System.Drawing.Color.White
            );

            plt.Grid(color: System.Drawing.Color.FromArgb(80, 255, 255, 255), lineStyle: LineStyle.Dot);

            double min = ys.Min();
            double max = ys.Max();
            double margin = (max - min) * 0.1;
            plt.SetAxisLimits(yMin: min - margin, yMax: max + margin);
            plt.AxisAutoX();

            PricePlot.Configuration.LeftClickDragPan = false;
            PricePlot.Configuration.RightClickDragZoom = false;
            PricePlot.Configuration.ScrollWheelZoom = false;
            PricePlot.Configuration.MiddleClickDragZoom = false;

            PricePlot.Refresh();
        }
    }
}
