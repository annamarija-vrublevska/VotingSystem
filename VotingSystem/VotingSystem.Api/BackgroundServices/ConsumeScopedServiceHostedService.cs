namespace VotingSystem.Api.BackgroundServices
{
    public class ConsumeScopedServiceHostedService : BackgroundService
    {

        public ConsumeScopedServiceHostedService(IServiceProvider services)
        {
            Services = services;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using (var scope = Services.CreateScope())
            {
                var itemStatusUpdateService =
                    scope.ServiceProvider
                        .GetRequiredService<IItemStatusUpdateService>();

                await itemStatusUpdateService.DoWork(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await base.StopAsync(stoppingToken);
        }
    }
}
