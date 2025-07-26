using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProductMonitorWinForms
{
    public partial class MainForm : Form
    {
        private readonly DataManager _dataManager = new();
        private readonly TrayIconManager _trayIcon;
        private readonly Timer _dataTimer = new();
        private readonly Timer _connectionTimer = new();
        private readonly Random _rnd = new();

        public MainForm()
        {
            InitializeComponent();

            _trayIcon = new TrayIconManager();
            _trayIcon.OpenRequested += () => Show();
            _trayIcon.ReconnectRequested += () => ToggleConnection(true);
            _trayIcon.ExitRequested += () => Close();

            _dataTimer.Interval = 2000; // simulate new queries
            _dataTimer.Tick += (s, e) => SimulateQuery();
            _dataTimer.Start();

            _connectionTimer.Interval = 5000; // simulate status change
            _connectionTimer.Tick += (s, e) => ToggleConnection(!_trayIcon.IsConnected);
            _connectionTimer.Start();
        }

        private void ToggleConnection(bool status)
        {
            _trayIcon.IsConnected = status;
        }

        private void SimulateQuery()
        {
            var products = new[] { "Arroz", "Feijao", "Macarrao", "Leite" };
            var product = products[_rnd.Next(products.Length)];
            _dataManager.AddQuery(product);
            UpdateGrid();
        }

        private void filterBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            var period = filterBox.SelectedIndex switch
            {
                0 => FilterPeriod.Today,
                1 => FilterPeriod.ThisMonth,
                2 => FilterPeriod.ThisYear,
                _ => FilterPeriod.Today
            };

            var data = _dataManager.GetCounts(period);
            gridView.Rows.Clear();
            foreach (var kvp in data)
            {
                gridView.Rows.Add(kvp.Key, kvp.Value);
            }
        }
    }
}
