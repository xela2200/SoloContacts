using System;
using SoloContacts.Core.Data;
using SoloContacts.Core.Validation;

namespace SoloContacts.Library.Business
{
    public class Contact : BusinessBase
    {

        #region Private Variables
        //private int _Id = -1;
        //private int _FirstName = -1;
        //private int _UserIdTo = -1;
        //private string _MessageText = String.Empty;
        //private SmartDate _DateSent = new SmartDate(false);
        #endregion

        #region Constructor
        public Contact() { }

        public Contact(int id)
        {
            Retrieve(id);
        }
        #endregion

        #region CRUD Methods
        public void Create()
        {
            //Apply Authorization rules
            if (!CanCreateObject())
            {
                return;
            }

            //Apply validation rules
            if (!ValidateObject())
            {
                return;
            }

            //Make Database call
            try
            {
                //Data_Create();
            }
            catch (Exception ex)
            {
                _BrokenRulesManager.AddBrokenRule(
                RuleSeverity.Error, "DataProblem",
                ex.Message.Replace("\r\n", " "),
                "Contact.Create");

                ////Log Error
                //AppException.LogError(
                //"An error has occurred while calling the Message.Create method.",
                //ex);
            }
        }

        public void Retrieve(int messageId)
        {

            //Apply Authorization rules
            if (!CanRetrieveObject())
            {
                return;
            }

            //Make Database call
            try
            {
                //Data_Retrieve(messageId);
            }
            catch (Exception ex)
            {
                _BrokenRulesManager.AddBrokenRule(
                RuleSeverity.Error, "DataProblem",
                ex.Message.Replace("\r\n", " "),
                "Message.Retrieve");

                ////Log Error
                //AppException.LogError(
                //"An error has occurred while calling the Message.Retrieve() method.",
                //ex);
            }

        }

        public void Update()
        {
            //Apply Authorization rules
            if (!CanUpdateObject())
            {
                return;
            }

            //Apply validation rules
            if (!ValidateObject())
            {
                return;
            }

            //Make Database call
            try
            {
                //Data_Update();
            }
            catch (Exception ex)
            {
                BrokenRulesManager.AddBrokenRule(
                Core.Validation.RuleSeverity.Error, "DataProblem",
                ex.Message.Replace("\r\n", " "),
                "Message.Update");

                ////Log Error
                //AppException.LogError(
                //"An error has occurred while calling the Message.Update method.",
                //ex);
            }
        }

        public void Delete()
        {
            //Apply Authorization rules
            if (!CanDeleteObject())
            {
                return;
            }

            //Apply validation rules
            if (!ValidateObject())
            {
                return;
            }

            //Make Database call
            try
            {
                //Data_Delete();
            }
            catch (Exception ex)
            {
                BrokenRulesManager.AddBrokenRule(
                RuleSeverity.Error, "DataProblem",
                ex.Message.Replace("\r\n", " "),
                "Message.Update");

                ////Log Error
                //AppException.LogError(
                //"An error has occurred while calling the User.Delete method.",
                //ex);
            }

        }
        #endregion

        #region Class Methods
        //public static List<MessageInfo> ListReceived(int userId)
        //{
        //    return Data_ListReceived(userId);
        //}

        //public static List<MessageInfo> ListThread(int threadId)
        //{
        //    return Data_ListThread(threadId);
        //}

        //public static List<MessageInfo> GetSentList(int userId)
        //{
        //    return Data_GetSentList(userId);
        //}
        #endregion

        #region Authorization Rules
        private bool CanCreateObject()
        {

            //To do later
            /**************
            bool _Result = false;
            if (ApplicationContext.User.IsInRole("Administrators")) { _Result = true; }
            int _UserId = Convert.ToInt32(ApplicationContext.User.Identity.Name);
            if (_UserId == _UserTo || _UserId == _UserFrom) { _Result = true; }
            return _Result;
            ***********/

            return true;
        }

        private bool CanRetrieveObject()
        {
            return true;
        }

        private bool CanUpdateObject()
        {
            return true;
        }

        private bool CanDeleteObject()
        {
            return true;
        }

        #endregion

