namespace BikesBooking.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using BikesBooking.Data.Models;
    using BikesBooking.Services.Data.Client;
    using BikesBooking.Services.Data.Dealer;
    using BikesBooking.Services.Data.DTO.Dealers;
    using BikesBooking.Services.Data.User;
    using BikesBooking.Web.ViewModels.Client;
    using BikesBooking.Web.ViewModels.Dealers;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    using static BikesBooking.Common.GlobalConstants;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly IDealersService dealersService;
        private readonly IUserService userService;
        private readonly IServiceProvider serviceProvider;
        private readonly IClientService clientService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IDealersService dealersService,
            IUserService userService,
            IServiceProvider serviceProvider,
            IClientService clientService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.dealersService = dealersService;
            this.userService = userService;
            this.serviceProvider = serviceProvider;
            this.clientService = clientService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = this.Input.Email, FirstName = this.Input.FirstName, LastName = this.Input.LastName, Email = this.Input.Email };
                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(this.Input.Email, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (this.Input.Role == "Dealer")
                    {
                        var isAlreadyExistId = this.dealersService.IsDealer(user.Id);

                        if (isAlreadyExistId)
                        {
                            return this.BadRequest();
                        }

                        var dealer = new CreateDealerDto
                        {
                            Name = this.Input.DealerFormModel.Name,
                            Address = this.Input.DealerFormModel.Address,
                            City = this.Input.DealerFormModel.City,
                            Country = this.Input.DealerFormModel.Country,
                            Description = this.Input.DealerFormModel.Description,
                            Email = this.Input.DealerFormModel.Email,
                        };

                        await this.dealersService.CreateDealerAsync(dealer, user.Id);

                        await this.userService.AssignRole(this.serviceProvider, dealer.Email, DealerRoleName);

                        returnUrl ??= this.Url.Content("~/Motor/All");
                    }
                    else if (this.Input.Role == "Client")
                    {
                        var isAlreadyExists = this.clientService.IsAlreadyClientExist(user.Id);

                        if (isAlreadyExists)
                        {
                            return this.BadRequest();
                        }

                        await this.clientService.CreateClientAsync(user.Id, this.Input.ClientFromModel.Address, this.Input.ClientFromModel.City);

                        var email = this.clientService.GetCurrentClientEmail(user.Id);
                        await this.userService.AssignRole(this.serviceProvider, email, ClientRoleName);

                        returnUrl ??= this.Url.Content("~/");
                    }


                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
          
            public string Role { get; set; }

            public BecomeDealerFormModel DealerFormModel { get; set; }

            public BecomeClientFromModel ClientFromModel { get; set; }
        }
    }
}
