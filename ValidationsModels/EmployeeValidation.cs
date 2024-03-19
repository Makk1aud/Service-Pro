using Coursework.DataApp;
using Coursework.ValidationsModels.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.ValidationsModels
{
    public class EmployeeValidation : IDataErrorInfo
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string this[string columnName]
        {
            get 
            { 
                var error = string.Empty;
                switch (columnName)
                {
                    case "Firstname":
                        if (!ValidationExtensions.NameValidation(Firstname))
                            error = "Имя должно быть заполнено";
                        break;
                    case "Lastname":
                        if (!ValidationExtensions.NameValidation(Lastname))
                            error = "Имя должно быть заполнено";
                        break;
                    case "Phone":
                        if (!ValidationExtensions.PhoneValidation(Phone))
                            error = "Имя должно быть заполнено";
                        break;
                }
                return error;
            }
        }

        public string Error => throw new NotImplementedException();
    }
}
