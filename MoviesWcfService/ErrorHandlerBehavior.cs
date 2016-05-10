using System;
using System.ServiceModel.Configuration;

namespace MoviesWcfService
{
    internal class ErrorHandlerBehavior : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof (ErrorHandler); }
        }

        protected override object CreateBehavior()
        {
            return new ErrorHandler();
        }
    }
}