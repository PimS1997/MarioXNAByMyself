using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SuperMarioWorldInXNA
{
    class Enums
    {
        public string GetEnumDescription(Enum en)
        {
            //folowing code is copied (and slightly modified) from the answer at http://stackoverflow.com/questions/1799370/getting-attributes-of-enums-value
            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = ((DescriptionAttribute)attributes[0]).Description;
            return description;
        }
    }
}
