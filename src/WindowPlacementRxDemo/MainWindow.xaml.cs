using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;
using WindowPlacementExample;

namespace WindowPlacementRxDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Observable.Merge(
                Observable.FromEventPattern<SizeChangedEventHandler, SizeChangedEventArgs>
                    (h => SizeChanged += h, h => SizeChanged -= h)
                    .Select(e => Unit.Default),
                Observable.FromEventPattern<EventHandler, EventArgs>
                                                        (h => LocationChanged += h, h => LocationChanged -= h)
                                                        .Select(e => Unit.Default)
                ).Throttle(TimeSpan.FromSeconds(5), RxApp.DeferredScheduler)
                .Subscribe(_ =>
                {
                    info.Text = "Placement saved: " + DateTime.Now;
                    this.SavePlacement();
                });
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.RestorePlacement();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.SavePlacement();
            base.OnClosing(e);
        }
    }
}