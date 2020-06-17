using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoloContacts.Core.Validation;
using SoloContacts.Library.Models;


namespace SoloContacts.UI.Pages.Account
{
    public class RegisterModel : PageModel, IModelBase
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [BindProperty]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [BindProperty]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [BindProperty]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public BrokenRulesManager BrokenRulesManager { get; } = new BrokenRulesManager();

        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;




        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            //ViewData["ReturnUrl"] = returnUrl;

            var user = new ApplicationUser { UserName = Email, Email = Email };

            var _Result = await _UserManager.CreateAsync(user, Password);

            //BrokenRulesManager.AddBrokenRule(RuleSeverity.Error,)
            if (!_Result.Succeeded)
            {
                _Result.Errors.ToList().ForEach(x => BrokenRulesManager.AddBrokenRule(RuleSeverity.Error, x.Description));
                ModelState.Clear();

                return Page();
            }



            await _SignInManager.SignInAsync(user, isPersistent: false);

            // return _Result;
            return Page();



            //if (ModelState.IsValid)
            //{

            //Task<IdentityResult>


            //if (_Result.Succeeded)
            //{
            ////Add notification - account created
            //Models.Notification _Notification = new Models.Notification
            //{
            //    ApplicationId = 1,
            //    ApplicationUserId = user.Id,
            //    Subject = "Account has been created",
            //    Message = "Welcome, your account has been created succesfully, and you have been granted access to all functionality on the application.",
            //    IsRead = false,
            //    DateSubmitted = new Data.SmartDate(DateTime.Now)
            //};

            //try
            //{
            //    _NotificationStore.Create(_Notification);
            //}
            //catch (System.Exception _Exception)
            //{
            //    _Logger.LogError(_Exception, _Exception.Message);
            //}

            //_Logger.LogInformation("User created a new account with password.");

            // Email Confirm with startup --> config.SignIn.RequireConfirmedEmail = true
            //var code = await _UserManager.GenerateEmailConfirmationTokenAsync(user);
            //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
            //await _EmailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

            ////Secret key
            //StripeConfiguration.ApiKey = _Configuration.GetSection("Stripe:SecretKey").Value;

            ////1 - Get credit card saved on stripe with the form and get credit card token
            //string _StripeToken = Request.Form["stripeToken"];

            //if (string.IsNullOrEmpty(_StripeToken))
            //{
            //    _Logger.LogError("stripeToken is null for " + _Register.Email);
            //    _Register.BrokenRulesManager.AddBrokenRule(Validation.RuleSeverity.Error, "A valid credit card is required.");
            //    return View(_Register);
            //}

            ////2 - Create Customer and get CustomerId token
            //CustomerCreateOptions customerOptions = new CustomerCreateOptions
            //{
            //    Source = _StripeToken,
            //    Email = _Register.Email
            //};

            //CustomerService customerService = new CustomerService();
            //Customer _Customer;

            //    try
            //    {
            //        _Customer = customerService.Create(customerOptions);
            //    }
            //    catch (Exception _Exception)
            //    {
            //        _Logger.LogError(_Exception, _Exception.Message);
            //        _Register.BrokenRulesManager.AddBrokenRule(Validation.RuleSeverity.Error, _Exception.Message, _Exception.StackTrace);
            //        return View(_Register);
            //    }

            //    //3 - Add Subscription to customer 
            //    var _Items = new List<SubscriptionItemOptions>
            //    {
            //      new SubscriptionItemOptions
            //      {
            //        Plan = _Register.Subscription.ProcessorPlanId
            //      }
            //    };

            //    SubscriptionCreateOptions _SubscriptionCreateOptions = new SubscriptionCreateOptions
            //    {
            //        Customer = _Customer.Id,
            //        Items = _Items
            //    };
            //    _SubscriptionCreateOptions.AddExpand("latest_invoice.payment_intent");
            //    SubscriptionService service = new SubscriptionService();

            //    try
            //    {
            //        Stripe.Subscription _Subscription = service.Create(_SubscriptionCreateOptions);
            //        _Register.Subscription.ProcessorSubscriptionId = _Subscription.Id;

            //    }
            //    catch (Exception _Exception)
            //    {
            //        _Logger.LogError(_Exception, _Exception.Message);
            //        _Register.BrokenRulesManager.AddBrokenRule(Validation.RuleSeverity.Error, _Exception.Message, _Exception.StackTrace);
            //        return View(_Register);
            //    }

            //    await _SignInManager.SignInAsync(user, isPersistent: false);
            //    _Logger.LogInformation("User created a new account with password.");

            //    //create subscription in database
            //    _Register.Subscription.StartDate = new Data.SmartDate(DateTime.Now);
            //    _Register.Subscription.EndDate = new Data.SmartDate(DateTime.MaxValue);
            //    _Register.Subscription.Active = true;
            //    _Register.Subscription.ApplicationUserId = user.Id;
            //    _Register.Subscription.Processor = Models.Processor.Stripe;
            //    _Register.Subscription.ProcessorCustomerId = _Customer.Id;

            //    try
            //    {
            //        _SubscriptionStore.Create(_Register.Subscription);
            //    }
            //    catch (System.Exception _Exception)
            //    {
            //        _Logger.LogError(_Exception, _Exception.Message);
            //        _Register.BrokenRulesManager.AddBrokenRule(Validation.RuleSeverity.Error, _Exception.Message, _Exception.StackTrace);
            //        return View(_Register);
            //    }

            //    //Add notification - subscription created
            //    _Notification.Subject = "Subscription has been added";
            //    _Notification.Message = "A subscription has been created with our credit card processor Stipe. " +
            //        "Your credit card will be billed periodically until the subscription is cancelled. " +
            //        "Cancel anytime by going to Account > Subscription on the navigation sidebar.";
            //    _Notification.DateSubmitted = new Data.SmartDate(DateTime.Now);
            //    try
            //    {
            //        _NotificationStore.Create(_Notification);
            //    }
            //    catch (System.Exception _Exception)
            //    {
            //        _Logger.LogError(_Exception, _Exception.Message);
            //    }

            //    return RedirectToLocal(returnUrl);
            //}
            //AddErrors(_Result);
            //}

            // If we got this far, something failed, redisplay form
            //return View(_Register);

        }

    }
}
