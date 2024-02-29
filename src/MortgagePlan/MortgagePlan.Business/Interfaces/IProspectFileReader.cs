using MortgagePlan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgagePlan.Business.Interfaces
{
    public interface IProspectFileReader
    {
        IEnumerable<Prospect> ReadFile();
        void WriteFile(Prospect prospects);
    }
}
