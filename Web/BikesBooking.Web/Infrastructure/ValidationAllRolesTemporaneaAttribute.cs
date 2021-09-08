namespace BikesBooking.Web.Infrastructure
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ValidationAllRolesTemporaneaAttribute : ValidationAttribute
    {
        public ValidationAllRolesTemporaneaAttribute(string allowenRole)
            => this.allowenRole = allowenRole;

        public override bool IsValid(object value)
        {
            var role = value.ToString();
            if (role == "Dealer" || role == "Client")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
