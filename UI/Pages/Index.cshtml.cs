using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SoloContacts.Library;
using SoloContacts.Library.Interfaces;
using SoloContacts.Library.Models;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace SoloContacts.UI.Pages
{
    public class IndexModel : PageModel, IModelBase
    {
        private readonly ILogger<IndexModel> _Logger;
        private readonly IContactService<Contact> _ContactService;

        public IndexModel(ILogger<IndexModel> logger, IContactService<Contact> contactService)
        {
            _Logger = logger;
            _ContactService = contactService;
        }

        public void OnGet()
        {
            Contact _Contact = _ContactService.RetrieveContact(1);
            _Contact.FirstName = "G.I. Joe";
            FirstName = _Contact.FirstName;
            LastName = _Contact.LastName;
            BrokenRulesManager.AddBrokenRuleRange(_Contact.BrokenRulesManager.BrokenRulesCollection);

        }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Core.Validation.BrokenRulesManager BrokenRulesManager { get; } = new Core.Validation.BrokenRulesManager();
    }
}
