using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SoloContacts.Core.Validation
{
    public class BrokenRulesManager
    {
        private int _ErrorCount = 0;
        private int _WarningCount = 0;
        private int _InfoCount = 0;
        private int _SuccessCount = 0;
        private List<BrokenRule> _BrokenRules;

        public BrokenRulesManager()
        {
            _BrokenRules = new List<BrokenRule>();
        }

        public void Clear()
        {
            _ErrorCount = 0;
            _WarningCount = 0;
            _InfoCount = 0;
            _SuccessCount = 0;
            _BrokenRules.Clear();
        }

        public void Clear(RuleSeverity ruleSeverity)
        {
            switch (ruleSeverity)
            {
                case RuleSeverity.Error:
                    _BrokenRules.RemoveAll(rule => rule.Severity == RuleSeverity.Error);
                    _ErrorCount = 0;
                    break;
                case RuleSeverity.Warning:
                    _BrokenRules.RemoveAll(rule => rule.Severity == RuleSeverity.Warning);
                    _WarningCount = 0;
                    break;
                case RuleSeverity.Information:
                    _BrokenRules.RemoveAll(rule => rule.Severity == RuleSeverity.Information);
                    _InfoCount = 0;
                    break;
                case RuleSeverity.Success:
                    _BrokenRules.RemoveAll(rule => rule.Severity == RuleSeverity.Success);
                    _SuccessCount = 0;
                    break;
                default:
                    break;
            }
        }

        public void AddBrokenRule(RuleSeverity severity, string description)
        {
            AddBrokenRule(severity, description, "No technical Information available.", string.Empty);
        }

        public void AddBrokenRule(RuleSeverity severity, string description, string key)
        {
            AddBrokenRule(severity, description, "No technical Information available.", key);
        }

        public void AddBrokenRule(RuleSeverity severity, string description, string technical, string key)
        {
            BrokenRule _BrokenRule = new BrokenRule(severity, description, technical, key);
            _BrokenRules.Add(_BrokenRule);

            switch (severity)
            {
                case RuleSeverity.Error:
                    _ErrorCount = _ErrorCount + 1;
                    break;
                case RuleSeverity.Warning:
                    _WarningCount = _WarningCount + 1;
                    break;
                case RuleSeverity.Information:
                    _InfoCount = _InfoCount + 1;
                    break;
                case RuleSeverity.Success:
                    _SuccessCount = _SuccessCount + 1;
                    break;
                default:
                    break;
            }
        }

        public void AddBrokenRule(BrokenRule brokenRule)
        {
            _BrokenRules.Add(brokenRule);

            switch (brokenRule.Severity)
            {
                case RuleSeverity.Error:
                    _ErrorCount = _ErrorCount + 1;
                    break;
                case RuleSeverity.Warning:
                    _WarningCount = _WarningCount + 1;
                    break;
                case RuleSeverity.Information:
                    _InfoCount = _InfoCount + 1;
                    break;
                case RuleSeverity.Success:
                    _SuccessCount = _SuccessCount + 1;
                    break;
                default:
                    break;
            }
        }

        public void AddBrokenRuleRange(List<BrokenRule> brokenRules)
        {
            foreach (BrokenRule _BrokenRule in brokenRules)
            {
                AddBrokenRule(_BrokenRule);
            }
        }

        public void RemoveBrokenRule(BrokenRule brokenRule)
        {
            _BrokenRules.Remove(brokenRule);

            switch (brokenRule.Severity)
            {
                case RuleSeverity.Error:
                    _ErrorCount = _ErrorCount - 1;
                    break;
                case RuleSeverity.Warning:
                    _WarningCount = _WarningCount - 1;
                    break;
                case RuleSeverity.Information:
                    _InfoCount = _InfoCount - 1;
                    break;
                case RuleSeverity.Success:
                    _SuccessCount = _SuccessCount - 1;
                    break;
                default:
                    break;
            }
        }

        public string ToSuccessString()
        {
            if (_SuccessCount == 0)
            {
                return string.Empty;
            }

            StringBuilder _Results = new StringBuilder();
            int _Counter = 1;

            var _Succcess = from _BrokenRule in _BrokenRules
                               where _BrokenRule.Severity == RuleSeverity.Success
                               select _BrokenRule.Description + "\r\n";


            foreach (string _Rule in _Succcess)
            {
                _Results.Append(_Counter.ToString() + ". " + _Rule);
                _Counter++;
            }

            return _Results.ToString();
        }

        public string ToInformationString()
        {
            if (_InfoCount == 0)
            {
                return string.Empty;
            }

            StringBuilder _Results = new StringBuilder();
            int _Counter = 1;

            var _Information = from _BrokenRule in _BrokenRules
                               where _BrokenRule.Severity == RuleSeverity.Information
                               select _BrokenRule.Description + "\r\n";


            foreach (string _Rule in _Information)
            {
                _Results.Append(_Counter.ToString() + ". " + _Rule);
                _Counter++;
            }

            return _Results.ToString();
        }

        public string ToWarningString()
        {
            if (_WarningCount == 0)
            {
                return string.Empty;
            }

            StringBuilder _Results = new StringBuilder();
            int _Counter = 1;

            var _Information = from _BrokenRule in _BrokenRules
                               where _BrokenRule.Severity == RuleSeverity.Warning
                               select _BrokenRule.Description + "\r\n";


            foreach (string _Rule in _Information)
            {
                _Results.Append(_Counter.ToString() + ". " + _Rule);
                _Counter++;
            }

            return _Results.ToString();
        }

        public string ToErrorsString()
        {
            if (_ErrorCount == 0)
            {
                return string.Empty;
            }

            StringBuilder _Results = new StringBuilder();
            int _Counter = 1;

            var _Errors = from _BrokenRule in _BrokenRules
                          where _BrokenRule.Severity == RuleSeverity.Error
                          select _BrokenRule.Description + "\r\n";


            foreach (string _Rule in _Errors)
            {
                _Results.Append(_Counter.ToString() + ". " + _Rule);
                _Counter++;
            }

            return _Results.ToString();
        }

        public string ToTechnicalErrorsString()
        {
            if (_ErrorCount == 0)
            {
                return string.Empty;
            }

            StringBuilder _Results = new StringBuilder();
            int _Counter = 1;

            var _Errors = from _BrokenRule in _BrokenRules
                          where _BrokenRule.Severity == RuleSeverity.Error
                          select _BrokenRule.Description + "\r\n";


            foreach (string _Rule in _Errors)
            {
                _Results.Append(_Counter.ToString() + ". " + _Rule);
                _Counter++;
            }

            return _Results.ToString();
        }

        public string ToTechnicalWarningsString()
        {
            if (_WarningCount == 0)
            {
                return string.Empty;
            }

            StringBuilder _Results = new StringBuilder();
            int _Counter = 1;

            var _Warnings = from _BrokenRule in _BrokenRules
                            where _BrokenRule.Severity == RuleSeverity.Warning
                            select _BrokenRule.Description + "\r\n";


            foreach (string _Rule in _Warnings)
            {
                _Results.Append(_Counter.ToString() + ". " + _Rule);
                _Counter++;
            }

            return _Results.ToString();
        }

        public string ToTechnicalInformationString()
        {
            if (_InfoCount == 0)
            {
                return string.Empty;
            }

            StringBuilder _Results = new StringBuilder();
            int _Counter = 1;

            var _Information = from _BrokenRule in _BrokenRules
                               where _BrokenRule.Severity == RuleSeverity.Information
                               select _BrokenRule.Technical + "\r\n";


            foreach (string _Rule in _Information)
            {
                _Results.Append(_Counter.ToString() + ". " + _Rule);
                _Counter++;
            }

            return _Results.ToString();
        }

        public bool HasErrors
        {
            get { return _ErrorCount > 0; }
        }

        public bool HasWarnings
        {
            get { return _WarningCount > 0; }
        }

        public bool HasInformations
        {
            get { return _InfoCount > 0; }
        }

        public bool HasSuccess
        {
            get { return _SuccessCount > 0; }
        }

        /// <summary>
        /// Gets the number of broken rules in
        /// the collection that have a severity
        /// of Error.
        /// </summary>
        /// <value>An integer value.</value>
        public int ErrorCount
        {
            get { return _ErrorCount; }
        }

        /// <summary>
        /// Gets the number of broken rules in
        /// the collection that have a severity
        /// of Warning.
        /// </summary>
        /// <value>An integer value.</value>
        public int WarningCount
        {
            get { return _WarningCount; }
        }

        /// <summary>
        /// Gets the number of broken rules in
        /// the collection that have a severity
        /// of Information.
        /// </summary>
        /// <value>An integer value.</value>
        public int InformationCount
        {
            get { return _InfoCount; }
        }

        /// <summary>
        /// Gets the collection of broken rules.
        /// </summary>
        /// <value>An integer value.</value>
        public List<BrokenRule> BrokenRulesCollection
        {
            get { return _BrokenRules; }
        }
    }
}
