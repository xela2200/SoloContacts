using SoloContacts.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloContacts.Library.Models
{
        public class ModelBase
        {
            public ModelBase() { }

            protected BrokenRulesManager _BrokenRulesManager = new BrokenRulesManager();
            public BrokenRulesManager BrokenRulesManager
            {
                get { return _BrokenRulesManager; }
            }
        }
}
