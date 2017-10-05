using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MauticApiClient.Net.Model;

namespace MauticApiClient.Net
{
    [Serializable]
    public class MauticApiException : Exception {

        private MessageHandler _MessageList = new MessageHandler();

        public MauticApiException(string message, MessageHandler pErrorList) : base(message) {
            _MessageList = pErrorList;
        }

        public List<Message> GetMessages(){
            return _MessageList.Data;
        }

    }
}
