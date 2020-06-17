using System;

namespace SoloContacts.Core.Data
{

    /// <summary>
    /// Provides an integer data type that understands the concept
    /// of an empty value.
    /// </summary>
    public class SmartInt : IComparable
    {
        private int _int;
        private bool _initialized;
        private bool _emptyIsMax;
        private string _format;

        #region Constructors
        /// <summary>
        /// Creates a new SmartInt object.
        /// </summary>
        /// <param name="emptyIsMin">Indicates whether an empty int is the min or max int value.</param>
        public SmartInt(bool emptyIsMin)
        {
            _emptyIsMax = !emptyIsMin;
            _format = null;
            _initialized = false;
            // provide a dummy value to allow real initialization
            _int = int.MinValue;
            if (!_emptyIsMax)
                Int = int.MinValue;
            else
                Int = int.MaxValue;
        }

        /// <summary>
        /// Creates a new SmartInt object.
        /// </summary>
        /// <remarks>
        /// The SmartInt created will use the min possible
        /// int to represent an empty int.
        /// </remarks>
        /// <param name="Value">The initial value of the object.</param>
        public SmartInt(int value)
        {
            _emptyIsMax = false;
            _format = null;
            _initialized = false;
            _int = int.MinValue;
            Int = value;
        }

        /// <summary>
        /// Creates a new SmartInt object.
        /// </summary>
        /// <param name="Value">The initial value of the object.</param>
        /// <param name="EmptyIsMin">Indicates whether an empty int is the min or max int value.</param>
        public SmartInt(int value, bool emptyIsMin)
        {
            _emptyIsMax = !emptyIsMin;
            _format = null;
            _initialized = false;
            _int = int.MinValue;
            Int = value;
        }

        /// <summary>
        /// Creates a new SmartInt object.
        /// </summary>
        /// <remarks>
        /// The SmartInt created will use the min possible
        /// int to represent an empty int.
        /// </remarks>
        /// <param name="Value">The initial value of the object (as text).</param>
        public SmartInt(string value)
        {
            _emptyIsMax = false;
            _format = null;
            _initialized = true;
            _int = int.MinValue;
            this.Text = value;
        }

        /// <summary>
        /// Creates a new SmartInt object.
        /// </summary>
        /// <param name="Value">The initial value of the object (as text).</param>
        /// <param name="EmptyIsMin">Indicates whether an empty int is the min or max int value.</param>
        public SmartInt(string value, bool emptyIsMin)
        {
            _emptyIsMax = !emptyIsMin;
            _format = null;
            _initialized = true;
            _int = int.MinValue;
            this.Text = value;
        }
        #endregion

        #region Text Support

        /// <summary>
        /// Gets or sets the format string used to format a int
        /// value when it is returned as text.
        /// </summary>
        /// <remarks>
        /// The format string should follow the requirements for the
        /// .NET <see cref="System.String.Format"/> statement.
        /// </remarks>
        /// <value>A format string.</value>
        public string FormatString
        {
            get
            {
                if (_format == null)
                    _format = "d";
                return _format;
            }
            set
            {
                _format = value;
            }
        }

        /// <summary>
        /// Gets or sets the int value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property can be used to set the int value by passing a
        /// text representation of the int. Any text int representation
        /// that can be parsed by the .NET runtime is valid.
        /// </para><para>
        /// When the int value is retrieved via this property, the text
        /// is formatted by using the format specified by the
        /// <see cref="FormatString" /> property. The default is the
        /// short int format (d).
        /// </para>
        /// </remarks>
        public string Text
        {
            get { return IntToString(this.Int, FormatString, !_emptyIsMax); }
            set { this.Int = StringToInt(value, !_emptyIsMax); }
        }
        #endregion

        #region Int Support

        /// <summary>
        /// Gets or sets the int value.
        /// </summary>
        public int Int
        {
            get
            {
                if (!_initialized)
                {
                    _int = int.MinValue;
                    _initialized = true;
                }
                return _int;
            }
            set
            {
                _int = value;
                _initialized = true;
            }
        }
        #endregion

        #region System.Object overrides

        /// <summary>
        /// Returns a text representation of the int value.
        /// </summary>
        public override string ToString()
        {
            return this.Text;
        }

        /// <summary>
        /// Compares this object to another <see cref="SmartInt"/>
        /// for equality.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is SmartInt)
            {
                SmartInt tmp = (SmartInt)obj;
                if (this.IsEmpty && tmp.IsEmpty)
                    return true;
                else
                    return this.Int.Equals(tmp.Int);
            }
            else if (obj is int)
                return this.Int.Equals((int)obj);
            else if (obj is string)
                return (this.CompareTo(obj.ToString()) == 0);
            else
                return false;
        }

        /// <summary>
        /// Returns a hash code for this object.
        /// </summary>
        public override int GetHashCode()
        {
            return this.Int.GetHashCode();
        }
        #endregion

