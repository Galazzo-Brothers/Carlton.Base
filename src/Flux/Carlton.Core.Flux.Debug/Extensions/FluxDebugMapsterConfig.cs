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

        config.NewConfig<FluxDebugState, LogViewerViewModel>()
              .Map(dest => dest.LogMessages, src => src.LogMessages);

        config.NewConfig<FluxDebugState, TraceLogViewerViewModel>()
            .Map(dest => dest.LogMessages, src => src.LogMessages);

        //config.NewConfig<FluxDebugState, ModalViewModel>()
        //    .Ignore(dest => dest.IsVisible)
        //    .ConstructUsing(_ => new ModalViewModel(false));

        config.NewConfig<LogMessage, LogMessage>();

        config.NewConfig<LogLevel, LogLevel>();


        config.NewConfig<Exception, Exception>();

        config.Compile();
    }
}