        #region Validation Rules
        private bool ValidateObject()
        {
            bool _Result = true;
            //if (_UserIdTo == -1)
            //{
            //    _BrokenRulesManager.AddBrokenRule(
            //    RuleSeverity.Error, "NoUserTo",
            //    "User To is required.", "Message.Validation");
            //    _Result = false;
            //}

            //if (_UserIdFrom == -1)
            //{
            //    _BrokenRulesManager.AddBrokenRule(
            //    RuleSeverity.Error, "NoUserFrom",
            //    "User From is required.", "Message.Validation");
            //    _Result = false;
            //}

            //if (_UserIdFrom == _UserIdTo)
            //{
            //    _BrokenRulesManager.AddBrokenRule(
            //    RuleSeverity.Error, "NoSelfEmail",
            //    "You cannot send emails to yourself.", "Message.Validation");
            //    _Result = false;
            //}

            //if (_ThreadId == -1)
            //{
            //    _BrokenRulesManager.AddBrokenRule(
            //    RuleSeverity.Error, "NoThreadId",
            //    "Thread Id is required.", "Message.Validation");
            //    _Result = false;
            //}

            return _Result;
        }

        private bool ValidateUpdate()
        {
            bool _Result = true;
            //int _UserId = Convert.ToInt32(ApplicationContext.User.Identity.Name);
            //if (_UserId == _UserIdFrom) { _Result = false; }
            return _Result;
        }
        #endregion

        #region Properties
        //public int Id
        //{
        //    get { return _Id; }
        //    set { _Id = value; }
        //}

        //public int Id { get; set; } = int.MinValue;

        public Guid Id { get; set; } = new Guid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public SmartDate DateCreated { get; set; } = new SmartDate(false);
        #endregion

