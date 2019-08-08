using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevShells.Configuration;
using Serilog;
using Squirrel;

namespace DevShells.Services
{
    public class Updater
    {
        private  readonly string UpdatePath;
        private readonly ILogger _logger;
        private readonly IConfigAccess _configAccess;
        private bool _updatePending;

        public Updater(ILogger logger, IConfigAccess configAccess)
        {
            _logger = logger;
            _configAccess = configAccess;

            UpdatePath = _configAccess.Get().UpdateDropLocation;
        }

        private int UpdateCheckFrequencyInMs => 60 * 1000;

        public async Task RunAsync(CancellationToken token)
        {
            if (!CanUpdate())
            {
                throw new Exception("Update functionality not available!");
            }

            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                await CheckAndApplyUpdates()
                    .ConfigureAwait(false);
                await Task.Delay(UpdateCheckFrequencyInMs, token)
                    .ConfigureAwait(false);
            }
        }

        public bool IsUpdatePending => _updatePending;

        private async Task CheckAndApplyUpdates()
        {
            using(var mgr = new UpdateManager(UpdatePath))
            {
                var updateInfo = await mgr.CheckForUpdate();
                if (updateInfo.CurrentlyInstalledVersion.Version < updateInfo.FutureReleaseEntry.Version)
                {
                    _logger.Information("Update pending: {0}", updateInfo.FutureReleaseEntry.Version);

                    try
                    {
                        await mgr.UpdateApp()
                            .ConfigureAwait(false);
                    }
                    catch(System.ComponentModel.Win32Exception)
                    {
                        _logger.Warning("Failed over to full install!");
                        await mgr.FullInstall()
                            .ConfigureAwait(false);
                    }

                    _updatePending = true;
                }
            }
        }

        private bool CanUpdate()
        {
            return !string.IsNullOrWhiteSpace(UpdatePath);
        }
    }
}
