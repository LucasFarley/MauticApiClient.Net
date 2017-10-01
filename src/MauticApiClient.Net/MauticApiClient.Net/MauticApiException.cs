using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MauticApiClient.Net.Model;

namespace MauticApiClient.Net
{
    [Serializable]
    public class MauticApiException : Exception {

        private Errorhandler _ErrorList = new Errorhandler();

        public MauticApiException(string message, Errorhandler pErrorList) : base(message) { 
            _ErrorList = pErrorList;
        }

        public List<Error> GetErrors(){
            return _ErrorList.Data;
        }

    }
}