        #region DBValue
        /// <summary>
        /// Gets a database-friendly version of the int value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the SmartInt contains an empty int, this returns <see cref="DBNull"/>.
        /// Otherwise the actual int value is returned as type Int.
        /// </para><para>
        /// This property is very useful when setting parameter values for
        /// a Command object, since it automatically stores null values into
        /// the database for empty int values.
        /// </para><para>
        /// When you also use the SafeDataReader and its GetSmartInt method,
        /// you can easily read a null value from the database back into a
        /// SmartInt object so it remains considered as an empty int value.
        /// </para>
        /// </remarks>
        public object DBValue
        {
            get
            {
                if (this.IsEmpty)
                    return DBNull.Value;
                else
                    return this.Int;
            }
        }
        #endregion

        #region Empty Ints
        /// <summary>
        /// Gets a value indicating whether this object contains an empty int.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (!_emptyIsMax)
                    return this.Int.Equals(int.MinValue);
                else
                    return this.Int.Equals(int.MaxValue);
            }
        }

        /// <summary>
        /// Gets a value indicating whether an empty int is the
        /// min or max possible int value.
        /// </summary>
        /// <remarks>
        /// Whether an empty int is considered to be the smallest or largest possible
        /// int is only important for comparison operations. This allows you to
        /// compare an empty int with a real int and get a meaningful result.
        /// </remarks>
        public bool EmptyIsMin
        {
            get { return !_emptyIsMax; }
        }
        #endregion

        #region Conversion Functions

        /// <summary>
        /// Converts a string value into a SmartInt.
        /// </summary>
        /// <param name="value">String containing the int value.</param>
        /// <returns>A new SmartInt containing the int value.</returns>
        /// <remarks>
        /// EmptyIsMin will default to <see langword="true"/>.
        /// </remarks>
        public static SmartInt Parse(string value)
        {
            return new SmartInt(value);
        }

        /// <summary>
        /// Converts a string value into a SmartInt.
        /// </summary>
        /// <param name="value">String containing the int value.</param>
        /// <param name="emptyIsMin">Indicates whether an empty int is the min or max int value.</param>
        /// <returns>A new SmartInt containing the int value.</returns>
        public static SmartInt Parse(string value, bool emptyIsMin)
        {
            return new SmartInt(value, emptyIsMin);
        }

        /// <summary>
        /// Converts a text int representation into a Int value.
        /// </summary>
        /// <remarks>
        /// An empty string is assumed to represent an empty int. An empty int
        /// is returned as the MinValue of the Int datatype.
        /// </remarks>
        /// <param name="Value">The text representation of the int.</param>
        /// <returns>A Int value.</returns>
        public static int StringToInt(string value)
        {
            return StringToInt(value, true);

        }

        /// <summary>
        /// Converts a text int representation into a Int value.
        /// </summary>
        /// <remarks>
        /// An empty string is assumed to represent an empty int. An empty int
        /// is returned as the MinValue or MaxValue of the Int datatype depending
        /// on the EmptyIsMin parameter.
        /// </remarks>
        /// <param name="Value">The text representation of the int.</param>
        /// <param name="EmptyIsMin">Indicates whether an empty int is the min or max int value.</param>
        /// <returns>A Int value.</returns>
        public static int StringToInt(string value, bool emptyIsMin)
        {
            int tmp;
            if (String.IsNullOrEmpty(value))
            {
                if (emptyIsMin)
                    return int.MinValue;
                else
                    return int.MaxValue;
            }
            else if (int.TryParse(value, out tmp))
                return tmp;
            else
            {
                string lint = value.Trim().ToLower();
                throw new ArgumentException("Invalid integer value.");
            }
        }

        /// <summary>
        /// Converts a int value into a text representation.
        /// </summary>
        /// <remarks>
        /// The int is considered empty if it matches the min value for
        /// the Int datatype. If the int is empty, this
        /// method returns an empty string. Otherwise it returns the int
        /// value formatted based on the FormatString parameter.
        /// </remarks>
        /// <param name="Value">The int value to convert.</param>
        /// <param name="FormatString">The format string used to format the int into text.</param>
        /// <returns>Text representation of the int value.</returns>
        public static string IntToString(
        int value, string formatString)
        {
            return IntToString(value, formatString, true);

        }

        /// <summary>
        /// Converts a int value into a text representation.
        /// </summary>
        /// <remarks>
        /// Whether the int value is considered empty is determined by
        /// the EmptyIsMin parameter value. If the int is empty, this
        /// method returns an empty string. Otherwise it returns the int
        /// value formatted based on the FormatString parameter.
        /// </remarks>
        /// <param name="Value">The int value to convert.</param>
        /// <param name="FormatString">The format string used to format the int into text.</param>
        /// <param name="EmptyIsMin">Indicates whether an empty int is the min or max int value.</param>
        /// <returns>Text representation of the int value.</returns>
        public static string IntToString(
        int value, string formatString, bool emptyIsMin)
        {
            if (emptyIsMin && value == int.MinValue)
                return string.Empty;
            else if (!emptyIsMin && value == int.MaxValue)
                return string.Empty;
            else
                return string.Format("{0:" + formatString + "}", value);
        }
        #endregion

        #region Manipulation Functions
        /// <summary>
        /// Compares one SmartInt to another.
        /// </summary>
        /// <remarks>
        /// This method works the same as the <see cref="int.CompareTo"/> method
        /// on the Int inttype, with the exception that it
        /// understands the concept of empty int values.
        /// </remarks>
        /// <param name="Value">The int to which we are being compared.</param>
        /// <returns>A value indicating if the comparison int is less than, equal to or greater than this int.</returns>
        public int CompareTo(SmartInt value)
        {
            if (this.IsEmpty && value.IsEmpty)
                return 0;
            else
                return _int.CompareTo(value.Int);
        }

        /// <summary>
        /// Compares one SmartInt to another.
        /// </summary>
        /// <remarks>
        /// This method works the same as the <see cref="int.CompareTo"/> method
        /// on the Int inttype, with the exception that it
        /// understands the concept of empty int values.
        /// </remarks>
        /// <param name="obj">The int to which we are being compared.</param>
        /// <returns>A value indicating if the comparison int is less than, equal to or greater than this int.</returns>
        int IComparable.CompareTo(object value)
        {
            if (value is SmartInt)
                return CompareTo((SmartInt)value);
            else
                throw new ArgumentException("Not a SmartInt to compare to.");
        }

        /// <summary>
        /// Compares a SmartInt to a text int value.
        /// </summary>
        /// <param name="Value">The int to which we are being compared.</param>
        /// <returns>A value indicating if the comparison int is less than, equal to or greater than this int.</returns>
        public int CompareTo(string value)
        {
            return this.Int.CompareTo(StringToInt(value, !_emptyIsMax));
        }

        /// <summary>
        /// Compares a SmartInt to a int value.
        /// </summary>
        /// <param name="Value">The int to which we are being compared.</param>
        /// <returns>A value indicating if the comparison int is less than, equal to or greater than this int.</returns>
        public int CompareTo(int value)
        {
            return this.Int.CompareTo(value);
        }

        /// <summary>
        /// Adds an integer value onto the object.
        /// </summary>
        public int Add(int value)
        {
            if (IsEmpty)
                return this.Int;
            else
                return this.Int + value;
        }

        /// <summary>
        /// Subtracts an integer value from the object.
        /// </summary>
        public int Subtract(int value)
        {
            if (IsEmpty)
                return this.Int;
            else
                return this.Int - value;
        }
        #endregion

        #region Operators
        public static bool operator ==(SmartInt obj1, SmartInt obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(SmartInt obj1, SmartInt obj2)
        {
            return !obj1.Equals(obj2);
        }

        public static bool operator ==(SmartInt obj1, int obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(SmartInt obj1, int obj2)
        {
            return !obj1.Equals(obj2);
        }

        public static bool operator ==(SmartInt obj1, string obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(SmartInt obj1, string obj2)
        {
            return !obj1.Equals(obj2);
        }

        public static SmartInt operator +(SmartInt start, int span)
        {
            return new SmartInt(start.Add(span), start.EmptyIsMin);
        }

        public static SmartInt operator -(SmartInt start, int span)
        {
            return new SmartInt(start.Subtract(span), start.EmptyIsMin);
        }

        public static int operator -(SmartInt start, SmartInt finish)
        {
            return start.Subtract(finish.Int);
        }

        public static bool operator >(SmartInt obj1, SmartInt obj2)
        {
            return obj1.CompareTo(obj2) > 0;
        }

        public static bool operator <(SmartInt obj1, SmartInt obj2)
        {
            return obj1.CompareTo(obj2) < 0;
        }

        public static bool operator >(SmartInt obj1, int obj2)
        {
            return obj1.CompareTo(obj2) > 0;
        }

        public static bool operator <(SmartInt obj1, int obj2)
        {
            return obj1.CompareTo(obj2) < 0;
        }

        public static bool operator >(SmartInt obj1, string obj2)
        {
            return obj1.CompareTo(obj2) > 0;
        }

        public static bool operator <(SmartInt obj1, string obj2)
        {
            return obj1.CompareTo(obj2) < 0;
        }

        public static bool operator >=(SmartInt obj1, SmartInt obj2)
        {
            return obj1.CompareTo(obj2) >= 0;
        }

        public static bool operator <=(SmartInt obj1, SmartInt obj2)
        {
            return obj1.CompareTo(obj2) <= 0;
        }

        public static bool operator >=(SmartInt obj1, int obj2)
        {
            return obj1.CompareTo(obj2) >= 0;
        }

        public static bool operator <=(SmartInt obj1, int obj2)
        {
            return obj1.CompareTo(obj2) <= 0;
        }

        public static bool operator >=(SmartInt obj1, string obj2)
        {
            return obj1.CompareTo(obj2) >= 0;
        }

        public static bool operator <=(SmartInt obj1, string obj2)
        {
            return obj1.CompareTo(obj2) <= 0;
        }
        #endregion
    }
}