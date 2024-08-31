using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils.GenerateCode
{
    public class GenerateCode : IGenerateCode
    {
        public string GenerateOrderCode()
        {
            return $"ORDER-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }
    }
}
