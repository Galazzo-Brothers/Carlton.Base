using Mapster;

namespace Carlton.Core.Components.Debug;

public static class FluxDebugMapsterConfig
{
    public static TypeAdapterConfig BuildMapsterConfig()
    {
    //    TypeAdapterConfig.GlobalSettings.RequireExplicitMapping = true;
      //  TypeAdapterConfig.GlobalSettings.RequireDestinationMemberSource = true;

        var config = new TypeAdapterConfig
        {
            //RequireExplicitMapping = true,
            //RequireDestinationMemberSource = true
        };

      
        config.Compile();

        return config;
    }

}

