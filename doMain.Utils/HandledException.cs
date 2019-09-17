using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace pryUtil
{
    public class HandledException : Exception
    {
        #region Variables Internas

        private string _ErrorMessage;
        private int _ErrorTypeId;
        private string _ErrorMessageFull;
        private long _ErrorId;

        #endregion

        #region Propiedades

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        public int ErrorTypeId
        {
            get { return _ErrorTypeId; }
            set { _ErrorTypeId = value; }
        }

        public string ErrorMessageFull
        {
            get { return _ErrorMessageFull; }
            set { _ErrorMessageFull = value; }
        }

        public long ErrorId
        {
            get { return _ErrorId; }
            set { _ErrorId = value; }
        }


        #endregion

        #region Constructor

        public HandledException(string ErrorMessage)
        {
            this.ErrorTypeId = (Int32)enumGeneric.ServiceError;
            this.ErrorMessage = ErrorMessage;
            this.ErrorMessageFull = "";
        }

        public HandledException(int ErrorTypeId, string ErrorMessage)
        {
            this.ErrorTypeId = ErrorTypeId;
            this.ErrorMessage = ErrorMessage;
            this.ErrorMessageFull = "";
        }

        public HandledException(int ErrorTypeId, string ErrorMessage, string ErrorMessageFull)
        {
            this.ErrorTypeId = ErrorTypeId;
            this.ErrorMessage = ErrorMessage;
            this.ErrorMessageFull = ErrorMessageFull;
        }

        public HandledException(string ErrorMessage, string ErrorMessageFull)
        {
            this.ErrorTypeId = (Int32)enumGeneric.ServiceError;
            this.ErrorMessage = ErrorMessage;
            this.ErrorMessageFull = ErrorMessageFull;
        }

        public HandledException(HandledFaultException ex)
        {
            this.ErrorTypeId = ex.ErrorTypeId;
            this.ErrorMessage = ex.ErrorMessage;
            this.ErrorMessageFull = ex.ErrorMessageFull;
            this.ErrorId = ex.ErrorId;
        }

        public HandledException(Exception ex)
        {
            this.ErrorTypeId = (Int32)enumGeneric.Default;
            this.ErrorMessage = ex.Message;
            this.ErrorMessageFull = "";
            this.ErrorId = 0;
        }

        public HandledException(Exception ex, bool bolServiceProxyError)
        {
            this.ErrorTypeId = (bolServiceProxyError) ? (Int32)enumGeneric.ProxyError : (Int32)enumGeneric.Default;
            this.ErrorMessage = ex.Message;
            this.ErrorMessageFull = "";
            this.ErrorId = 0;
        }

        #endregion
    }

    
    public class HandledFaultException
    {
        #region Variables

        private string _ErrorMessage;
        private int _ErrorTypeId;
        private string _ErrorMessageFull;
        private long _ErrorId;

        #endregion

        #region Propiedades

        
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        
        public int ErrorTypeId
        {
            get { return _ErrorTypeId; }
            set { _ErrorTypeId = value; }
        }

        
        public string ErrorMessageFull
        {
            get { return _ErrorMessageFull; }
            set { _ErrorMessageFull = value; }
        }

        
        public long ErrorId
        {
            get { return _ErrorId; }
            set { _ErrorId = value; }
        }

        #endregion

        #region Constructor

        public HandledFaultException(HandledException ex)
        {
            this.ErrorMessage = ex.ErrorMessage;
            this.ErrorTypeId = ex.ErrorTypeId;
            this.ErrorMessageFull = ex.ErrorMessageFull;
            this.ErrorId = ex.ErrorId;
        }

        #endregion

        
    }
}


