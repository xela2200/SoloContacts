using SoloContacts.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoloContacts.UI.Pages
{
    public abstract class ModelBase
    {
        protected BrokenRulesManager _BrokenRulesManager = new BrokenRulesManager();
        public BrokenRulesManager BrokenRulesManager
        {
            get { return _BrokenRulesManager; }
        }
    }
}
