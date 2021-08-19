using ChessGameCore.Games;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace ChessGameView
{

    public class Scheduler : IHostedService, IDisposable
    {
        private int _timeLimitForPlayerToRejoin = 15;
        private int _updateTimeInSeconds = 1;
        private Timer _timer;

        private readonly GameManager _manager;

        public Scheduler(GameManager manager)
        {
            _manager = manager;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_updateTimeInSeconds));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _manager.TimerForTab(_timeLimitForPlayerToRejoin);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }

}