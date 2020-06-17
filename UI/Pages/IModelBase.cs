using SoloContacts.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoloContacts.UI.Pages
{
    public interface IModelBase
    {
        public BrokenRulesManager BrokenRulesManager { get;}
    }
}
