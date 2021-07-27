using AZMA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AZMA.Application.Interfaces
{
    public interface IAlertAnalyzersFactory
    {
        IAlertStandardSchemaAnalyzer CreateFor(IAlertStandardSchemaDataContext alertContext);
    }
}
