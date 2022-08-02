using System.Windows.Forms.DataVisualization.Charting;

namespace cartesian_22
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            _chart = new Chart{ Dock = DockStyle.Fill, };
            _series = new Series
            {
                Name = "Series1",
                Color = Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Line,
                BorderWidth = 2, // In a line chart, this is the width of the line.
            };
            _series.Points.Add(new DataPoint { IsEmpty = true });

            _chartArea = new ChartArea
            {
                AxisX =
                {
                    Minimum = -10,
                    Maximum = 10,
                    Interval = 1,
                    IsMarksNextToAxis = false,
                    IsLabelAutoFit = false,
                    Crossing = 0,
                    LineWidth = 1,
                    MajorGrid = {  Interval = 1, LineColor = Color.LightBlue, LineWidth = 1 },
                    Enabled = AxisEnabled.True,
                },
                AxisY =
                {
                    Minimum = -10,
                    Maximum = 10,
                    Interval = 1,
                    IsMarksNextToAxis = false,
                    IsLabelAutoFit = false,
                    Crossing = 0,
                    LineWidth = 1,
                    MajorGrid = {  Interval = 1, LineColor = Color.LightBlue, LineWidth = 1 },
                    Enabled = AxisEnabled.True,
                },
            };

            _chart.ChartAreas.Add(_chartArea);
            _series.MarkerStyle = MarkerStyle.Cross;
            _chart.Series.Add(_series);

            _chartArea.CursorX.LineColor = Color.LightCoral;
            _chartArea.CursorX.Interval = 0.01;
            _chartArea.CursorX.IsUserEnabled = true;

            _chartArea.CursorY.LineColor = Color.LightCoral;
            _chartArea.CursorY.Interval = 0.01;
            _chartArea.CursorY.IsUserEnabled = true;

            panel.Controls.Add(_chart);
#if false
            // Draw diagonal
            for (int i = -10; i <= 10; i++)
            {
                _series.Points.AddXY(i, i);
            }
#endif
            _chart.MouseClick += onChartClick;
            _chart.MouseDoubleClick += onChartDoubleClick;
            _chart.MouseMove += onChartMouseMove;
        }
        private void onChartMouseMove(object? sender, MouseEventArgs e)
        {
            _chartArea.CursorX.SetCursorPixelPosition(e.Location, true);
            _chartArea.CursorY.SetCursorPixelPosition(e.Location, true);
        }

        private void onChartClick(object? sender, MouseEventArgs e)
        {
            _series.Points.AddXY(_chartArea.CursorX.Position, _chartArea.CursorY.Position);
        }

        private void onChartDoubleClick(object? sender, MouseEventArgs e)
        {
            _series.Points.Clear();
            _series.Points.Add(new DataPoint { IsEmpty = true });
        }


        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Task.Delay(100).GetAwaiter().OnCompleted(() =>
            {
                panel.Visible = true;
            });
        }
        private readonly Series _series;
        private readonly Chart _chart;
        private readonly ChartArea _chartArea;
    }
}