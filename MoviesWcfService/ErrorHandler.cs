using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace MoviesWcfService
{
    public class ErrorHandler : Attribute, IErrorHandler, IServiceBehavior
    {
        #region IErrorHandler Members

        public bool HandleError(Exception error)
        {
            //TODO: Add error logging
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException)
                return;

            // Shield the unknown exception
            var faultException = new FaultException(error.Message + "\n  in " + GetType());
            var messageFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(version, messageFault, faultException.Action);
            // Note: the Stack Trace shown in WCFStorm or WCF Test Client is it's own local stack trace, not ours.
        }

        #endregion

        #region IServiceBehavior Members

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            IErrorHandler errorHandler = new ErrorHandler();

            // Adds our ErrorHandler to each ChannelDispatcher
            foreach (var channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                Debug.Assert(channelDispatcher != null, "channelDispatcher != null");
                channelDispatcher.ErrorHandlers.Add(errorHandler);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var endpoint in serviceDescription.Endpoints)
            {
                if (endpoint.Contract.Name.Equals("IMetadataExchange") &&
                    endpoint.Contract.Namespace.Equals("http://schemas.microsoft.com/2006/04/mex"))
                    continue;

                if (endpoint.Contract.Operations.Any(description => description.Faults.Count == 0))
                {
                    throw new InvalidOperationException("FaultContractAttribute not found on this method");
                }
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        #endregion
    }
}