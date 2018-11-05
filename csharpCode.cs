using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace tessssssssssss.Client
{
    public class MyService : MyService.SOAP.IMyService, IDisposable
    {
        protected MyService.SOAP.IMyService Channel { get; set; }

        public MyService(string endpointAddress)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.None;
            ChannelFactory<MyService.SOAP.IMyService> factory =
                new ChannelFactory<MyService.SOAP.IMyService>(
                    binding, new EndpointAddress(endpointAddress));
            Channel = factory.CreateChannel();
        }

        public void Process(Data.Order order)
        {
            Channel.Process(order);
        }

        public List<Data.Document> GetDocuments(Data.Order order)
        {
            return Channel.GetDocuments(order);
        }

        #region IDisposable
        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
            if (!disposing) return;

            ICommunicationObject co = (ICommunicationObject)Channel;
            try
            {
                co.Close();
            }
            catch
            {
                try
                {
                    co.Abort();
                }
                catch { }
            }
            Channel = null;
        }

        ~MyService()
        {
            Dispose(false);
        }

        #endregion
    }
}
