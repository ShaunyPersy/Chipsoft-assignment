using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Util;

namespace Chipsoft.Assignments.EPDConsole.Forms
{
    public static class PersonForm
    {
        public static Person CreateForm()
        {
            string firstName = ConsoleUtil.ReadNonEmptyString("Voornaam: ");
            string lastName = ConsoleUtil.ReadNonEmptyString("Familienaam: ");

            DateTime dob = ConsoleUtil.ReadDate("Geboorte datum (dd/mm/yyyy): ");

            string email = ConsoleUtil.ReadValidEmail("Email: ");

            string phone = ConsoleUtil.ReadNonEmptyString("Telefoon nummer: ");

            return new Person
            (
                firstName,
                lastName,
                dob,
                email,
                phone
            );
        }
    }
}