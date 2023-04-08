namespace Carlton.Base.Components.Test;

public static class NotificationTestHelper
{
    public const string BaseNotificationMarkup =
    @"
<div class=""carlton-notification"" b-rlbygplu4k>
    <div class=""content"" b-rlbygplu4k>
        <span class=""icon mdi mdi-24px icon-class"" b-rlbygplu4k></span>
        <div class=""message-container"" b-rlbygplu4k>
            <span class=""title"" b-rlbygplu4k>Notification Title</span>
            <span class=""message"" b-rlbygplu4k>Some Notification Message</span>
        </div>
        <span class=""dismiss mdi mdi-18px mdi-close"" blazor:onclick=""1"" b-rlbygplu4k></span>
    </div>
</div>";

    public const string NotificationBarMarkup =
    @"
<div class=""notification-bar"" b-5w4ezungf0>
    <div class=""content"" style=""top:50px;right:70px;"" b-5w4ezungf0></div>
</div>
<div class=""test-btns"">
    <button class=""success-btn"" blazor:onclick=""1"" b-5w4ezungf0>Success</button>
    <button class=""info-btn"" blazor:onclick=""2"" b-5w4ezungf0>Info</button>
    <button class=""warning-btn"" blazor:onclick=""3"" b-5w4ezungf0>Warning</button>
    <button class=""error-btn"" blazor:onclick=""4"" b-5w4ezungf0>Error</button>
</div>";

    public const string TestBtnsMarkup =
    @"<div class=""test-btns"" b-5w4ezungf0>
        <button class=""success-btn"" blazor:onclick=""1"" b-5w4ezungf0>Success</button>
        <button class=""info-btn"" blazor:onclick=""2"" b-5w4ezungf0>Info</button>
        <button class=""warning-btn"" blazor:onclick=""3"" b-5w4ezungf0>Warning</button>
        <button class=""error-btn"" blazor:onclick=""4"" b-5w4ezungf0>Error</button>
    </div>";

    public static IEnumerable<object[]> GetNotifications()
    {
        yield return new object[]
           {
               new List<int> { 2, 7, 3, 4 }.AsReadOnly()
           };
        yield return new object[]
           {
               new List<int> { 1, 2, 3, 4 }.AsReadOnly()
           };
        yield return new object[]
           {
              new List<int> { 0, 1, 3, 0 }.AsReadOnly()
           };
    }
}
