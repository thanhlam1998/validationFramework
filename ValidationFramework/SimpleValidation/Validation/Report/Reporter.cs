using System;
using System.Collections.Generic;

namespace SimpleValidation.Validation
{
    public class Reporter
    {
        public List<string> MessageList { get; set; }
        private OnFailListener failListener;
        private OnSuccessListener successListener;

        public Reporter()
        {
            MessageList = new List<string>();
        }

        public void AddWarningMessage(string message)
        {
            if (null != message && !message.Equals(""))
                MessageList.Add(message);
        }

        public void Report()
        {
            if (0 == MessageList.Count)
            {
                successListener.Do();
            }
            else
            {
                failListener.Do(MessageList);
            }
        }

        public bool GetResult()
        {
            if (0 == MessageList.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Reporter AddOnSuccessListener(OnSuccessListener successListener)
        {
            this.successListener = successListener;
            return this;
        }

        public Reporter AddOnFailListener(OnFailListener failListener)
        {
            this.failListener = failListener;
            return this;
        }

    }

    public interface OnFailListener
    {
        void Do(List<string> messages);
    }

    public interface OnSuccessListener
    {
        void Do();
    }
}
