using Carlton.Core.Flux.Debug.Models.Common;
using Mapster;
using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Debug.Extensions;


public class FluxDebugMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.RequireExplicitMapping = true;
        config.RequireDestinationMemberSource = true;

        config.NewConfig<FluxDebugState, FluxDebugState>()
            .ConstructUsing(_ => new FluxDebugState(_.State, _.LogEntries));

        config.NewConfig<FluxDebugState, EventLogViewerViewModel>()
              .Map(dest => dest.LogEntries, src => src.LogEntries);

        config.NewConfig<FluxDebugState, TraceLogViewerViewModel>()
            .Map(dest => dest.LogEntries, src => src.LogEntries);

        config.NewConfig<FluxDebugState, EventLogDetailsViewModel>()
            .Map(dest => dest.SelectedLogEntry, src => src.SelectedLogEntry);

        config.NewConfig<FluxDebugState, EventLogScopesViewModel>()
            .Map(dest => dest.SelectedLogEntry, src => src.SelectedLogEntry);

        config.NewConfig<LogEntry, LogEntry>();

        config.NewConfig<LogLevel, LogLevel>();

        config.NewConfig<ExceptionEntry, ExceptionEntry>();

        config.Compile();
    }
}
