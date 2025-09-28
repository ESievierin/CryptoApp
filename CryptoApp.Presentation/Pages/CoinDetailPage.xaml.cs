using CryptoApp.ApplicationCore.ViewModels;
using ScottPlot;
using System.Windows;
using System.Windows.Controls;
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

        private static System.Drawing.Color GetColor(string brushKey)
        {
            if (Application.Current.Resources[brushKey] is SolidColorBrush brush)
            {
                var c = brush.Color;
                return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
            }

            return System.Drawing.Color.White;
        }

        private void RenderPlot(CoinDetailViewModel vm)
        {
            if (vm.PriceSeries == null || vm.PriceSeries.Length == 0)
                return;

            var xs = vm.PriceSeries.Select(p => p.TimeOADate).ToArray();
            var ys = vm.PriceSeries.Select(p => p.Price).ToArray();

            var plt = PricePlot.Plot;
            plt.Clear();

            var lineColor = GetColor("ChartLineBrush");
            var baseFill = GetColor("ChartFillBrush");
            var fillColor = System.Drawing.Color.FromArgb(60, baseFill.R, baseFill.G, baseFill.B);

            var figureBg = GetColor("PrimaryBackgroundBrush");
            var dataBg = GetColor("CardBackgroundBrush");
            var textColor = GetColor("PrimaryTextBrush");
            var gridColor = GetColor("BorderBrush");

            plt.AddScatter(xs, ys,
                color: lineColor,
                lineWidth: 3,
                markerSize: 0);

            plt.AddFill(xs, ys,
                baseline: ys.Min(),
                color: fillColor);

            plt.Title($"{vm.Coin?.Name} price history", bold: true, size: 20);
            plt.XLabel("Time");
            plt.YLabel($"Price ({vm.SelectedCurrency.Code.ToUpper()})");
            plt.XAxis.DateTimeFormat(true);

            plt.Style(
                figureBackground: figureBg,
                dataBackground: dataBg,
                tick: textColor,
                axisLabel: textColor,
                titleLabel: textColor
            );

            plt.Grid(color: gridColor, lineStyle: LineStyle.Dot);

            double min = ys.Min();
            double max = ys.Max();

            const double eps = 1e-9;
            if (Math.Abs(max - min) < eps)
            {
                double offset = Math.Abs(min) < eps ? 1 : Math.Abs(min) * 0.1;
                min -= offset;
                max += offset;
            }

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
