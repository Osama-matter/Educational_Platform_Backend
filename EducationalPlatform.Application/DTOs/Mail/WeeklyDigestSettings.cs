using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.DTOs.Mail
{
    /// <summary>
    /// Bind via appsettings.json under the key "WeeklyDigest".
    /// </summary>
    /// <example>
    /// "WeeklyDigest": {
    ///   "DayOfWeek": "Monday",
    ///   "Hour": 8,
    ///   "Minute": 0,
    ///   "TimeZoneId": "Europe/Cairo"
    /// }
    /// </example>
    public class WeeklyDigestSettings
    {
        /// <summary>
        /// The day on which the digest is sent every week.
        /// Defaults to Monday.
        /// </summary>
        public DayOfWeek DayOfWeek { get; set; } = DayOfWeek.Monday;

        /// <summary>
        /// Hour (0-23) in the configured time zone at which the digest runs.
        /// Defaults to 8 (08:00).
        /// </summary>
        public int Hour { get; set; } = 8;

        /// <summary>
        /// Minute (0-59) in the configured time zone at which the digest runs.
        /// Defaults to 0.
        /// </summary>
        public int Minute { get; set; } = 0;

        /// <summary>
        /// IANA time-zone identifier used to interpret Hour and Minute.
        /// Defaults to "Europe/Cairo".
        /// </summary>
        public string TimeZoneId { get; set; } = "Europe/Cairo";
    }
}
