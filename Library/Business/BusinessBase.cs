using SoloContacts.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloContacts.Library.Business
{
        public class BusinessBase
        {
            public BusinessBase() { }

            protected BrokenRulesManager _BrokenRulesManager = new BrokenRulesManager();
            public BrokenRulesManager BrokenRulesManager
            {
                get { return _BrokenRulesManager; }
            }
        }
}
