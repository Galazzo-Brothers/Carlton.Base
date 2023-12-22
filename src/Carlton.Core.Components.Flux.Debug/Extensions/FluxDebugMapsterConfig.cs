using Carlton.Core.Components.Flux.Debug.State;
using Carlton.Core.Components.Flux.Debug.ViewModels;
using Carlton.Core.Utilities.Logging;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Carlton.Core.Components.Debug;


public class FluxDebugMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.RequireExplicitMapping = true;
        config.RequireDestinationMemberSource = true;

        config.NewConfig<FluxDebugState, FluxDebugState>()
            .ConstructUsing(_ => new FluxDebugState(_.State, _.LogMessages));

        config.NewConfig<FluxDebugState, LogViewerViewModel>()
              .Map(dest => dest.LogMessages, src => src.LogMessages);

        config.NewConfig<FluxDebugState, TraceLogViewerViewModel>()
            .Map(dest => dest.LogMessages, src => src.LogMessages);

        config.NewConfig<LogMessage, LogMessage>();

        config.NewConfig<LogLevel, LogLevel>();


        config.NewConfig<Exception, Exception>();

        config.Compile();
    }
}
