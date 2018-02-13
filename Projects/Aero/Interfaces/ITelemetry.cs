using System;

namespace Aero.Interfaces
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


    /// <summary>
    /// To be removed once we implement the first real telementry service
    /// </summary>
    public class NullTelemetry : ITelemetry
    {
        /// <summary>
        /// Only used for logging errors related to Telemetry itself
        /// </summary>
        private readonly IAeroLogger _logger;

        public NullTelemetry(IAeroLogger logger)
        {
            _logger = logger;
        }

        public void TrackEvent(string eventName)
        {
            //Logging just so we can see it
            _logger.Debug($"Track Event: {eventName}");
        }

        public void TrackException(Exception exception, bool IsTerminating)
        {
            
        }

        public void TrackException(Exception exception, string message = null)
        {
            
        }

        public void TrackMetric(string name, object value)
        {
            //Just logging so we can see something. 
            _logger.Debug($"Track Metric - Name:{name}, Value:{value}");
        }

        public void TrackPageView(string pageName)
        {
            _logger.Debug($"Track PageView - PageName:{pageName}");
        }

        public void TrackTrace(string logEvent)
        {
            
        }
    }

    /// <summary>
    /// To be removed once we implement the first real telemetry service
    /// </summary>
    public class NullTelemetryService : ITelemetryService
    {
        public void OnAppShutdown()
        {

        }

        public void OnAppStart()
        {

        }

        public void OnSessionChanged()
        {

        }
    }
}
