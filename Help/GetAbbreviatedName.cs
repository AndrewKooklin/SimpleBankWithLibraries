using SimpleBank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank.Help
{
    public static class GetAbbreviatedName
    {
        public static string GetFIO(this Person person, Person personArg)
        {
            string fio = "";

            string firstLetterFirstName = personArg.FirstName.ToUpper()
                                                                  .Substring(0, 1);
            string firstLetterFathersName = personArg.FathersName.ToUpper()
                                                              .Substring(0, 1);

            fio = personArg.LastName + " " + firstLetterFirstName + "." + firstLetterFathersName + ".";

            return fio;
        }
    }
}
