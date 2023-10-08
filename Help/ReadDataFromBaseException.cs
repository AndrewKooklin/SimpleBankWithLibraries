using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank.Help
{
    public class ReadDataFromBaseException : Exception
    {
        private string NoDataMessage { get; set; }
        ErrorMessage errorMessage;

        public ReadDataFromBaseException(string msg)
        {
            NoDataMessage = msg;
        }

        public void ShowMessage()
        {
            errorMessage = new ErrorMessage();
            errorMessage.MessageShow(NoDataMessage);
        }
    }
}