        #region Data Access
        //private void Data_Create()
        //{
        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["OpenToDate"].ConnectionString))
        //    {
        //        cn.Open();
        //        using (SqlCommand cm = cn.CreateCommand())
        //        {
        //            cm.CommandType = CommandType.StoredProcedure;
        //            cm.CommandText = "MessageCreate";
        //            cm.Parameters.AddWithValue("@UserIdFrom", _UserIdFrom);
        //            cm.Parameters.AddWithValue("@UserIdTo", _UserIdTo);
        //            cm.Parameters.AddWithValue("@MessageText", _MessageText);
        //            cm.Parameters.AddWithValue("@DateSent", _DateSent.DBValue);
        //            cm.Parameters.AddWithValue("@ThreadId", _ThreadId);
        //            cm.Parameters.AddWithValue("@MessageId", SqlDbType.Int).Direction = ParameterDirection.Output;

        //            cm.ExecuteNonQuery();

        //            _Id = Convert.ToInt32(cm.Parameters["@MessageId"].Value);
        //        }
        //    }
        //}

        //private void Data_Retrieve(int messageId)
        //{
        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["OpenToDate"].ConnectionString))
        //    {
        //        cn.Open();
        //        using (SqlCommand cm = cn.CreateCommand())
        //        {
        //            cm.CommandType = CommandType.StoredProcedure;
        //            cm.CommandText = "MessageRetrieve";
        //            cm.Parameters.AddWithValue("@MessageId", messageId);

        //            using (SafeDataReader dr = new SafeDataReader(cm.ExecuteReader()))
        //            {
        //                dr.Read();
        //                _Id = dr.GetInt32("MessageId");
        //                _UserIdFrom = dr.GetInt32("UserIdFrom");
        //                _UserIdTo = dr.GetInt32("UserIdTo");
        //                _MessageText = dr.GetString("MessageText");
        //                _DateSent = dr.GetSmartDate("DateSent");
        //                _ThreadId = dr.GetInt32("ThreadId");

        //                //dr.NextResult();
        //                //while (dr.Read())
        //                //{
        //                //    _UserFrom = new Library.UserInfo(dr);
        //                //}

        //                //dr.NextResult();
        //                //while (dr.Read())
        //                //{
        //                //    _UserTo = new Library.UserInfo(dr);
        //                //}
        //            }
        //        }
        //    }
        //}

        //private void Data_Update()
        //{
        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["OpenToDate"].ConnectionString))
        //    {
        //        cn.Open();
        //        using (SqlCommand cm = cn.CreateCommand())
        //        {
        //            cm.CommandType = CommandType.StoredProcedure;
        //            cm.CommandText = "MessageUpdate";
        //            cm.Parameters.AddWithValue("@MessageId", _Id);
        //            cm.Parameters.AddWithValue("@UserIdFrom", _UserIdFrom);
        //            cm.Parameters.AddWithValue("@UserIdTo", _UserIdTo);
        //            cm.Parameters.AddWithValue("@MessageText", _MessageText);
        //            cm.Parameters.AddWithValue("@DateSent", _DateSent.DBValue);
        //            cm.Parameters.AddWithValue("@TheadId", _ThreadId);
        //            cm.ExecuteNonQuery();
        //        }
        //    }
        //}

        //private void Data_Delete()
        //{
        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["OpenToDate"].ConnectionString))
        //    {
        //        cn.Open();
        //        using (SqlCommand cm = cn.CreateCommand())
        //        {
        //            cm.CommandType = CommandType.StoredProcedure;
        //            cm.CommandText = "MessageDelete";
        //            cm.Parameters.AddWithValue("@MessageId", _Id);

        //            cm.ExecuteNonQuery();
        //        }
        //    }
        //}

        //private static List<MessageInfo> Data_ListReceived(int userId)
        //{
        //    List<MessageInfo> _Messages = new List<MessageInfo>();

        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["OpenToDate"].ConnectionString))
        //    {

        //        using (SqlCommand cm = cn.CreateCommand())
        //        {
        //            cm.CommandType = CommandType.StoredProcedure;
        //            cm.CommandText = "MessageListReceived";
        //            cm.Parameters.AddWithValue("@UserId", userId);
        //            cm.CommandTimeout = 90;
        //            cn.Open();


        //            using (SafeDataReader dr = new SafeDataReader(cm.ExecuteReader()))
        //            {
        //                while (dr.Read())
        //                {
        //                    MessageInfo _MessageInfo = new MessageInfo(dr);
        //                    _Messages.Add(_MessageInfo);
        //                }
        //            }
        //        }
        //    }
        //    return _Messages;
        //}

        //private static List<MessageInfo> Data_ListThread(int threadId)
        //{
        //    List<MessageInfo> _Messages = new List<MessageInfo>();

        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["OpenToDate"].ConnectionString))
        //    {

        //        using (SqlCommand cm = cn.CreateCommand())
        //        {
        //            cm.CommandType = CommandType.StoredProcedure;
        //            cm.CommandText = "MessageListThread";
        //            cm.Parameters.AddWithValue("@ThreadId", threadId);
        //            cm.CommandTimeout = 90;
        //            cn.Open();


        //            using (SafeDataReader dr = new SafeDataReader(cm.ExecuteReader()))
        //            {
        //                while (dr.Read())
        //                {
        //                    MessageInfo _MessageInfo = new MessageInfo(dr);
        //                    _Messages.Add(_MessageInfo);
        //                }
        //            }
        //        }
        //    }
        //    return _Messages;
        //}

        //private static List<MessageInfo> Data_GetSentList(int userId)
        //{
        //    List<MessageInfo> _Messages = new List<MessageInfo>();

        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["OpenToDate"].ConnectionString))
        //    {

        //        using (SqlCommand cm = cn.CreateCommand())
        //        {
        //            cm.CommandType = CommandType.StoredProcedure;
        //            cm.CommandText = "MessageSent";
        //            cm.Parameters.AddWithValue("@UserId", userId);
        //            cm.CommandTimeout = 90;
        //            cn.Open();


        //            using (SafeDataReader dr = new SafeDataReader(cm.ExecuteReader()))
        //            {
        //                while (dr.Read())
        //                {
        //                    MessageInfo _MessageInfo = new MessageInfo(dr);
        //                    _Messages.Add(_MessageInfo);
        //                }
        //            }
        //        }
        //    }
        //    return _Messages;
        //}
        #endregion
    }
}
