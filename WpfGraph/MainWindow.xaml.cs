using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (false)
            {
                var tbItem = new TabItem()
                {
                    Header = $"TabOne"
                };
                tbCtrl.Items.Add(tbItem);
                var grid = new System.Windows.Controls.Grid();
                tbItem.Content = grid;
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(3) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(3) });
                {
                    var splitter = new GridSplitter() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center, Height = 3, Background = Brushes.Blue };
                    System.Windows.Controls.Grid.SetRow(splitter, 2);
                    grid.Children.Add(splitter);
                }
                {
                    var splitter = new GridSplitter() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center, Height = 3, Background = Brushes.Blue };
                    System.Windows.Controls.Grid.SetRow(splitter, 4);
                    grid.Children.Add(splitter);
                }
                {
                    var gridTop = new System.Windows.Controls.Grid();
                    System.Windows.Controls.Grid.SetRow(gridTop, 1);
                    grid.Children.Add(gridTop);
                    gridTop.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(400) });
                    gridTop.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3) });
                    gridTop.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    var splitter = new GridSplitter() { VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Center, Width = 3, Background = Brushes.Blue };
                    System.Windows.Controls.Grid.SetColumn(splitter, 1);
                    gridTop.Children.Add(splitter);
                    var g1 = MakeGraph(1);
                    gridTop.Children.Add(g1);
                    var g2 = MakeGraph(2);
                    System.Windows.Controls.Grid.SetColumn(g2, 2);
                    gridTop.Children.Add(g2);
                }
                {
                    var gridSecond = new System.Windows.Controls.Grid();
                    System.Windows.Controls.Grid.SetRow(gridSecond, 3);
                    grid.Children.Add(gridSecond);
                    gridSecond.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(400) });
                    gridSecond.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3) });
                    gridSecond.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    var splitter = new GridSplitter() { VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Center, Width = 3, Background = Brushes.Blue };
                    System.Windows.Controls.Grid.SetColumn(splitter, 1);
                    gridSecond.Children.Add(splitter);
                    var g1 = MakeGraph(1);
                    gridSecond.Children.Add(g1);
                    var dp = new DockPanel();
                    System.Windows.Controls.Grid.SetColumn(dp, 2);
                    var g2 = MakeGraph(2);
                    dp.Children.Add(g2);
                    gridSecond.Children.Add(dp);
                }
            }
            {
                var tbItem = new TabItem() { Header = "TabTwo" };
                tbCtrl.Items.Add(tbItem);
                var grid = new System.Windows.Controls.Grid();
                tbItem.Content = grid;
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(3) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(3) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(3) });
                {
                    var splitter = new GridSplitter() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center, Height = 3, Background = Brushes.Blue };
                    System.Windows.Controls.Grid.SetRow(splitter, 2);
                    grid.Children.Add(splitter);
                }
                {
                    var splitter = new GridSplitter() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center, Height = 3, Background = Brushes.Blue };
                    System.Windows.Controls.Grid.SetRow(splitter, 4);
                    grid.Children.Add(splitter);
                }
                {
                    var splitter = new GridSplitter() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center, Height = 3, Background = Brushes.Blue };
                    System.Windows.Controls.Grid.SetRow(splitter, 6);
                    grid.Children.Add(splitter);
                }
                grid.Children.Add(new TextBlock() { Text = "testing" });
                var dp = new DockPanel();
                System.Windows.Controls.Grid.SetRow(dp, 3);
                grid.Children.Add(dp);

                var lstGraphs = new ObservableCollection<FrameworkElement>();
                var lb = new ListBox
                {
                    ItemsSource = lstGraphs,
                    ItemsPanel = (ItemsPanelTemplate)XamlReader.Parse($@"
<ItemsPanelTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
    <WrapPanel Orientation=""Horizontal"" HorizontalAlignment=""Stretch""/>
</ItemsPanelTemplate>")
                };
                for (int i = 0; i < 20; i++)
                {
                    await Task.Delay(100);
                    if (i % 4 == 0)
                    {
                        lstGraphs.Add(new TextBlock() { Text = $"adsf {i}" });
                    }
                    else
                    {
                        lstGraphs.Add(MakeGraph(i));
                    }
                }
                tbCtrl.SelectionChanged += async (o, e) =>
                 {
                     if (tbCtrl.Items[tbCtrl.SelectedIndex] == tbItem)
                     {
                         await Task.Delay(100); //til ScrollViewer ready
                         var sviewer = lb.GetChild<ScrollViewer>(); //sviewer is not available til loaded
                         if (sviewer != null)
                         {
                             sviewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                         }
                     }
                 };
                lstGraphs.CollectionChanged += async (o, e) =>
                {
                    if (e.NewItems != null && e.NewItems.Count > 0)
                    {
                        foreach (var itm in e.NewItems)
                        {
                            if (itm is WindowsFormsHostForScrollViewer wex)
                            {
                                await Task.Delay(10); //delay til new items processed by scrollviewer, then force a poschanged
                                wex.RedoPosChanged();
                                break; // only need this once if any found
                            }
                        }
                    }
                };
                dp.Children.Add(lb);
            }
            tbCtrl.SelectedIndex = 0;
        }

        static FrameworkElement MakeGraph(int num)
        {
            var wfh = new WindowsFormsHostForScrollViewer();
            var chart = new Chart() { Name = "MyChart" };
            wfh.Child = chart;
            var chartArea = new ChartArea("MyChartAreaName");
            chart.ChartAreas.Add(chartArea);
            var legend = new Legend();
            chart.Legends.Add(legend);
            legend.DockedToChartArea = chartArea.Name;
            legend.IsDockedInsideChartArea = true;
            legend.Docking = Docking.Left;
            var dataSeries = new Series("Data") { ChartType = SeriesChartType.Line };
            chart.Series.Add(dataSeries);
            for (int i = 0; i < 100; i++)
            {
                var dp = new DataPoint(i, i * (1 + num));
                dataSeries.Points.Add(dp);
            }
            chart.Titles.Add($"Graph {num}");
            return wfh;
        }
    }
    public static class MyStatics
    {
        public static T? GetAncestor<T>(this DependencyObject element) where T : DependencyObject
        {
            while (element != null && element is not T)
            {
                element = VisualTreeHelper.GetParent(element);
            }
            return (T?)element;
        }

        public static T? GetChild<T>(this DependencyObject element) where T : DependencyObject
        {
            T? res = null;
            if (element != null)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
                {
                    var child = VisualTreeHelper.GetChild(element, i);
                    res = (child as T) ?? GetChild<T>(child);//recur
                    if (res != null)
                    {
                        break;
                    }
                }
            }
            return res;
        }
    }
    /// <summary>
    /// Works without ScrollViewer too
    ///https://stackoverflow.com/questions/14080580/scrollviewer-is-not-working-in-wpf-windowsformhost/46858873#46858873
    /// </summary>
    public class WindowsFormsHostForScrollViewer : WindowsFormsHost
    {
        [DllImport("User32.dll", SetLastError = true)]
        static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        public event EventHandler? LocationChanged;
        public ScrollViewer? ParentScrollViewer { get; set; }
        private bool Scrolling;
        public bool Resizing;
        private Rect lastBoundingBox;
        public WindowsFormsHostForScrollViewer()
        {
            PresentationSource.AddSourceChangedHandler(this, SourceChangedEventHandler);
        }
        public void RedoPosChanged()
        {
            OnWindowPositionChanged(lastBoundingBox);
        }

        protected override void OnWindowPositionChanged(Rect rcBoundingBox)
        {
            base.OnWindowPositionChanged(rcBoundingBox);
            lastBoundingBox = rcBoundingBox;
            if (ParentScrollViewer != null)
            {
                DpiScale dpiScale = VisualTreeHelper.GetDpi(this);
                Rect newRect = ScaleRectDownFromDPI(rcBoundingBox, dpiScale);
                Rect finalRect;
                ParentScrollViewer.ScrollChanged += ParentScrollViewer_ScrollChanged;
                ParentScrollViewer.SizeChanged += ParentScrollViewer_SizeChanged;
                ParentScrollViewer.Loaded += ParentScrollViewer_Loaded;

                if (Scrolling || Resizing)
                {
                    if (PresentationSource.FromVisual(this)?.RootVisual?.TransformToDescendant(ParentScrollViewer) is MatrixTransform tr)
                    {
                        var scrollRect = new Rect(new Size(ParentScrollViewer.ViewportWidth, ParentScrollViewer.ViewportHeight));
                        var c = tr.TransformBounds(newRect);

                        var intersect = Rect.Intersect(scrollRect, c);
                        if (!intersect.IsEmpty)
                        {
                            tr = (MatrixTransform)ParentScrollViewer.TransformToDescendant(this);
                            if (tr != null)
                            {
                                intersect = tr.TransformBounds(intersect);
                                finalRect = ScaleRectUpToDPI(intersect, dpiScale);
                            }
                        }
                        else
                            finalRect = intersect = new Rect();

                        int x1 = (int)Math.Round(finalRect.X);
                        int y1 = (int)Math.Round(finalRect.Y);
                        int x2 = (int)Math.Round(finalRect.Right);
                        int y2 = (int)Math.Round(finalRect.Bottom);

                        SetRegion(x1, y1, x2, y2);
                        this.Scrolling = false;
                        this.Resizing = false;
                    }
                }
            }
            LocationChanged?.Invoke(this, new EventArgs());
        }
        private void ParentScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            this.Resizing = true;
        }

        private void ParentScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Resizing = true;
        }

        private void ParentScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0 || e.HorizontalChange != 0 || e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
                Scrolling = true;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                PresentationSource.RemoveSourceChangedHandler(this, SourceChangedEventHandler);
            }
        }

        private void SourceChangedEventHandler(Object sender, SourceChangedEventArgs e)
        {
            if (ParentScrollViewer != null)
            {
                ParentScrollViewer.ScrollChanged -= ParentScrollViewer_ScrollChanged;
                ParentScrollViewer.SizeChanged -= ParentScrollViewer_SizeChanged;
                ParentScrollViewer.Loaded -= ParentScrollViewer_Loaded;
            }
            ParentScrollViewer = this.GetAncestor<ScrollViewer>();
        }

        private void SetRegion(int x1, int y1, int x2, int y2)
        {
            SetWindowRgn(Handle, CreateRectRgn(x1, y1, x2, y2), true);
        }

        public static Rect ScaleRectDownFromDPI(Rect _sourceRect, DpiScale dpiScale)
        {
            double dpiX = dpiScale.DpiScaleX;
            double dpiY = dpiScale.DpiScaleY;
            return new Rect(new Point(_sourceRect.X / dpiX, _sourceRect.Y / dpiY), new System.Windows.Size(_sourceRect.Width / dpiX, _sourceRect.Height / dpiY));
        }

        public static Rect ScaleRectUpToDPI(Rect _toScaleUp, DpiScale dpiScale)
        {
            double dpiX = dpiScale.DpiScaleX;
            double dpiY = dpiScale.DpiScaleY;
            return new Rect(new Point(_toScaleUp.X * dpiX, _toScaleUp.Y * dpiY), new System.Windows.Size(_toScaleUp.Width * dpiX, _toScaleUp.Height * dpiY));
        }
    }
}
