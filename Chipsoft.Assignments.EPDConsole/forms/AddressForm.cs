using Chipsoft.Assignments.EPDConsole.Models;
using Chipsoft.Assignments.EPDConsole.Util;

namespace Chipsoft.Assignments.EPDConsole.Forms
{
    public static class AddressForm
    {
        public static Address CreateForm()
        {
            string street = ConsoleUtil.ReadNonEmptyString("Straat: ");

            int houseNbr = ConsoleUtil.ReadInt("Huisnummer: ");
            int postalCode = ConsoleUtil.ReadInt("Postcode: ");

            string city = ConsoleUtil.ReadNonEmptyString("Stad: ");

            return new Address
            (
                street,
                houseNbr,
                postalCode,
                city
            );
        }
    }
}