using System;

namespace Aero.Infrastructure
{
    public interface ITelemetry
    {
        void TrackPageView(string pageName);
        void TrackEvent(string eventName);
        void TrackMetric(string name, object value);
        void TrackTrace(string logEvent);
        void TrackException(Exception exception, string message=null);
        void TrackException(Exception exception, bool IsTerminating);
    }

    public interface ITelemetryService
    {
        void OnAppStart();
        void OnAppShutdown();
        void OnSessionChanged();
    }
}
