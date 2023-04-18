namespace Carlton.Base.TestBedFramework;
public record EventRecorded(object Evt) : IComponentEvent<EventConsoleViewModel>;