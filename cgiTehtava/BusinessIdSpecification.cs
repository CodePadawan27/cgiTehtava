using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cgiTehtava
{
    /*Luokan tehtävänä on tarkistaa merkkijonona annetun y-tunnuksen oikeellisuus, 
        ja kertoa, miksi epäkelpo y-tunnus ei täytä vaatimuksia. */
    public class BusinessIdSpecification<TEntity> : ISpecification<TEntity>
    {
        private IEnumerable<string> _reasonsForDissatisfaction = new List<string>()
        {
            "No value given.",
            "No alphabets or special symbols allowed.",
            "Too many characters given.",
            "Too few characters given.",
            "Incorrect format. Correct format is 1234567-8."
        };
        public IEnumerable<string> ReasonsForDissatisfaction
        {
            get
            {
                return _reasonsForDissatisfaction;
            }
        }
        public bool IsSatisfiedBy(TEntity entity)
        {
            var givenString = Convert.ToString(entity);

            //Annettu merkkijono on tyhjä
            if (string.IsNullOrEmpty(givenString))
            {
                Console.WriteLine(ReasonsForDissatisfaction.ElementAt(2));
                return false;
            }

            var yTunnus = new Regex(@"(^[0-9]{6,7})-([0-9]$)");
            Match match = yTunnus.Match(givenString);

            if (match.Success)
            {
                Console.WriteLine("Correct Y-tunnus given.");
                return true;
            }

            //Annettu merkkijono sisältää kirjaimia tai pelkästään erikoismerkkejä
            else if (givenString.Any(char.IsPunctuation) || givenString.Any(char.IsLetter))
            {
                Console.WriteLine(ReasonsForDissatisfaction.ElementAt(1));
                return false;
            }

            //Annettu merkkijono on pienempi kuin 8 merkkiä
            else if (givenString.Length < 8)
            {
                Console.WriteLine(ReasonsForDissatisfaction.ElementAt(3));
                return false;
            }

            //Annettu merkkijono on suurempi kuin 8 merkkiä
            else if (givenString.Length > 8)
            {
                Console.WriteLine(ReasonsForDissatisfaction.ElementAt(2));
                return false;
            }

            //Kaikki muut virheet
            else
            {
                Console.WriteLine(ReasonsForDissatisfaction.ElementAt(4));
                return false;
            }
        }
    }
}
