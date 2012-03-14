using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCForge.API
{
    public abstract class Cancellable
    {
        private bool _cancelled;

        /// <summary>
        /// Weather or not the event is canceled
        /// </summary>
        public sealed bool IsCancelled { get { return _cancelled; } }

        /// <summary>
        /// Cancel the event
        /// </summary>
        /// <param name="value">If true, the event will be canceled, if false, the event will be un-canceled</param>
        public sealed void SetCancelled(bool value)
        {
            _cancelled = value;
        }
    }
}
