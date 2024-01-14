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
            .ConstructUsing(_ => new FluxDebugState(_.State, _.LogMessages));

        config.NewConfig<FluxDebugState, EventLogViewerViewModel>()
              .Map(dest => dest.LogMessages, src => src.LogMessages);

        config.NewConfig<FluxDebugState, TraceLogViewerViewModel>()
            .Map(dest => dest.LogMessages, src => src.LogMessages);

        config.NewConfig<FluxDebugState, EventLogDetailsViewModel>()
            .Map(dest => dest.SelectedLogMessage, src => src.SelectedLogMessage);

        config.NewConfig<FluxDebugState, EventLogScopesViewModel>()
            .Map(dest => dest.SelectedLogMessage, src => src.SelectedLogMessage);

        config.NewConfig<LogMessage, LogMessage>();

        config.NewConfig<LogLevel, LogLevel>();
        config.NewConfig<EventId, EventId>();
        config.NewConfig<Exception, Exception>();
        config.NewConfig<KeyValuePair<string, object>, KeyValuePair<string, object>>();

        //config.NewConfig<LogEntry, LogEntry>();

        config.NewConfig<LogLevel, LogLevel>();

        config.NewConfig<ExceptionEntry, ExceptionEntry>();

    //    config.Compile();
    }
}
