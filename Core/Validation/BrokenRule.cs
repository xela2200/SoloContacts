using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SoloContacts.Core.Validation
{
    /// <summary>
    /// Stores details about a specific broken business rule.
    /// </summary>
    public class BrokenRule
    {
        public BrokenRule() {}

        public BrokenRule(RuleSeverity severity, string description, string technical, string key)
        {
            Severity = severity;
            Description = description;
            Technical = technical;
            Key = key;
        }

        /// <summary>
        /// Gets the severity of the broken rule.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public RuleSeverity Severity { get; private set; }

        /// <summary>
        /// Provides access to the description of the broken rule.
        /// </summary>
        /// <value>The description of the rule.</value>
        public string Description { get; private set; }

        /// <summary>
        /// Provides access to technical information.
        /// </summary>
        /// <value>The inner exception, stack trace or information to determine bug.</value>
        public string Technical { get; private set; }

        /// <summary>
        /// Provides unique identifying value for rule to technical information.
        /// </summary>
        /// <value>The inner exception, stack trace or information to determine bug.</value>
        public string Key { get; private set; }
    }
}
