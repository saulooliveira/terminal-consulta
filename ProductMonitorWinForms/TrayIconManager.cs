using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProductMonitorWinForms
{
    public class TrayIconManager : IDisposable
    {
        private readonly NotifyIcon _icon;
        private bool _isConnected;

        public event Action? OpenRequested;
        public event Action? ReconnectRequested;
        public event Action? ExitRequested;

        public TrayIconManager()
        {
            _icon = new NotifyIcon
            {
                Visible = true
            };
            _icon.ContextMenuStrip = BuildMenu();
            UpdateIcon();
        }

        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    UpdateIcon();
                }
            }
        }

        private ContextMenuStrip BuildMenu()
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add("Abrir painel", null, (s, e) => OpenRequested?.Invoke());
            menu.Items.Add("Forçar reconexão", null, (s, e) => ReconnectRequested?.Invoke());
            menu.Items.Add("Sair do aplicativo", null, (s, e) => ExitRequested?.Invoke());
            return menu;
        }

        private void UpdateIcon()
        {
            _icon.Icon = _isConnected ? SystemIcons.Application : SystemIcons.Error;
            _icon.Text = _isConnected ? "Conectado" : "Desconectado";
        }

        public void Dispose()
        {
            _icon.Dispose();
        }
    }
}
