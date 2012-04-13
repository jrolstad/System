using System;

namespace Rolstad.System.Dates
{
    /// <summary>
    /// A wrapper around the static operations on <see cref="DateTime"/> which allows time
    /// to be frozen.
    /// </summary>
    [Obsolete("Use Directus.Core instead")]
    public static class Clock
    {
        private static bool Frozen { get; set; }
        private static DateTime FreezeTime { get; set; }

        /// <summary>
        /// The current time
        /// </summary>
        public static DateTime Now
        {
            get
            {
                var now = (Frozen) ? FreezeTime : DateTime.Now;
                return  now;
            }
        }

        /// <summary>
        /// The current date
        /// </summary>
        public static DateTime Today
        {
            get { return Now.Date; }
        }

        /// <summary>
        /// Let the clock start flowing again
        /// </summary>
        public static void Thaw()
        {
            Frozen = false;
        }
        /// <summary>
        /// Freeze the clock to the current date / time
        /// </summary>
        /// <returns></returns>
        public static IDisposable Freeze()
        {
            return Freeze(DateTime.Now);
        }
        /// <summary>
        /// Freeze the clock to a given point in time
        /// </summary>
        /// <param name="dateTimeToFreezeTo">When to freeze the clock to</param>
        /// <returns></returns>
        public static IDisposable Freeze(DateTime dateTimeToFreezeTo)
        {
            if (Frozen)
                throw new InvalidOperationException("Clock already frozen.");
            FreezeTime = dateTimeToFreezeTo;
            Frozen = true;

            return new ClockThawer();
        }

        private class ClockThawer : IDisposable
        {
            public void Dispose()
            {
                Thaw();
            }
        }
    }
}