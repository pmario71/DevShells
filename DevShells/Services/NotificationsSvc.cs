using System;
using System.Windows;
using Serilog;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace DevShells.Services
{
    public interface INotifications
    {
        void ShowInformation(string message);
        void ShowWarning(string message);
        void ShowError(string message);
    }

    internal sealed class NotificationsSvc : INotifications, IDisposable
    {
        public NotificationsSvc(ILogger logger, MainWindow window)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Notifier notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: window,
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = window.Dispatcher;
            });
            _notifier = notifier;
        }
        private readonly Notifier _notifier;
        private readonly ILogger _logger;

        public void ShowSuccess(string message)
        {
            _logger.Information(message);
            _notifier.ShowSuccess(message);
        }

        public void ShowInformation(string message)
        {
            _logger.Information(message);
            _notifier.ShowInformation(message);
        }

        public void ShowWarning(string message)
        {
            _logger.Warning(message);
            _notifier.ShowWarning(message);
        }

        public void ShowError(string message)
        {
            _logger.Error(message);
            _notifier.ShowError(message);
        }

        public void Dispose()
        {
            _notifier?.Dispose();
        }
    }
}