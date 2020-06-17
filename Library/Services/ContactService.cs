using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using SoloContacts.Library;
using SoloContacts.Library.Interfaces;
using SoloContacts.Library.Models;

namespace SoloContacts.Library.Services
{
    public class ContactService : IContactService<Contact>
    {

        //private readonly ILogger<IndexModel> _Logger;
        //private readonly ApplicationUser _ApplicationUser;

        //public IndexModel(ILogger<IndexModel> logger, ApplicationUser contactService)

        //public ContactService(ApplicationUser applicationUser)
        //{
        //    _ApplicationUser = applicationUser;
        //}

        public Contact RetrieveContact(int id)
        {



            // private System.Security.Claims.ClaimsPrincipal principal;
            //protected async override void OnParametersSet()
            //{
            //if (authState != null)
            //{
            //System.Security.Claims.ClaimsPrincipal //= (await authState).User;
            //}


            //System.Security.Claims.ClaimsPrincipal _Current = System.Security.Claims.ClaimsPrincipal.Current;








            //ClaimsPrincipal cp = this.User;

            //HttpContext xx


            //_HomeViewModel.DistributorId = Convert.ToInt32(
            //       ClaimsPrincipal.Current.Claims.Where(x => x.Type == "CompanyId").Select(c => c.Value).FirstOrDefault()
            //        );




            Business.Contact _Contact = new Business.Contact() { LastName = "Ugma" };

            _Contact.BrokenRulesManager.AddBrokenRule(Core.Validation.RuleSeverity.Information, "This is an information Message");
            //database call


            //throw new NotImplementedException();

            Contact _Result = new Contact() { LastName = _Contact.LastName};

            _Result.BrokenRulesManager.AddBrokenRuleRange(_Contact.BrokenRulesManager.BrokenRulesCollection);


            return _Result;
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
