using Hyms.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hyms.Security
{
    public interface ISecurityContext
    {
        User User { get; }

        bool IsAdministrator { get; }
    }
}
