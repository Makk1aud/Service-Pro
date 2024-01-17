using Coursework.ValidationsModels.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Coursework.ValidationsModels
{
    public class ClientValidation : IDataErrorInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNum { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error=String.Empty;
                switch(columnName)
                {
                    case "FirstName":
                        if (!ValidationExtensions.NameValidation(FirstName))
                            error = "Имя должно быть введено";
                        break;
                    case "LastName":
                        if (!ValidationExtensions.NameValidation(LastName))
                            error = "Имя должно быть введено";
                        break;
                    case "PhoneNum":
                        if (!ValidationExtensions.PhoneValidation(PhoneNum))
                            error = "Формат номера не правильный, проверьте пробелы";
                        break;
                }
                return error;
            }
        }
        public string Error => throw new NotImplementedException();
    }
}
